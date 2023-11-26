using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    Vector2 _playerPosition;
    Health _playerHealth;
    Vector2 _position;
    LayerMask _playerLayer;
    LayerMask _groundLayer;
    LayerMask _enemyLayer;
    public int Damage { get; set; }

    void Awake()
    {
	    var playerBody = GameObject.Find("Body");
	    _playerPosition = playerBody.transform.position;
	    _playerHealth = playerBody.GetComponent<Health>();
	    _playerLayer = playerBody.layer;
	    _groundLayer = LayerMask.GetMask("Ground");
	    _enemyLayer = LayerMask.GetMask("Enemy");
	    _position = transform.position;
    }

    void FixedUpdate()
    {
	    var direction = _playerPosition - _position;
	    
	    direction.Normalize();
	    
	    transform.Translate(direction * (speed * Time.fixedDeltaTime));
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
	    if (other.gameObject.CompareTag("Player"))
	    {
		    _playerHealth.Damage(Damage);
	    }
    }
}
