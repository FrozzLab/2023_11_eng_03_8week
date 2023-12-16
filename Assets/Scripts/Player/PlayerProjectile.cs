using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] float returnSpeed = 2f;
    [SerializeField] float absorbDistance = 0.4f; //from how far a player can absorb a projectile to ble able to use it again
    [SerializeField] LayerMask enemyLayer;
    Rigidbody2D rb;
    new CircleCollider2D collider;
    [SerializeField] PlayerAttackInput inputScript;
    [SerializeField] Transform player;

    [SerializeField] UnityEvent absorbedEvent;
    [SerializeField] UnityEvent explodedEvent;
    [SerializeField] UnityEvent launchedEvent;

	public Vector2 debugVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
		rb.freezeRotation = true;
        collider = GetComponent<CircleCollider2D>();
		DisableMe(); 
    }

	private void FixedUpdate()
    {
        debugVelocity = rb.velocity;
    }

    public void Launch(Vector2 direction, float force)
    {
		EnableMe();
        rb.velocity = direction * force;
		launchedEvent.Invoke();
    }

	private void OnCollisionEnter2D(Collision2D other) {
		if (!other.gameObject.CompareTag("Wall") && !other.gameObject.CompareTag("Enemy") && !other.gameObject.CompareTag("BreakableWall")) return;
		if (other.gameObject.CompareTag("BreakableWall"))
		{
			other.gameObject.GetComponent<Health>().Damage(1);
		}
		Debug.Log($"EXPLOSION: collided with {other.gameObject.tag}");
        Explode();
	}

    void Explode()
    {
		var obstacles = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(var o in obstacles)
		{
			if(o.TryGetComponent(out EnemyAI enemy)) 
				enemy.NoticeDistraction(transform.position);
		}

        explodedEvent.Invoke();
		StartCoroutine(ReturnToPlayer());
    }

	IEnumerator ReturnToPlayer()
	{
		//wait for explosion
		rb.isKinematic = true; 
		rb.velocity = Vector2.zero;
		collider.enabled = false;
		yield return new WaitForSeconds(0.3f);

		//return to player
		var direction = player.position - transform.position;
		
		while(direction.magnitude > absorbDistance)
		{
			rb.velocity = (Vector2)direction.normalized * returnSpeed;
			direction = player.position - transform.position; 
			yield return new WaitForSeconds(0.1f);
		}

		absorbedEvent.Invoke();
		DisableMe();
	}

	void EnableMe()
	{
		rb.isKinematic = false; 
		collider.enabled = true;
		transform.position = player.position;
		inputScript.hasWeapon = false;
	}

	void DisableMe()
	{
		collider.enabled = false;
		rb.isKinematic = true; 
		rb.velocity = Vector2.zero;
		transform.position = player.position;
		inputScript.hasWeapon = true;
	}
}
