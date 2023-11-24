using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] bool isTargetLoud;
    Health _targetHealth;
    LayerMask _targetLayer;
    PlayerHide _targetHide;
    int _targetPriority = 5;
    
    LayerMask _groundLayer;

    Rigidbody2D _rigidbody;
    Transform _transform;
    Collider2D _collider;

    EnemyAnimations _animations;

    [SerializeField] float seeRange;
    [SerializeField] float hearRange;
    [SerializeField] float sneakHearRange;
    [SerializeField] float attackRange;
    [SerializeField] float attackDelay;
    [SerializeField] int damage;
    [SerializeField] Projectile projectile;
    bool _canAttack = true;
    [SerializeField] UnityEvent attackedEvent;

    [SerializeField] float runSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float jumpHeight;
    bool _isGrounded;

    enum Direction
    {
        Left = -1,
        Right = 1
    }
    enum State
    {
        Idle,
        Patrol,
        Chase,
    }

    enum AttackType
    {
        Melee,
        Range
    }

    [SerializeField] Direction direction = Direction.Right;
    [SerializeField] State state = State.Patrol;
    [SerializeField] AttackType attackType = AttackType.Melee;
    Vector2 _positionReference;
    Vector2 _targetPositionReference;
    float _speed;
    float _distanceToTarget;
    float _distanceToTargetHorizontal;
    Direction _targetDirection;
    Bounds _colliderBoundsReference;

    void Awake()
    {
        if (!target)
        {
            target = GameObject.FindWithTag("Player");
        }

        if (target.CompareTag("Player"))
        {
            _targetHide = target.GetComponent<PlayerHide>();
        }
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _targetHealth = target.GetComponent<Health>();
        _groundLayer = LayerMask.GetMask("Ground");

        _animations = GetComponent<EnemyAnimations>();
    }

    void Start()
    {
        _transform = transform;
        _targetLayer = target.layer;
    }

    void Update()
    {
        _positionReference = (Vector2) _transform.position;
        _targetPositionReference = (Vector2) target.transform.position;

        _speed = 0f;

        _distanceToTarget = Vector2.Distance(_positionReference, _targetPositionReference);
        _distanceToTargetHorizontal = Mathf.Abs(_positionReference.x - _targetPositionReference.x);
        _targetDirection = _positionReference.x > _targetPositionReference.x ? Direction.Left : Direction.Right;

        _colliderBoundsReference = _collider.bounds;
        _isGrounded = Physics2D.Raycast(new Vector2(_colliderBoundsReference.center.x, _colliderBoundsReference.center.y), Vector2.down, 1.5f, _groundLayer);
        Debug.DrawRay(new Vector2(_colliderBoundsReference.center.x, _colliderBoundsReference.center.y), Vector2.down, _isGrounded ? Color.green : Color.red);
        switch (state)
        {
            case State.Idle:
            {
                _speed = 0f;
                break;
            }
            case State.Patrol:
            {
                _speed = walkSpeed;
                
                var isGroundAhead = Physics2D.Raycast(new Vector2(_colliderBoundsReference.center.x + (int)direction, _colliderBoundsReference.center.y), Vector2.down, 1.5f, _groundLayer);
                Debug.DrawRay(new Vector2(_colliderBoundsReference.center.x + (int)direction, _colliderBoundsReference.center.y), Vector2.down, isGroundAhead ? Color.green : Color.red);
                if (!isGroundAhead && _isGrounded)
                {
                    FlipDirection();
                }
                
                if (_targetHide && _targetHide.IsHidden)
                {
                    break;
                }
                
                if (_distanceToTarget < sneakHearRange)
                {
                    state = State.Chase;
                    break;
                }

                if (_distanceToTarget < hearRange && isTargetLoud)
                {
                    state = State.Chase;
                    break;
                }

                if (_distanceToTarget < seeRange && direction == _targetDirection)
                {
                    state = State.Chase;
                    break;
                }
                
                break;
            }
            case State.Chase:
            {
                if (_distanceToTarget > seeRange)
                {
                    state = State.Patrol;
                    break;
                }
                _speed = runSpeed;
                if (direction != _targetDirection)
                {
                    FlipDirection();
                }

                if (_distanceToTarget < attackRange)
                {
                    if (!_canAttack) break;
                    StartCoroutine(WaitForNextAttack());
                    Attack();
                }
                break;
            }
            default:
            {
                state = State.Idle;
                break;
            }
        }

        var isObstacleAhead = Physics2D.Raycast(new Vector2(_colliderBoundsReference.center.x, _colliderBoundsReference.center.y - _colliderBoundsReference.size.y / 2 + 0.1f), Vector2.right * (int)direction, 1f, _groundLayer);
        var isObstacleAboveHeight = Physics2D.Raycast(new Vector2(_colliderBoundsReference.center.x, _colliderBoundsReference.center.y + _colliderBoundsReference.size.y / 2 + 0.1f), Vector2.right, 1f, _groundLayer);
        _canJumpOverObstacle = isObstacleAhead && !isObstacleAboveHeight;
        Debug.DrawRay(new Vector2(_colliderBoundsReference.center.x, _colliderBoundsReference.center.y + _colliderBoundsReference.size.y / 2), Vector2.right * (int)direction, isObstacleAboveHeight ? Color.red : Color.green);
        Debug.DrawRay(new Vector2(_colliderBoundsReference.center.x, _colliderBoundsReference.center.y - _colliderBoundsReference.size.y / 2), Vector2.right * (int)direction, isObstacleAhead ? Color.red : Color.green);

        if (isObstacleAboveHeight && _isGrounded)
        {
            FlipDirection();
        }
    }

    bool _canJumpOverObstacle;
    void FixedUpdate()
    {
        if (_canJumpOverObstacle)
        {
            Jump();
        }
        
        if (_distanceToTargetHorizontal < attackRange && state == State.Chase) return;
        _rigidbody.velocity = new Vector2((int)direction * _speed * Time.deltaTime, _rigidbody.velocity.y);
    }

    void Attack()
    {
        if (!_targetHealth) return;
        
        switch (attackType)
        {
            case AttackType.Melee:
                _targetHealth.Damage(damage);
                attackedEvent.Invoke();
                break;
            case AttackType.Range:
                var projectile = Instantiate(this.projectile, transform);
                projectile.GetComponent<Projectile>().Init(target.transform, damage);
                attackedEvent.Invoke();
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
        _animations.Flip();
    }

    public void SetTarget(GameObject newTarget, bool isLoud, int priority)
    {
        target = newTarget;
        isTargetLoud = isLoud;
        _targetPriority = priority;
    }

    public IEnumerator SetTemporaryTarget(GameObject newTarget, bool isLoud, int priority, float seconds)
    {
        if (priority < _targetPriority && state == State.Chase) yield break;
        var previousTarget = target;
        var wasPreviousTargetLoud = isTargetLoud;
        var previousTargetPriority = _targetPriority;
        
        SetTarget(newTarget, isLoud, priority);
        yield return new WaitForSeconds(seconds);
        
        SetTarget(previousTarget, wasPreviousTargetLoud, previousTargetPriority);
    }

    public IEnumerator SetTemporaryTargetFromPosition(Vector2 position, bool isLoud, int priority, float seconds)
    {
        if (priority < _targetPriority && state == State.Chase) yield break;
        var previousTarget = target;
        var wasPreviousTargetLoud = isTargetLoud;
        var previousTargetPriority = _targetPriority;

        var newTarget = new GameObject
        {
            transform =
            {
                position = position
            }
        };

        SetTarget(newTarget, isLoud, priority);
        yield return new WaitForSeconds(seconds);
        
        SetTarget(previousTarget, wasPreviousTargetLoud, previousTargetPriority);
        Destroy(newTarget);
    }
    
    public void NoticeDistraction(Vector2 position)
    {
        var distanceToDistraction = Vector2.Distance(_transform.position, position);
        if (distanceToDistraction > hearRange) return;
        StartCoroutine(SetTemporaryTargetFromPosition(position, true, 0, 5f));
    }

    void Jump()
    {
        if (_isGrounded)
        {
            _rigidbody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }
    }
}
