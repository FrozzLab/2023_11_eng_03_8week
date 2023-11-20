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
    List<Collider2D> _contacts = new List<Collider2D>();

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        var velocity = _rigidbody.velocity;
        var isMoving = Mathf.Abs(velocity.x) > 0.01f || Mathf.Abs(velocity.y) > 0.01f;
        var isPressingButtons = Input.anyKeyDown;
        if (IsHidden && (isMoving || isPressingButtons))
        {
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemy"), false);
            
            IsHidden = false;
            unHidEvent.Invoke();
        }
        
        if (Input.GetButtonDown("Hide"))
        {
            _playerCollider.GetContacts(_contacts);
            if (!_contacts.Any(collider => collider.gameObject.CompareTag("HidingSpot"))) return;
            
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemy"));
            
            IsHidden = true;
            hidEvent.Invoke();
        }
    }
    
}