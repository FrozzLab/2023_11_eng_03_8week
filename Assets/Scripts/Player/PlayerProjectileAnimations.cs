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
        //animator.SetBool("todo", false);
    }

    public void Charge(float chargePercentage)
    {
        //animator.SetBool("todo", false);
    }

    public void Launch()
    {
        //animator.SetBool("todo", false);
    }

    public void Explode()
    {
        //animator.SetBool("todo", false);
    }

    public void Disappear()
    {
        //animator.SetBool("todo", false);
    }
}
