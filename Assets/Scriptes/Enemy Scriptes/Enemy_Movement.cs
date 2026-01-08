using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.TextCore;

public class Enemy_Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float attackRange;
    public Transform detectionPoint;
    public float detectionArea = 5;
    public LayerMask playerLayer;
    public float cooldown;
    private float cooldowntimer;

    private int facingDirection = -1;
    private Rigidbody2D rb;
    private Transform player;
    private Animator anim1;
    private EnemyState enemyState;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim1 = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState != EnemyState.Knockbacking)
        {
            CheckForPlayer();
            if (cooldown > 0)
            {
                cooldowntimer -= Time.deltaTime;
            }
            CheckForPlayer();
            if (enemyState == EnemyState.Chasing)
            {
                Chase();
            }
            if (enemyState == EnemyState.Attacking)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, detectionArea, playerLayer);
        if (hits.Length > 0)
        {
            player = hits[0].transform;
            if (Vector2.Distance(transform.position, player.position) <= attackRange && cooldowntimer <= 0)
            {
                cooldowntimer = cooldown;
                ChangeState(EnemyState.Attacking);
            }
            else if (Vector2.Distance(transform.position, player.position) > attackRange && enemyState != EnemyState.Attacking)
            {
                ChangeState(EnemyState.Chasing);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
    }
    private void Chase()
    {
        if (player.position.x > transform.position.x && facingDirection == -1 || player.position.x < transform.position.x && facingDirection == 1)
        {
            Flip();
        }
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }
    private void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    public void ChangeState(EnemyState newState)
    {
        //To check what the current state is and exit it
        if (enemyState == EnemyState.Idle)
            anim1.SetBool("isIdle", false);
        else if (enemyState == EnemyState.Chasing)
            anim1.SetBool("isChasing", false);
        else if (enemyState == EnemyState.Attacking)
            anim1.SetBool("isAttacking", false);

        //To change to the other state
        enemyState = newState;
        if (enemyState == EnemyState.Idle)
            anim1.SetBool("isIdle", true);
        else if (enemyState == EnemyState.Chasing)
            anim1.SetBool("isChasing", true);
        else if (enemyState == EnemyState.Attacking)
            anim1.SetBool("isAttacking", true);
        
    }
}
public enum EnemyState
{
    Idle,
    Chasing,
    Attacking,
    Knockbacking,
}