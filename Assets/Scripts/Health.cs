using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] int max = 3;
    int _current;
    [SerializeField] UnityEvent diedEvent;
    public bool IsDead { get; private set; }

    void Start()
    {
        _current = max;
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
            if (value > max) return;
            if (value < 0)
            {
	            _current = 0;
	            return;
            }
            _current = value;
        }
    }

    public void Heal(int amount)
    {
        Current += amount;
    }

    public void Damage(int amount)
    {
		if(_current <= 0) return;
		
        Current -= amount;
        Debug.Log($"{gameObject.name} took {amount} dmg. HP left {Current}");

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
        IsDead = true;
        diedEvent.Invoke();
        if (CompareTag("Player"))
        {
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
