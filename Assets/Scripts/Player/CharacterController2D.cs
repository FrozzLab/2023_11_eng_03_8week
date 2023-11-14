using System;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] float speed = 400f;
    [SerializeField] float jumpForce = 300f;
    [SerializeField] float jump2Force = 300f;
    [SerializeField] float jumpingForce = 20f;
    [SerializeField] float airTime = 10;
    float airTimeLeft;
    bool doubleJumped = false;

    //[SerializeField] [Range(0f, .3f)] float movementSmoothing = 0.05f;
    
    bool grounded;
    bool isFalling;

    [SerializeField] UnityEvent jumpedEvent;
    [SerializeField] UnityEvent landedEvent;
    [SerializeField] UnityEvent startedFallingEvent;

    Rigidbody2D rigidbody2d;

    public Vector2 debugVelocity;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
		airTimeLeft = airTime;
    }

    private void FixedUpdate()
    {
        if(!isFalling && !grounded && rigidbody2d.velocity.y < 0.1) Fall();
        debugVelocity = rigidbody2d.velocity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Wall")) return;
		if(isFalling)
        	Land();
    }

	private void OnTriggerExit2D(Collider2D other) 
	{
		if (!other.CompareTag("Wall")) return;
		grounded = false;
	}

    public void Move(float move)
    {
        var velocity = move * speed * Time.fixedDeltaTime;
        rigidbody2d.velocity = new Vector2(velocity, rigidbody2d.velocity.y);
    }

    public void Jump()
    {
        if(!grounded && doubleJumped) return;
        if(!grounded) doubleJumped = true;

		rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, 0f);
        rigidbody2d.AddForce(new Vector2(0f, doubleJumped ? jump2Force : jumpForce));
        jumpedEvent.Invoke();
    }

    public void Flight()
    {
        if(grounded) return;

        if(airTimeLeft > 0)
        {
            rigidbody2d.AddForce(new Vector2(0f, jumpingForce));
            airTimeLeft -= Time.fixedDeltaTime;
        }
    }

    private void Land()
    {
        grounded = true;
		doubleJumped = false;
        isFalling = false;
        airTimeLeft = airTime;
        landedEvent.Invoke();
    }

    private void Fall()
    {
        isFalling = true;
        startedFallingEvent.Invoke();
    }
}
