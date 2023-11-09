using System;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] float speed = 400f;
    [SerializeField] float jumpForce = 300f;
    [SerializeField] float jumpingForce = 20f;
    [SerializeField] float airTime = 10;
    float airTimeLeft;

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
    }

    private void FixedUpdate()
    {
        if (!isFalling && rigidbody2d.velocity.y < 0) Fall();
        debugVelocity = rigidbody2d.velocity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Wall")) return;
        Land();
    }

    public void Move(float move)
    {
        var velocity = move * speed * Time.fixedDeltaTime;
        rigidbody2d.velocity = new Vector2(velocity, rigidbody2d.velocity.y);
        Console.WriteLine("move: " + move);
    }

    public void Jump()
    {
        if (!grounded) return;

        grounded = false;
        rigidbody2d.AddForce(new Vector2(0f, jumpForce));
        jumpedEvent.Invoke();
    }

    public void Flight()
    {
        if (grounded) return;

        if (airTimeLeft > 0)
        {
            rigidbody2d.AddForce(new Vector2(0f, jumpingForce));
            airTimeLeft -= Time.fixedDeltaTime;
        }
    }

    private void Land()
    {
        grounded = true;
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
