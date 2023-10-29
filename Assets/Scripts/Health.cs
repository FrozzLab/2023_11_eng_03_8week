using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int max = 3;
    int _current;
    Animator _animator;
    static readonly int AnimatorHealth = Animator.StringToHash("health");

    void Awake()
    {
        _current = max;
        _animator = GetComponent<Animator>();
    }

    public int Max
    {
        get => max;
        set
        {
            if (value <= 0) return;
            max = value;
        }
    }

    public int Current
    {
        get => _current;
        set
        {
            if (value < 0 || value > max) return;
            _current = value;
            _animator.SetInteger(AnimatorHealth, _current);
        }
    }

    public void Heal(int amount)
    {
        Current += amount;
    }

    public void Damage(int amount)
    {
        Current -= amount;

        if (_current <= 0)
        {
            Die();
        }
    }

    public void Increase(int amount)
    {
        Max += amount;
    }

    public void Decrease(int amount)
    {
        Max -= amount;
    }

    void Die()
    {
        if (CompareTag("Player"))
        {
        }
        else
        {
            Destroy(gameObject);
        }
    }
}