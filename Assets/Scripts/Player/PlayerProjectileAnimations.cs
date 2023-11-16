using UnityEngine;

public class PlayerProjectileAnimations : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Focus(float focusPercentage)
    {
		RestartsAllTriggers();
        animator.SetTrigger("startedFocusing");
    }

    public void Charge(float chargePercentage)
    {
		RestartsAllTriggers();
        animator.SetTrigger("startedCharging");
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

	public void StopFocus()
    {
		RestartsAllTriggers();
        animator.SetTrigger("focusingInterrupted");
    }

	public void RestartsAllTriggers()
    {
        animator.ResetTrigger("startedFocusing");
        animator.ResetTrigger("startedCharging");
        animator.ResetTrigger("launched");
        animator.ResetTrigger("exploded");
        animator.ResetTrigger("returnedToPlayer");
        animator.ResetTrigger("focusingInterrupted");
    }
}
