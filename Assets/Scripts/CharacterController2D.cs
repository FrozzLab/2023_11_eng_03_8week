using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float speed = 400f;
    public float jumpForse = 300f;
    public float jumpingForse = 20f;
    public float jumpingAirTime = 10;
    [Range(0f, .3f)] public float movementSmoothing = 0.05f;
    public bool hasControl = true;

    private float jumpingAirTimeLeft;
    private bool grounded;
    private Vector2 velocity = Vector2.zero;
    
    private Rigidbody2D rigidbody2d;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        grounded = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        grounded = false;
        jumpingAirTimeLeft = jumpingAirTime;
    }
    public void Move(float move, bool jumping, bool jumped)
    {
        if (hasControl)
        {
            var velocity = new Vector2(move * speed * Time.fixedDeltaTime, 0f);
            rigidbody2d.velocity = new Vector2(velocity.x, rigidbody2d.velocity.y);

            if (grounded && jumped)
                rigidbody2d.AddForce(new Vector2(0f, jumpForse));
            
            if (jumping && jumpingAirTimeLeft > 0 && rigidbody2d.velocity.y > .1f)
            {
                rigidbody2d.AddForce(new Vector2(0f, jumpingForse));
                jumpingAirTimeLeft -= Time.fixedDeltaTime;
            }
        }
    }
}
