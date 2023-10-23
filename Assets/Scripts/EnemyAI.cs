using System;
using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform player;
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
    [SerializeField] GameObject bullet;
    Bullet _bullet;
    [SerializeField] float shootDelay = 1;
    bool _canShoot = true;

    void Awake()
    {
        _speed = patrolSpeed;
        _attackRange = type == Type.Melee ? 0.5f : 3.5f;
        _bullet = bullet.GetComponent<Bullet>();
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
        var playerPosition = player.position;
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

        if (_state is State.Hold or State.Chase && type == Type.Range)
        {
            if (!_canShoot) return;
            StartCoroutine(ShootDelay());
            Shoot();
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

    void Shoot()
    {
        var enemy = transform;
        _bullet.direction = _direction;
        Instantiate(bullet, enemy.position, enemy.rotation);
    }

    void Hit()
    {
        throw new NotImplementedException();
    }

    IEnumerator ShootDelay()
    {
        _canShoot = false;
        yield return new WaitForSeconds(shootDelay);
        _canShoot = true;
    }
    
    void ReverseDirection()
    {
        _direction = _direction == Direction.Left ? Direction.Right : Direction.Left;
    }
}