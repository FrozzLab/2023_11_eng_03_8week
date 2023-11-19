using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHide : MonoBehaviour
{
    [SerializeField] UnityEvent hidEvent;
    [SerializeField] UnityEvent unHidEvent;
    public bool IsHidden { get; private set; }
    Rigidbody2D _rigidbody;
    CircleCollider2D _playerCollider;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        var velocity = _rigidbody.velocity;
        var isMoving = velocity.x > 0.01f || velocity.y > 0.01f;
        var isPressingButtons = Input.anyKeyDown;
        if (isMoving || isPressingButtons)
        {
            IsHidden = false;
            unHidEvent.Invoke();
        }
        
        if (Input.GetButtonDown("Hide") && isOnSpot())
        {
            IsHidden = true;
            hidEvent.Invoke();
        }
    }

    bool isOnSpot()
    {
        var colliders = new List<Collider2D>();
        _playerCollider.GetContacts(colliders);
        return colliders.Any(collider => collider.gameObject.CompareTag("HidingSpot"));
    }
}