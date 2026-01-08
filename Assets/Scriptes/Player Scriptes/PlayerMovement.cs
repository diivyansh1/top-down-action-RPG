using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int facingDirection = 1;
    public Rigidbody2D rb; //declaring a rb for physics to work on the object
    public Animator anim;
    public PlayerCombat player_combat;
    private bool isKnockback = false;
    // FixedUpdate is called 50x frame

    void Update()
    {
        if (Input.GetButtonDown("Slash"))
        {
            player_combat.Attack();
        }
    }
    void FixedUpdate()
    {

        if (isKnockback == false)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            anim.SetFloat("horizontal", MathF.Abs(horizontal));
            anim.SetFloat("vertical", MathF.Abs(vertical));

            rb.velocity = new Vector2(horizontal, vertical) * StatsManager.Instance.speed;

            if (horizontal > 0 && transform.localScale.x < 0
            || horizontal < 0 && transform.localScale.x > 0)
            {
                Flip();
            }
        }
    }
    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        //flipping the player in the local scale, the game
    }
    public void Knockback(Transform enemy, float force, float stunTime)
    {
        isKnockback = true;
        Vector2 direction = (transform.position - enemy.position).normalized;
        rb.velocity = direction * force;
        StartCoroutine(KnockBackCounter(stunTime));
    }
    IEnumerator KnockBackCounter(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        rb.velocity = Vector2.zero;
        isKnockback = false;
    }
}