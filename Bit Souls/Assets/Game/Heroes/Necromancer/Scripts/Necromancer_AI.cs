using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer_AI : MonoBehaviour
{
    public Rigidbody2D rb;
    private Transform player;
    private Necromancer_Anim anim;

    public enum EnemyState { Idle, Aggro}
    public EnemyState enemyState;

    public float moveSpeed;
    public float aggroRange;
    public float loseAggroRange;
    public float attackRange;

    private Vector2 moveDir;
    public float animDir;

    #region Start
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameMaster.Instance.playerTransform;
        anim = GetComponent<Necromancer_Anim>();
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

        switch (enemyState)
        {
            case EnemyState.Idle:
                anim.AnimIdle();
                if (Vector2.Distance(transform.position, player.position) < aggroRange && Vector2.Distance(transform.position, player.position) > attackRange)
                {
                    enemyState = EnemyState.Aggro;
                }
                break;

            case EnemyState.Aggro:
                if (Vector2.Distance(transform.position, player.position) > attackRange)
                {
                    // Move enemy to attack range
                    anim.AnimRun();
                }
                if (Vector2.Distance(transform.position, player.position) <= attackRange)
                {
                    // Move enemy to attack range
                    enemyState = EnemyState.Idle;
                }
                if (Vector2.Distance(transform.position, player.position) > loseAggroRange)
                {
                    // Stop moving when outside aggro range
                    enemyState = EnemyState.Idle;
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
        }
    }
}
