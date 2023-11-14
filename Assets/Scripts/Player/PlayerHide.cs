using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHide : MonoBehaviour
{
    [SerializeField] UnityEvent hidEvent;
    public bool IsHidden { get; private set; }
    PlayerMovement _playerMovement;
    CircleCollider2D _playerCollider;

    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Hide") && isOnSpot())
        {
            IsHidden = !IsHidden;
            _playerMovement.hasControl = !IsHidden;
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
