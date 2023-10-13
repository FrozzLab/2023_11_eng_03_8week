using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] float jumpForse = 400f;
    [SerializeField] float jumpingForse = 10f;
    [SerializeField] int jumpBoostMax = 10;
    int jumpBoost;
    [Range(0f, .3f)][SerializeField] float movementSmoothing = 1f;
    [SerializeField] bool hasControl = true;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Transform groundCheck;

    bool grounded;
    Rigidbody2D rigidbody2;
    Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            grounded = true;
            jumpBoost = jumpBoostMax;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
            grounded = false;
        
    }
    public void Move(float move, bool jumping, bool startJump)
    {
        if (hasControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, rigidbody2.velocity.y);
            // And then smoothing it out and applying it to the character
            rigidbody2.velocity = Vector3.SmoothDamp(rigidbody2.velocity, targetVelocity, ref velocity, movementSmoothing);

            if (grounded && startJump)
                rigidbody2.AddForce(new Vector2(0f, jumpForse));
            if (jumping && jumpBoost > 0)
            {
                rigidbody2.AddForce(new Vector2(0f, jumpingForse));
                jumpBoost--;
            }
        }
    }
}
