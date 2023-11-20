using System;
using System.Collections;
using UnityEngine;

public class PlayerWeaponAnimations : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Focus(float focusTime)
    {
		RestartsAllTriggers();
        animator.SetTrigger("startedFocusing");
    }

    public void Charge(float chargeTime)
    {
		RestartsAllTriggers();
        animator.SetTrigger("startedCharging");
    }

    public void Launch()
    {
		RestartsAllTriggers();
        animator.SetTrigger("launched");
    }

	public void StopFocus()
    {
		RestartsAllTriggers();
        animator.SetTrigger("focusingInterrupted");
    }

	void RestartsAllTriggers()
    {
        animator.ResetTrigger("startedFocusing");
        animator.ResetTrigger("startedCharging");
        animator.ResetTrigger("launched");
        animator.ResetTrigger("focusingInterrupted");
    }
}
