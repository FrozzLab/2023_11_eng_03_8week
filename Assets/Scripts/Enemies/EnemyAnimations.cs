using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    Animator _animator;
    static readonly int IsDead = Animator.StringToHash("IsDead");
    static readonly int GotHit = Animator.StringToHash("GotHit");

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }

    public void Die()
    {
	    _animator.SetBool(IsDead, true);
    }

    public void GetHit()
    {
	    _animator.SetTrigger(GotHit);
    }
}
