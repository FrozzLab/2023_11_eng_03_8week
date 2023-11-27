using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }

    public void NightGuardAttack()
    {
	    
    }

    public void NightSoldierAttack()
    {
	    
    }

    public void NightWardenAttack()
    {
	    
    }
}
