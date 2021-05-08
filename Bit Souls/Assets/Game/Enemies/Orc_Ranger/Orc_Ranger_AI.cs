using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Ranger_AI : MonoBehaviour
{
    public Rigidbody2D rb;
    private Transform player;
    private Orc_Ranger_Anim anim;

    public enum EnemyState { Idle, Aggro, Retreat, LoseAggro }
    public EnemyState enemyState;

    public float moveSpeed;
    public float aggroRange;
    public float loseAggroRange;
    public float attackRange;
    public float retreatRange;

    private Vector2 startingPosition;
    private Vector2 moveDir;
    private bool attackCooldown = false;
    private float cooldownTimer;
    public float cooldownDur;
    private bool stopRetreat = false;
    public float animDir;

    #region Start
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameMaster.Instance.playerTransform;
        anim = GetComponent<Orc_Ranger_Anim>();
    }

    private void Awake()
    {
        enemyState = EnemyState.Idle;
        startingPosition = transform.position;
    }
	#endregion
	#region Collide with wall
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Wall"))
		{
            stopRetreat = true;
		}
	}
	private void OnCollisionExit2D(Collision2D collision)
	{
        if (collision.gameObject.CompareTag("Wall"))
        {
            stopRetreat = false;
        }
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
                    if (Vector2.Distance(transform.position, player.position) <= attackRange && (Vector2.Distance(transform.position, player.position) > retreatRange || stopRetreat))
                    {
                        // Attack when in attack range and not retreating
                        anim.AnimIdle();
                        anim.AnimAttack();
                        enemyState = EnemyState.Idle;
                    }
                }
                if (Vector2.Distance(transform.position, player.position) <= retreatRange && !attackAnim && !stopRetreat)
				{
                    // Retreat when too close to player
                    anim.AnimRun();
                    enemyState = EnemyState.Retreat;
				}
                if (Vector2.Distance(transform.position, player.position) > loseAggroRange)
                {
                    // Move back to start location when outside aggro range
                    enemyState = EnemyState.LoseAggro;
                }
                break;

            case EnemyState.Retreat:
                if(Vector2.Distance(transform.position, player.position) > retreatRange || stopRetreat)
				{
                    enemyState = EnemyState.Aggro;
				}
                break;

            case EnemyState.LoseAggro:
                if (Vector2.Distance(transform.position, startingPosition) < 1f)
                {
                    enemyState = EnemyState.Idle;
                }
                if (Vector2.Distance(transform.position, player.position) < aggroRange)
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
                if (Vector2.Distance(transform.position, player.position) > attackRange)
                {
                    rb.velocity = moveDir * moveSpeed;
                }
                break;
            case EnemyState.Retreat:
                if (Vector2.Distance(transform.position, player.position) <= retreatRange)
                {
                    rb.velocity = moveDir * -moveSpeed;
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
