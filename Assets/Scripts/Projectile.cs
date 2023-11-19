using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    Transform _player;
    Vector3 _direction;

    void Awake()
    {
        _player = GameObject.FindWithTag("Player").transform;
    }

    void Start()
    {
        _direction = _player.position - transform.position;
        _direction.Normalize();
    }

    void Update()
    {
        transform.Translate(_direction * (speed * Time.deltaTime));
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        other.GetComponent<Health>().Damage(1);
        
        Destroy(gameObject);
    }
}
