using System.Collections;
using DefaultNamespace;
using UnityEngine;

public class WeakPlatform : MonoBehaviour
{
	[SerializeField] float disappearDelay = 1f;
	[SerializeField] float reappearDelay = 3.5f;

	public BreakableBranchAnimations animations;

	private Collider2D _collider;
	private SpriteRenderer _spriteRenderer;

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
		animations.FadeOutStart();
		
		yield return new WaitForSeconds(disappearDelay);
			
		animations.FadeOutEnd();
		
		_collider.enabled = false;
		_spriteRenderer.enabled = false;
		
		yield return new WaitForSeconds(reappearDelay - 1);
		
		animations.FadeInStart();

		yield return new WaitForSeconds(1);
		
		animations.FadeInEnd();

		_collider.enabled = true;
		_spriteRenderer.enabled = true;
	}
}
