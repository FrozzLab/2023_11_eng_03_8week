using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController2D controller2D;
    public float speed = 1f;
    float horizontalInput = 0f;
    bool jumping = false;
    bool startedJump = false;

    private void Awake()
    {
        controller2D = GetComponent<CharacterController2D>();
    }
    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal") * speed;
        if (Input.GetButtonDown("Jump"))
            startedJump = true;
        
        if (Input.GetButton("Jump"))
            jumping = true;
    }
    private void FixedUpdate()
    {
        //move our player
        controller2D.Move(horizontalInput * Time.fixedDeltaTime, jumping, startedJump);
        jumping = false;
        startedJump = false;
    }
}
