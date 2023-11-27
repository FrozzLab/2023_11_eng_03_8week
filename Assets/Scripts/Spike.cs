using UnityEngine;

public class Spike : MonoBehaviour
{
	[SerializeField] int damage = 1;
	[SerializeField] float knockBackForce = 15f;
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
		{
			var otherObject = other.gameObject;
			var otherHealth = otherObject.GetComponent<Health>();
			var otherRigidBody = otherObject.GetComponent<Rigidbody2D>();
			
			otherHealth.Damage(damage);
			otherRigidBody.AddForce(new Vector2(0,  knockBackForce), ForceMode2D.Impulse);
		}
	}
}
