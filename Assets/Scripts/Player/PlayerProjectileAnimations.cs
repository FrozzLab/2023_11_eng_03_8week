using UnityEngine;

public class PlayerProjectileAnimations : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Launch()
    {
		RestartsAllTriggers();
        animator.SetTrigger("launched");
    }

    public void Explode()
    {
		RestartsAllTriggers();
        animator.SetTrigger("exploded");
    }

    public void Disappear()
    {
		RestartsAllTriggers();
        animator.SetTrigger("returnedToPlayer");
    }

	public void RestartsAllTriggers()
    {
        animator.ResetTrigger("launched");
        animator.ResetTrigger("exploded");
        animator.ResetTrigger("returnedToPlayer");
    }
}
