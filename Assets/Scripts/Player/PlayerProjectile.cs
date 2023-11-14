using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] float returnSpeed = 2f;
    [SerializeField] float returnSmoothness = 0.1f; //delay between updating direction of projectile
    [SerializeField] float absorbDistance = 0.4f; //from how far a player can absorb a projectile to ble able to use it again
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Transform player;
    Rigidbody2D rb;
    new CircleCollider2D collider;
    [SerializeField] PlayerAttackInput inputScript;
    bool isReady;

    [SerializeField] UnityEvent disabledEvent;
    [SerializeField] UnityEvent explodedEvent;
    [SerializeField] UnityEvent lunchedEvent;

	public Vector2 debugVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
		lunchedEvent.Invoke();
    }

	private void OnCollisionEnter2D(Collision2D other) {
		if (!isReady && !other.gameObject.CompareTag("Wall") && !other.gameObject.CompareTag("Enemy")) return;
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
        isReady = false;
		StartCoroutine(ReturnToPlayer());
    }

	IEnumerator ReturnToPlayer()
	{
		collider.enabled = false;
		var direction = player.position - transform.position;
		
		while(direction.magnitude > absorbDistance)
		{
			rb.velocity = direction.normalized * returnSpeed;
			direction = player.position - transform.position;
			yield return new WaitForSeconds(returnSmoothness);
		}
		DisableMe();
		disabledEvent.Invoke();
	}

	void EnableMe()
	{
		rb.isKinematic = false; 
		isReady = true;
		this.transform.position = player.position;
		collider.enabled = true;
		inputScript.hasWeapon = false;
	}

	void DisableMe()
	{
		rb.isKinematic = true; 
		collider.enabled = false;
		rb.velocity = Vector2.zero;
		inputScript.hasWeapon = true;
	}
}
