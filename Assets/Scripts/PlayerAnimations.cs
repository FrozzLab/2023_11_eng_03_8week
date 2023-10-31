using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void UpdateSpeed(float speed)
    {
        animator.SetFloat("Speed", Mathf.Abs(speed));
    }

    public void Jump()
    {
        animator.SetBool("Jumping", true);
    }

    public void Fall()
    {
        animator.SetBool("Falling", false);
    }

    public void Land()
    {
        animator.SetBool("Jumping", false);
    }

    public void Flip()
    {
        var theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
