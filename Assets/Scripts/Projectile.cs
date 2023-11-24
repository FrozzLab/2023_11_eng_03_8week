using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    Transform _target;
    Vector2 _direction;
    int _damage;
    
    public void Init(Transform target, int damage)
    {
        _target = target;
        _damage = damage;
    }

    void Start()
    {
        _direction = _target.position - transform.position;
        _direction.Normalize();
    }
    
    void FixedUpdate()
    {
        transform.Translate(_direction * (speed * Time.fixedDeltaTime));
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != _target.gameObject.layer) return;
        
        other.GetComponent<Health>().Damage(_damage);
        
        Destroy(gameObject);
    }
}
