using UnityEngine;

public class FallingSpike : MonoBehaviour
{
	[SerializeField] int damage = 1;
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
		{
			var otherObject = other.gameObject;
			var otherHealth = otherObject.GetComponent<Health>();
			
			otherHealth.Damage(damage);
		}
		
		Destroy(gameObject);
	}
}
