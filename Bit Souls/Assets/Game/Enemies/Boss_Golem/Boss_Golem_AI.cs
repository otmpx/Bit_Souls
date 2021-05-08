using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Golem_AI : MonoBehaviour
{
    public Rigidbody2D rb;
    private Transform player;
    private Boss_Golem_Anim anim;
    private EnemyStats stats;

    public enum EnemyState { Idle, Aggro, Melee, Jump, Shoot, Death }
    public EnemyState enemyState;

    public float moveSpeed;
    public float aggroRange;
    public float attackRangeShort;
    public float attackRangeLong;
    public float enrageThreshold;
    public bool isEnraged = false;
    public bool attackVariation = false;
    public int attackVariationFrequency;
    private int attackVariationCount = 0;

    private Vector2 moveDir;
    private bool attackCooldown = false;
    private float cooldownTimer;
    public float cooldownDur;
    public float animDir;

    public float enragedCooldownDur;
    public int enragedAttackVariationFrequency;

    #region Start
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameMaster.Instance.playerTransform;
        anim = GetComponent<Boss_Golem_Anim>();
        stats = GetComponent<EnemyStats>();
    }

    private void Awake()
    {
        enemyState = EnemyState.Idle;
    }
    #endregion

    private void Update()
    {
        Vector2 distToPlayer = (player.position - transform.position).normalized;
        moveDir = distToPlayer;
        animDir = distToPlayer.x;

        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0)
        {
            attackCooldown = false;
        }

        if (stats.currentHealth <= stats.maxHealth * enrageThreshold)
        {
            isEnraged = true;
            cooldownDur = enragedCooldownDur;
            attackVariationFrequency = enragedAttackVariationFrequency;
        }

        if (attackVariationCount >= attackVariationFrequency)
		{
            attackVariationCount = 0;
            attackVariation = true;
		}

        switch (enemyState)
        {
            case EnemyState.Idle:
                if (!isEnraged)
                {
                    anim.AnimIdle1();
                }
                else if (isEnraged)
                {
                    anim.AnimIdle2();
                }
                if (Vector2.Distance(transform.position, player.position) < aggroRange)
                {
                    enemyState = EnemyState.Aggro;
                }
                break;

            case EnemyState.Aggro:
                if (Vector2.Distance(transform.position, player.position) > attackRangeShort && !attackAnim)
                {
                    if (!isEnraged)
                    {
                        anim.AnimRun1();
                    }
                    else if (isEnraged)
                    {
                        anim.AnimRun2();
                    }
                }
                if (!attackCooldown && !attackAnim)
				{
                    if (Vector2.Distance(transform.position, player.position) <= attackRangeShort && !attackVariation)
                    {
                        // Short ranged attack
                        if (!isEnraged)
                        {
                            anim.AnimIdle1();
                        }
                        else if (isEnraged)
                        {
                            anim.AnimIdle2();
                        }
                        attackVariationCount += 1;
                        anim.AnimMelee();
                        enemyState = EnemyState.Idle;
                    }
                    else if (Vector2.Distance(transform.position, player.position) <= attackRangeLong && Vector2.Distance(transform.position, player.position) > attackRangeShort && !attackVariation)
                    {
                        // Long ranged attack
                        if (!isEnraged)
                        {
                            anim.AnimIdle1();
                        }
                        else if (isEnraged)
                        {
                            anim.AnimIdle2();
                        }
                        attackVariationCount += 1;
                        anim.AnimShoot();
                        enemyState = EnemyState.Idle;
                    }
                    else if (attackVariation)
                    {
                        if (!isEnraged)
                        {
                            anim.AnimJump1();
                        }
                        else if (isEnraged)
                        {
                            anim.AnimJump2();
                        }
                        attackVariation = false;
                        enemyState = EnemyState.Idle;
                    }
                }
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
                if (Vector2.Distance(transform.position, player.position) > attackRangeShort)
                {
                    rb.velocity = moveDir * moveSpeed;
                }
                break;
        }
        if (attackAnim)
        {
            rb.velocity = Vector2.zero;
        }
    }
    public bool attackAnim = false; // Triggered through events in Unity animations
    public void StartAttackAnim()
    {
        attackAnim = true;
    }
    public void EndAttackAnim()
    {
        attackAnim = false;
        attackCooldown = true;
        cooldownTimer = cooldownDur;
    }

    public GameObject jumpIndicator;
    public void JumpTargeting()
    {
        transform.position = new Vector2(player.position.x, player.position.y + 0.9f);
        Instantiate(jumpIndicator, new Vector2(player.position.x, player.position.y), Quaternion.identity);
    }
}
