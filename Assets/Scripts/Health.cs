using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int max = 3;
    private int _current;
    
    private void Awake()
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
            if (value < 0 || value > max) return;
            _current = value;
            // Do we update the HUD here or does the HUD listen to values?
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

    private void Die()
    {
        // Play death animation and wait for it to be over
        
        if (gameObject.tag == "Player")
        {
            // Death screen? Respawn at checkpoint? Just reset stats and location? Destroy and recreate?
        }
        else
        {
            // Fire some event to notify other elements that an enemy died?
            // Or just increase some sort of counter manually from here?
            Destroy(gameObject);
        }
    }
}
