using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontalInput = 0f;
    private bool jumping = false;
    private bool jumped = false;

    private CharacterController2D controller2D;
    private Animator animator;

    private void Awake()
    {
        controller2D = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(horizontalInput));
        
        if (Input.GetButtonDown("Jump"))
            jumped = true;
        
        if (Input.GetButton("Jump")){
            jumping = true;
            animator.SetBool("jumping", jumping);
        }
    }
    private void FixedUpdate()
    {
        controller2D.Move(horizontalInput, jumping, jumped);
        jumping = false;
        jumped = false;
    }
}
