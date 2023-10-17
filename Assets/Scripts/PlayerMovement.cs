using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontalInput = 0f;
    private bool jumping = false;
    private bool jumped = false;

    private CharacterController2D controller2D;

    private void Awake()
    {
        controller2D = GetComponent<CharacterController2D>();
    }
    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
            jumped = true;
        
        if (Input.GetButton("Jump"))
            jumping = true;
    }
    private void FixedUpdate()
    {
        controller2D.Move(horizontalInput, jumping, jumped);
        jumping = false;
        jumped = false;
    }
}
