using System.Collections;
using UnityEngine;

public class WeakPlatform : MonoBehaviour
{
	[SerializeField] float disappearDelay = 1.5f;
	[SerializeField] float reappearDelay = 3.5f;

	Collider2D _collider;
	SpriteRenderer _spriteRenderer;

	void Awake()
	{
		_collider = GetComponent<Collider2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
		{
			StartCoroutine(Disappear());
		}
	}

	IEnumerator Disappear()
	{
		yield return new WaitForSeconds(disappearDelay);
			
		_collider.enabled = false;
		_spriteRenderer.enabled = false;

		yield return new WaitForSeconds(reappearDelay);

		_collider.enabled = true;
		_spriteRenderer.enabled = true;
	}
}
