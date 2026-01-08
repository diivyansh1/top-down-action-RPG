using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public Animator anim2;
    public float cooldown;
    public Transform attackPoint;
    public LayerMask enemy_layer;
    public int damage = 1;
    private float timer;
    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Slash.performed += OnSlashPerformed;
    }

    private void OnDisable()
    {
        inputActions.Player.Slash.performed -= OnSlashPerformed;
        inputActions.Player.Disable();
    }

    private void OnSlashPerformed(InputAction.CallbackContext context)
    {
        Attack();
    }
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
    public void Attack()
    {
        if (timer <= 0)
        {
            anim2.SetBool("isAttacking", true);
            timer = cooldown;
        }
    }

    public void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, StatsManager.Instance.attackRange, enemy_layer);
        foreach (Collider2D enemy in enemies)
        {
            if (enemy.isTrigger) continue; //so it does not deal damage when inside detection range
        }
        if (enemies.Length > 0)
        {
            enemies[0].GetComponent<Enemy_health>().ChangeHealth(-damage);
            enemies[0].GetComponent<Enemy_Knockback>().Knockback(transform, StatsManager.Instance.Knockbackforce, StatsManager.Instance.stunTime, StatsManager.Instance.knockbackTime);
        }
    }
    public void FinishAttack()
    {
        anim2.SetBool("isAttacking", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, StatsManager.Instance.attackRange);
    }

}
