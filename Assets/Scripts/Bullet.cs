using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [HideInInspector] public EnemyAI.Direction direction;
    void Update()
    {
        // TODO: Actual aiming and stuff
        transform.Translate(speed * (int)direction * Time.deltaTime, 0, 0);
        // TODO: Destroy when it leaves the camera vision
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        other.GetComponent<Health>().Damage(1);
        
        Destroy(gameObject);
    }
}
