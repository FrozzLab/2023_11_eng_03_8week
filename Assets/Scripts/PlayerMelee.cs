using System.Linq;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    [SerializeField] int dmg = 1;
    [SerializeField] float range = .8f;
    [SerializeField] float cooldown = 1.5f;
    float _cooldown = 0f;
    bool _hasWeapon = true;
    [SerializeField] private LayerMask enemyLayer;


    void Update()
    {
        _cooldown -= Time.deltaTime;

        if (Input.GetButtonDown("Attack")) Attack();
    }

    void Attack()
    {
        if (!_hasWeapon || _cooldown > 0f) return;

        _cooldown = cooldown;

        var enemies = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer).Where(e => e.CompareTag("Enemy"));
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<Health>().Damage(dmg);
        }
    }

    void OnDrawGizmos() //show attack range
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
