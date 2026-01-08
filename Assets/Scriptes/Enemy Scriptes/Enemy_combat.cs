using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_combat : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage = 1;
    public LayerMask playerLayer;
    public Transform attackPoint;
    public float weaponRange;
    public float stunTime;
    public float force;
    private void Attack()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, playerLayer);
        if (hit.Length > 0)
        {
            hit[0].GetComponent<Health>().ChangeHealth(-damage);
            hit[0].GetComponent<PlayerMovement>().Knockback(transform, force, stunTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, weaponRange);
    }
}
