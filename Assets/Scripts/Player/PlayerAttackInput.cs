using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttackInput : MonoBehaviour
{
    public bool hasWeapon = true;

    [SerializeField] float cooldown = 1.5f;
    float _cooldown = 0f;
    [SerializeField] float focusMax = .3f;
    float _focus = 0f;
    bool _isFocused = false;
    [SerializeField] float chargeMax = 2f;
    float _charge = 0f;
    bool _isCharged = false;

    PlayerAttack playerAttack;

    [SerializeField] UnityEvent<float> startedFocusingEvent;
    [SerializeField] UnityEvent stoppedFocusingEvent;
    [SerializeField] UnityEvent<float> startedChargingEvent;
    [SerializeField] UnityEvent chargedEvent;
    [SerializeField] UnityEvent thrownEvent;
    [SerializeField] UnityEvent attackedEvent;

    private void Awake()
    {
        playerAttack = GetComponent<PlayerAttack>();
    }

    void Update()
    {
        if(!hasWeapon) return;

        _cooldown = Mathf.Max(0f, _cooldown - Time.deltaTime);
        if(_cooldown > 0f) return;

		if(Input.GetButtonDown("Attack"))
        {
            startedFocusingEvent.Invoke(focusMax);
        }

        if(Input.GetButton("Attack"))
        {
            Focus();
        }

        if(Input.GetButtonUp("Attack"))
        {
            Attack();
        }
    }

    private void Focus()
    {
        if(_focus < focusMax)
        {
            _focus = Mathf.Min(focusMax, _focus + Time.deltaTime);
        }
        else if(!_isFocused)
        {
            _isFocused = true;
			startedChargingEvent.Invoke(chargeMax);
        }
        else if(_charge < chargeMax)
        {
            _charge = Mathf.Min(chargeMax, _charge + Time.deltaTime);
        }
		else if(!_isCharged)
		{
			_isCharged = true;
            chargedEvent.Invoke();
		}
    }

    private void Attack()
    {
        if(_isFocused)
        {
            playerAttack.Throw(_charge / chargeMax);
            thrownEvent.Invoke();
        }
        else
        {
            playerAttack.Attack();
            attackedEvent.Invoke();
			stoppedFocusingEvent.Invoke();
        	_cooldown = cooldown;
        }
        _focus = 0f;
        _isFocused = false;
        _charge = 0f;
		_isCharged = false;
    }

	public void OnDeath()
	{
		hasWeapon = false;
	}
}
