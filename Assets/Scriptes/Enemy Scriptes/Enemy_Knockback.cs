using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Knockback : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D rb;
    public Enemy_Movement enemy_Movement;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy_Movement = GetComponent<Enemy_Movement>();
    }
    public void Knockback(Transform player, float force, float knockbackTime, float stuntime)
    {
        enemy_Movement.ChangeState(EnemyState.Knockbacking);
        StartCoroutine(StunTimer(knockbackTime, stuntime));
        Vector2 direction = (transform.position - player.position).normalized;
        rb.velocity = direction * force;
    }

    IEnumerator StunTimer(float knockbackTime, float stunTime)
    {
        yield return new WaitForSeconds(knockbackTime);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);
        enemy_Movement.ChangeState(EnemyState.Idle);
    }
}
