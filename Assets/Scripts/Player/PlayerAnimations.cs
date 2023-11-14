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
        animator.SetFloat("speed", Mathf.Abs(speed));
    }

    public void Jump()
    {
        animator.SetBool("jumping", true);
    }

    public void Fall()
    {
        animator.SetBool("falling", false);
    }

    public void Land()
    {
        animator.SetBool("jumping", false);
    }

    public void Flip()
    {
        var theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Die()
    {
        animator.SetBool("dead", true);
    }

    public void Hide()
    {
        animator.SetBool("hidden", true);
    }
}
