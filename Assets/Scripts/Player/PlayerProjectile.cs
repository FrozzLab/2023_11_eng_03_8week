using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Rock : MonoBehaviour
{
    [SerializeField] int dmg = 1;
    [SerializeField] float range = .8f;
    [SerializeField] LayerMask enemyLayer;
    Rigidbody2D rb;

    [SerializeField] UnityEvent destroyedEvent;
    [SerializeField] UnityEvent createdEvent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        createdEvent.Invoke();
    }

    public void Launch(Vector2 direction, float force)
    {
        rb.velocity = direction * force;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Wall") && !other.CompareTag("Enemy")) return;
        Explode();
    }

    void Explode()
    {
        Debug.Log($"EXPLOSION");
        var enemies = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer).Where(e => e.CompareTag("Enemy"));
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<Health>().Damage(dmg);  //enemy.GetComponent<EnemyAI>().Chase(this.position);
        }
        destroyedEvent.Invoke();
        Destroy(gameObject);
    }
    
    void OnDrawGizmos() //show explode range
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
