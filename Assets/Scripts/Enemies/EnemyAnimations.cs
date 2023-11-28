using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    Animator _animator;
    bool _isDead;
    static readonly int IsDead = Animator.StringToHash("IsDead");
    static readonly int GotHit = Animator.StringToHash("GotHit");
    static readonly int Attacked = Animator.StringToHash("Attacked");

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

    public void Attack()
    {
	    _animator.SetTrigger(Attacked);
    }

    public void GetHit()
    {
	    _animator.SetTrigger(GotHit);
    }
}
