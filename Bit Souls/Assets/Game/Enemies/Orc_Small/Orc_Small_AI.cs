using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Small_AI : MonoBehaviour
{
    public Rigidbody2D rb;
    private Transform player;
    private Orc_Small_Anim anim;

    public enum EnemyState { Idle, Aggro, Attack, Tired}
    public EnemyState enemyState;

    public float moveSpeed;
    public float aggroRange;

    private bool attackCooldown = false;
    public float cooldownTimer;
    public float cooldownDur;
    private bool isAttacking = false;
    private float attackTimer;
    public float attackDur;
    private Vector2 attackDir;
    public float animAttackDir;
    public float offsetMultiplier;
    public float animDir;

    #region Start
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameMaster.Instance.playerTransform;
        anim = GetComponent<Orc_Small_Anim>();
    }

    private void Awake()
    {
        enemyState = EnemyState.Idle;
        cooldownTimer = 1.5f;
    }
    #endregion
    private void Update()
    {
        Vector2 distToPlayer = (player.position - transform.position).normalized;
        animDir = distToPlayer.x;

        switch (enemyState)
        {
            case EnemyState.Idle:
                anim.AnimIdle();
                if (Vector2.Distance(transform.position, player.position) < aggroRange)
                {
                    // Locked on to player
                    enemyState = EnemyState.Aggro;
                }
                break;

            case EnemyState.Aggro:
                // Calculates distance to player position multiplied by offset
                Vector2 chargeAttackTarget = (player.position - transform.position).normalized * offsetMultiplier;
                attackDir = chargeAttackTarget;
                animAttackDir = animDir;

                cooldownTimer -= Time.deltaTime;
                if (cooldownTimer <= 0)
                {
                    attackCooldown = false;
                    cooldownTimer = cooldownDur;
                }

                if (cooldownTimer <= 1f)
                {
                    // Play running animation on the spot
                    anim.AnimRun();
                }
                else if (!attackCooldown)
                {
                    // Charges at player
                    anim.AnimAttack();
                    attackCooldown = true;
                    isAttacking = true;
                    enemyState = EnemyState.Attack;
                }
                break;

            case EnemyState.Attack:
                attackTimer -= Time.deltaTime;
                if (attackTimer <= 0)
                {
                    isAttacking = false;
                    attackTimer = attackDur;
                }
                if (!isAttacking)
				{
                    // Stop moving after finishing charge attack
                    enemyState = EnemyState.Tired;
				}
                break;
            case EnemyState.Tired:
                anim.AnimIdle();
                enemyState = EnemyState.Aggro;
                break;
        }
    }
    private void FixedUpdate()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                rb.velocity = Vector2.zero;
                break;
            case EnemyState.Aggro:
                rb.velocity = Vector2.zero;
                break;
            case EnemyState.Attack:
                rb.velocity = attackDir * moveSpeed;
                break;
            case EnemyState.Tired:
                rb.velocity = Vector2.zero;
                break;
        }
    }
}
