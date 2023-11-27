using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    [Header("General")]
    [SerializeField] float seeRange = 5f;
    [SerializeField] float hearRange = 7f;
    [SerializeField] float sneakHearRange = 1.5f;
    [SerializeField] float distractedForSeconds = 3f;

    [Header("Movement")]
    [SerializeField] float runSpeed = 300f;
    [SerializeField] float walkSpeed = 150f;
    [SerializeField] float jumpHeight = 3.5f;
    
    [Header("Combat")]
    [SerializeField] AttackType attackType = AttackType.Melee;
    [SerializeField] int damage = 1;
    [SerializeField] float attackRange = 1.5f;
    [SerializeField] float attackDelay = 1;
    [SerializeField] Projectile projectile;
    
	[SerializeField] UnityEvent movedEvent;
    [SerializeField] UnityEvent movedQuicklyEvent;
    [SerializeField] UnityEvent startedChasingEvent;
    [SerializeField] UnityEvent jumpedEvent;
    [SerializeField] UnityEvent shotEvent;
    [SerializeField] UnityEvent attackedEvent;
    [SerializeField] UnityEvent turnedEvent;

    GameObject _player;
    GameObject _distraction;
    Health _playerHealth;
    PlayerHide _playerHide;
    Vector2 _playerPosition;
    
    Transform _transform;
    Rigidbody2D _rigidbody;
    Collider2D _collider;
    
    LayerMask _groundLayer;

    enum AttackType { Melee, Range }
    enum Direction { Left = -1, Right = 1 }
    enum State { Idle, Patrol, Chase }
    
    [SerializeField] Direction direction = Direction.Right;
    [SerializeField] State state = State.Patrol;

    Vector2 _position;
    Bounds _colliderBounds;
    float _speed;

    Vector2 _distractionPosition;
    float _distanceToPlayer;
    float _distanceToPlayerHorizontal;
    Direction _playerDirection;
    Direction _playerDirectionKnown;

    float _distanceToDistraction;
    Direction _distractionDirection;

    bool _isDistracted;
    bool _isGrounded;
    bool _canAttack = true;
    bool _canJumpOverObstacle;
    bool _seesPlayer;

    void Awake()
    {
        _player = GameObject.Find("Body");
	    _playerHide = _player.GetComponent<PlayerHide>();
	    _playerHealth = _player.GetComponent<Health>();

	    _distraction = new GameObject();

	    _transform = transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _groundLayer = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        UpdateReferences();
        CheckGrounded();
        UpdateState();
        HandleObstacles();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void UpdateReferences()
    {
        _position = _transform.position;
        _playerPosition = _player.transform.position;
        _playerDirection = _position.x > _playerPosition.x ? Direction.Left : Direction.Right;

        _distanceToPlayer = Vector2.Distance(_position, _playerPosition);
        _distanceToPlayerHorizontal = Mathf.Abs(_position.x - _playerPosition.x);
        
        _distanceToDistraction = Vector2.Distance(_position, _distractionPosition);
        _distractionDirection = _position.x > _distanceToDistraction ? Direction.Left : Direction.Right;

        _colliderBounds = _collider.bounds;
        
        var collidersInSight = Physics2D.OverlapAreaAll(new Vector2(_colliderBounds.center.x + (int)direction*seeRange, _colliderBounds.center.y + _colliderBounds.size.y), new Vector2(_colliderBounds.center.x, _colliderBounds.center.y));

        foreach (var collider in collidersInSight)
        {
	        if (collider.gameObject.CompareTag("Player"))
	        {
		        _seesPlayer = !Physics2D.Raycast(new Vector2(_colliderBounds.center.x, _colliderBounds.center.y + _colliderBounds.size.y / 2), direction == Direction.Left ? Vector2.left : Vector2.right, _distanceToPlayerHorizontal, _groundLayer);
		        if (_seesPlayer)
		        {
			        _playerDirectionKnown = _playerDirection;
		        }
	        }
	        else
	        {
		        _seesPlayer = false;
	        }
        }
    }

    void CheckGrounded()
    {
        _isGrounded = Physics2D.Raycast(_colliderBounds.center, Vector2.down, 1.5f, _groundLayer);
        Debug.DrawRay(new Vector2(_colliderBounds.center.x, _colliderBounds.center.y), Vector2.down, _isGrounded ? Color.green : Color.red);
    }

    void UpdateState()
    {
        switch (state)
        {
            case State.Idle:
                _speed = 0f;
                break;
            case State.Patrol:
                UpdatePatrolState();
                break;
            case State.Chase:
                UpdateChaseState();
                break;
            default:
                state = State.Idle;
                break;
        }
    }

    void UpdatePatrolState()
    {
        _speed = walkSpeed;

        var isGroundAhead = Physics2D.Raycast(new Vector2(_colliderBounds.center.x + (int)direction, _colliderBounds.center.y), Vector2.down, 1.5f, _groundLayer);
        Debug.DrawRay(new Vector2(_colliderBounds.center.x + (int)direction, _colliderBounds.center.y), Vector2.down, isGroundAhead ? Color.green : Color.red);
        if (!isGroundAhead && _isGrounded)
        {
            FlipDirection();
        }

        if (_playerHide.IsHidden)
        {
            return;
        }
        
        if (_distanceToPlayer < sneakHearRange)
        {
            state = State.Chase;
            _playerDirectionKnown = _playerDirection;
			startedChasingEvent.Invoke();
            return;
        }

        if (_seesPlayer || _isDistracted)
        {
	        state = State.Chase;
			startedChasingEvent.Invoke();
	        return;
        }
    }

    void UpdateChaseState()
    {
        if (!_seesPlayer && !_isDistracted && _distanceToPlayer > seeRange)
        {
            state = State.Patrol;
            return;
        }
        _speed = runSpeed;

        if (_isDistracted && _seesPlayer)
        {
	        _isDistracted = false;
        }
        
        if (_isDistracted && _distractionDirection != direction)
        {
	        FlipDirection();
	        return;
        }
        
        if (direction != _playerDirectionKnown && !_isDistracted)
        {
            FlipDirection();
        }

        if (_distanceToPlayer < attackRange)
        {
	        _speed = 0;
            if (!_canAttack) return;
            Attack();
            StartCoroutine(WaitForNextAttack());
        }
    }
    
    void HandleObstacles()
    {
        var isObstacleAhead = Physics2D.Raycast(new Vector2(_colliderBounds.center.x, _colliderBounds.center.y - _colliderBounds.size.y / 2 + 0.1f), Vector2.right * (int)direction, 1f, _groundLayer);
        var isObstacleAboveHeight = Physics2D.Raycast(new Vector2(_colliderBounds.center.x, _colliderBounds.center.y + _colliderBounds.size.y / 2 + 0.1f), Vector2.right, 1f, _groundLayer);
        _canJumpOverObstacle = isObstacleAhead && !isObstacleAboveHeight;
        Debug.DrawRay(new Vector2(_colliderBounds.center.x, _colliderBounds.center.y + _colliderBounds.size.y / 2), Vector2.right * (int)direction, isObstacleAboveHeight ? Color.red : Color.green);
        Debug.DrawRay(new Vector2(_colliderBounds.center.x, _colliderBounds.center.y - _colliderBounds.size.y / 2), Vector2.right * (int)direction, isObstacleAhead ? Color.red : Color.green);

        if (isObstacleAboveHeight && _isGrounded)
        {
            FlipDirection();
        }
    }

    void HandleMovement()
    {
        if (_distanceToPlayer < attackRange && state == State.Chase) return;
        _rigidbody.velocity = new Vector2((int)direction * _speed * Time.deltaTime, _rigidbody.velocity.y);
		if(_speed == runSpeed) movedQuicklyEvent.Invoke(); 
		else movedEvent.Invoke();
        
        if (_canJumpOverObstacle && _isGrounded) //jump
        {
	        _rigidbody.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
			jumpedEvent.Invoke();
        }
    }

    void Attack()
    {
	    if (!_canAttack) return;

	    switch (attackType)
	    {
		    case AttackType.Melee:
			    _playerHealth.Damage(damage);
	    		attackedEvent.Invoke();
			    break;
		    case AttackType.Range:
			    var projectileInstance = Instantiate(projectile, _colliderBounds.center, Quaternion.identity);
			    projectileInstance.Damage = damage;
	    		shotEvent.Invoke();
			    break;
	    }
    }

    IEnumerator WaitForNextAttack()
    {
        _canAttack = false;
        yield return new WaitForSeconds(attackDelay);
        _canAttack = true;
    }

    void FlipDirection()
    {
        direction = direction == Direction.Left ? Direction.Right : Direction.Left;
        turnedEvent.Invoke();
    }
    
    IEnumerator GetDistracted(Vector2 position, float seconds)
    {
	    _distraction.transform.position = position;
	    _distractionPosition = position;
	    
	    _isDistracted = true;
	    yield return new WaitForSeconds(seconds);
	    _isDistracted = false;
    }

    public void NoticeDistraction(Vector2 position)
    {
	    var distanceToDistraction = Vector2.Distance(_position, position);
	    if (distanceToDistraction > hearRange) return;
	    StartCoroutine(GetDistracted(position, distractedForSeconds));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
	    if (other.gameObject.CompareTag("LeftPatrolTrigger"))
	    {
		    if (direction == Direction.Left && state == State.Patrol)
		    {
			    FlipDirection();
		    }
	    }

	    if (other.gameObject.CompareTag("RightPatrolTrigger"))
	    {
		    if (direction == Direction.Right && state == State.Patrol)
		    {
			    FlipDirection();
		    }
	    }
    }
}