using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Transform _player;
    [SerializeField] Transform leftPatrolTrigger;
    [SerializeField] Transform rightPatrolTrigger;
    [SerializeField] Transform leftChaseTrigger;
    [SerializeField] Transform rightChaseTrigger;
    [SerializeField] float patrolSpeed = 1.5f;
    [SerializeField] float chaseSpeed = 3;
    float _speed;
    [SerializeField] float detectRange = 5;
    [SerializeField] float hearRange = 1.5f;
    float _attackRange;
    [SerializeField] GameObject projectile;
    [SerializeField] float attackDelay = 1;
    bool _canAttack = true;
    Health _playerHealth;
    LayerMask _playerLayer;

    void Awake()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _speed = patrolSpeed;
        _attackRange = type == Type.Melee ? 1.5f : 3.5f;
        _playerLayer = LayerMask.GetMask("Player");
        _playerHealth = _player.GetComponent<Health>();
    }

    enum State
    {
        Patrol,
        Chase,
        Return,
        Hold
    }

    public enum Direction
    {
        Left = -1,
        Right = 1
    }

    enum Type
    {
        Melee,
        Range
    }

    State _state = State.Patrol;
    Direction _direction = Direction.Left;
    [SerializeField] Type type = Type.Melee;
    float _distanceToPlayer;
    Direction _playerDirection = Direction.Left;

    void Update()
    {
        var position = transform.position;
        var playerPosition = _player.position;
        _distanceToPlayer = Vector3.Distance(position, playerPosition);
        if (playerPosition.x - position.x < 0)
        {
            _playerDirection = Direction.Left;
        }

        if (playerPosition.x - position.x > 0)
        {
            _playerDirection = Direction.Right;
        }

        transform.Translate(_speed * (int)_direction * Time.deltaTime, 0, 0);
        if ((_distanceToPlayer < hearRange || (_distanceToPlayer < detectRange && _direction == _playerDirection)) &&
            _state != State.Return)
        {
            Chase();
        }
        else if (_state == State.Chase)
        {
            ReverseDirection();
            Patrol();
        }

        if (_state == State.Hold && _distanceToPlayer > detectRange)
        {
            Return();
        }

        if (_state is State.Hold or State.Chase && _distanceToPlayer < _attackRange)
        {
            if (!_canAttack) return;
            StartCoroutine(AttackDelay());
            Attack();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var trigger = other.transform;
        if (trigger == leftPatrolTrigger || trigger == rightPatrolTrigger)
        {
            if (_state == State.Patrol)
            {
                ReverseDirection();
            }

            if (_state == State.Return)
            {
                Patrol();
            }
        }

        if (trigger == leftChaseTrigger || trigger == rightChaseTrigger)
        {
            if (type == Type.Melee)
            {
                Return();
            }

            if (type == Type.Range)
            {
                Hold();
            }
        }
    }

    void Patrol()
    {
        _state = State.Patrol;
        _speed = patrolSpeed;
    }

    void Return()
    {
        ReverseDirection();
        _state = State.Return;
        _speed = patrolSpeed;
    }

    void Hold()
    {
        _state = State.Hold;
        _speed = 0;
    }

    void Chase()
    {
        if (_state == State.Hold && _direction == _playerDirection) return;
        _state = State.Chase;
        _direction = _playerDirection;
        if (_distanceToPlayer < _attackRange)
        {
            _speed = 0;
            return;
        }

        _speed = chaseSpeed;
    }
    
    void Attack()
    {
        var position = transform.position;
        if (type == Type.Melee)
        {
            var playerInRange = Physics2D.OverlapCircle(position, _attackRange, _playerLayer);
            if (playerInRange == null) return;
            _playerHealth.Damage(1);
        }

        if (type == Type.Range)
        {
            Instantiate(projectile, position, transform.rotation);
        }
    }
    IEnumerator AttackDelay()
    {
        _canAttack = false;
        yield return new WaitForSeconds(attackDelay);
        _canAttack = true;
    }
    
    void ReverseDirection()
    {
        _direction = _direction == Direction.Left ? Direction.Right : Direction.Left;
    }
}