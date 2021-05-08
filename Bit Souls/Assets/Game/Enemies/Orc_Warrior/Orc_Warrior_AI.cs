using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Warrior_AI : MonoBehaviour
{
    public Rigidbody2D rb;
    private Transform player;
    private Orc_Warrior_Anim anim;

    public enum EnemyState { Idle, Aggro, LoseAggro}
    public EnemyState enemyState;

    public float moveSpeed;
    public float aggroRange;
    public float loseAggroRange;
    public float attackRange;

    private Vector2 startingPosition;
    private Vector2 moveDir;
    private bool attackCooldown = false;
    private float cooldownTimer;
    public float cooldownDur;
    public float animDir;

	#region Start
	private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameMaster.Instance.playerTransform;
        anim = GetComponent<Orc_Warrior_Anim>();
    }

	private void Awake()
	{
        enemyState = EnemyState.Idle;
        startingPosition = transform.position;
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

        switch (enemyState)
        {
            case EnemyState.Idle:
				anim.AnimIdle();
				if (Vector2.Distance(transform.position, player.position) < aggroRange)
                {
                    enemyState = EnemyState.Aggro;
                }
                break;

            case EnemyState.Aggro:
                if (Vector2.Distance(transform.position, player.position) > attackRange && !attackAnim)
                {
					// Move enemy to attack range
					anim.AnimRun();
				}
                if (!attackCooldown && !attackAnim)
				{
                    if (Vector2.Distance(transform.position, player.position) <= attackRange)
                    {
                        // Stop to attack at attack range
                        anim.AnimIdle();
                        anim.AnimAttack();
                        enemyState = EnemyState.Idle;
                    }
                }
                if (Vector2.Distance(transform.position, player.position) > loseAggroRange)
                {
                    // Move back to start location when outside aggro range
                    enemyState = EnemyState.LoseAggro;
                }
                break;

            case EnemyState.LoseAggro:
                if (Vector2.Distance(transform.position, startingPosition) < 1f)
				{
                    enemyState = EnemyState.Idle;
				}
                if(Vector2.Distance(transform.position, player.position) < aggroRange)
				{
                    enemyState = EnemyState.Aggro;
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
                if(Vector2.Distance(transform.position, player.position) > attackRange)
				{
                    rb.velocity = moveDir * moveSpeed;
				}
                break;
            case EnemyState.LoseAggro:
                // Needs pathfinding AI added
                rb.velocity = (startingPosition - rb.position).normalized * moveSpeed;
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
}
