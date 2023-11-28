using System.Linq;
using UnityEngine;
using System;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] int dmg = 1;
    [SerializeField] float force = 2f;
    [SerializeField] float range = .8f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] PlayerProjectile rock;

    public void Attack()
    {
        var enemies = Physics2D.OverlapCircleAll(transform.position, range).Where(e => e.CompareTag("Enemy") || e.CompareTag("BreakableWall"));
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<Health>().Damage(dmg);
        }
    }

    public void Throw(float powerPercentage)
    {
        if (powerPercentage < 0 || powerPercentage > 1) throw new ArgumentOutOfRangeException("you can only throw with percentage power between 0% and 100%!!");

        var mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        var target = Camera.main.ScreenToWorldPoint(mousePosition);
        target.z = 0f;
        var direction = target - transform.position;

        rock.Launch(direction, force * powerPercentage);
    }

    void OnDrawGizmos() //show attack range
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
