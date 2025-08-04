using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackOrigin;
    public float attackRadius = 1f;
    public LayerMask enemyMask;
    public float wait = 0.3f;

    public float cooldownTime = 0.5f;
    private float cooldownTimer = 0f;

    public int Damage = 1;

    public Animator anim;
    public bool idle = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackOrigin.position, attackRadius);
    }

    public void Attack()
    {
        if (cooldownTimer > 0) return;

        Debug.Log("Serang!");
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(attackOrigin.position, attackRadius, enemyMask);

        foreach (var enemy in enemiesInRange)
        {
            // Coba cari EnemyHealth
            var enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(Damage);
            }
            else
            {
                // Coba cari BossHealth
                var bossHealth = enemy.GetComponent<BossHealth>();
                if (bossHealth != null)
                {
                    bossHealth.TakeDamage(Damage);
                }
            }
        }

        cooldownTimer = cooldownTime;
    }

    public bool CanAttack()
    {
        return cooldownTimer <= 0f && IsIdle();
    }

    public bool IsIdle()
    {
        return anim.GetBool("idle");
    }
}
