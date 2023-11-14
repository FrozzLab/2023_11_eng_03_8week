using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public bool hasControl = true;

    float horizontalInput = 0f;
    bool jumped = false;
    bool jumping = false;
    bool isLookingRight = true;

    CharacterController2D controller2D;

    [SerializeField] UnityEvent<float> speedChangedEvent;
    [SerializeField] UnityEvent turnedEvent;

    private void Awake()
    {
        controller2D = GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        if(!hasControl) return;

        horizontalInput = Input.GetAxisRaw("Horizontal");
        speedChangedEvent.Invoke(horizontalInput);

        if (Input.GetButtonDown("Jump"))
        {
            jumped = true;
        }

        if(Input.GetButton("Jump"))
        {
            jumping = true;
        }

        if((isLookingRight && horizontalInput < 0f) || (!isLookingRight && horizontalInput > 0f))
        {
            isLookingRight = !isLookingRight;
            turnedEvent.Invoke();
        }
    }

    private void FixedUpdate()
    {
        controller2D.Move(horizontalInput);
        if(jumped) controller2D.Jump();
        if(jumping) controller2D.Flight();

        jumped = false;
        jumping = false;
    }
}
