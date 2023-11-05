using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAtackInput : MonoBehaviour
{
    public bool hasWeapon = true;

    [SerializeField] float cooldown = 1.5f;
    float _cooldown = 0f;
    [SerializeField] float focusMax = .3f;
    float _focus = 0f;
    bool _isFocused = false;
    [SerializeField] float chargeMax = 2f;
    float _charge = 0f;

    PlayerAtack playerAtack;

    [SerializeField] UnityEvent<float> startedFocusingEvent;
    [SerializeField] UnityEvent stoppedFocusingEvent;
    [SerializeField] UnityEvent focusedEvent;
    [SerializeField] UnityEvent thrownEvent;
    [SerializeField] UnityEvent attackedEvent;

    private void Awake()
    {
        playerAtack = GetComponent<PlayerAtack>();
    }

    void Update()
    {
        if (!hasWeapon) return;

        _cooldown = Mathf.Max(0f, _cooldown - Time.deltaTime);
        if (_cooldown > 0f) return;

        if (Input.GetButton("Attack"))
        {
            Focus();
            startedFocusingEvent.Invoke(_focus);
        }

        if (Input.GetButtonUp("Attack"))
        {
            Attack();
            stoppedFocusingEvent.Invoke();
        }
    }

    private void Focus()
    {
        if (_focus < focusMax)
        {
            _focus = Mathf.Min(focusMax, _focus + Time.deltaTime);
        }
        else if (!_isFocused)
        {
            _isFocused = true;
            focusedEvent.Invoke();
        }
        else
        {
            _charge = Mathf.Min(chargeMax, _charge + Time.deltaTime);
        }
    }

    private void Attack()
    {
        if (_isFocused)
        {
            playerAtack.Throw(_charge / chargeMax);
            thrownEvent.Invoke();
        }
        else
        {
            playerAtack.Attack();
            attackedEvent.Invoke();
        }
        _focus = 0f;
        _isFocused = false;
        _charge = 0f;
        _cooldown = cooldown;
    }
}
