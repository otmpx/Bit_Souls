using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnight : MonoBehaviour
{
	public Rigidbody2D rb;
	public Animator anim;
	public Camera cam;
	private KnightAnimation playerAnim;
	private PlayerStats stats;

	public enum PlayerState { Normal, Rolling, Attack, Special }
	public static PlayerState playerState;

	[Header("Movement")]
	public const float moveSpeed = 5f;
	public Vector2 mousePos;
	public Vector2 moveDir;

	[Space]

	[Header("Rolling")]
	public float rollDistance = 4f;
	public float rollSpeed = 10f;
	/*private float rollDuration;*/
	private Vector2 target;
	public LayerMask rollLayerMask;

	#region Start
	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		playerAnim = GetComponent<KnightAnimation>();
		stats = GetComponent<PlayerStats>();
		/*rollDuration = rollDistance / rollSpeed;*/
	}
	private void Awake()
	{
		playerState = PlayerState.Normal;
	}
	#endregion

	private void Update()
	{
		if (!PauseMenu.gamePaused)
		{
			#region Movement

			mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

			Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
			moveDir = moveInput.normalized;

			#endregion

			switch (playerState)
			{
				case PlayerState.Normal:
				playerAnim.AnimMovement();
		
					#region Rolling

					if (Input.GetKeyDown(KeyCode.Space) && moveDir != new Vector2(0, 0) && stats.currentStamina > 0)
					{
						playerAnim.AnimRoll();
						stats.LoseStamina(250);
						playerState = PlayerState.Rolling;
						/*Invoke("AnimEnd", rollDuration);*/

						Vector2 playerPos = transform.position;
						target = playerPos + moveDir * rollDistance;

						RaycastHit2D rc = Physics2D.Raycast(transform.position, moveDir, rollDistance, rollLayerMask);
						if (rc.collider != null)
						{
							target = rc.point;
						}
					}

					#endregion

					#region Attacking

					if (Input.GetKeyDown(KeyCode.Mouse0) && stats.currentStamina > 0)
					{
						playerState = PlayerState.Attack;
						playerAnim.AnimAttack();
						stats.LoseStamina(stats.staminaCost);
					}

					#endregion

					#region Special

					if(Input.GetKeyDown(KeyCode.Mouse1) && stats.currentMana >= stats.manaCost)
					{
						playerState = PlayerState.Special;
						playerAnim.AnimSpecial();
						stats.LoseMana(stats.manaCost);
					}
					#endregion

					#region Inventory

					/*if (Input.GetKeyDown(KeyCode.Alpha1))
					{
						PlayerStats.weaponState = PlayerStats.WeaponState.Sword;
					}
					if (Input.GetKeyDown(KeyCode.Alpha2))
					{
						PlayerStats.weaponState = PlayerStats.WeaponState.Greatsword;
					}
					if (Input.GetKeyDown(KeyCode.Alpha3))
					{
						PlayerStats.weaponState = PlayerStats.WeaponState.Katana;
					}*/

					#endregion
					break;
			}
		}
	}
	private void FixedUpdate()
	{
		switch (playerState)
		{
			case PlayerState.Normal:
				rb.velocity = moveDir * moveSpeed;
				break;

			case PlayerState.Rolling:
				transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * rollSpeed);
				break;
		
			case PlayerState.Attack:
				rb.velocity = moveDir * moveSpeed * stats.atkMovementMultiplier;
				break;
			case PlayerState.Special:
				rb.velocity = moveDir * moveSpeed;
				break;
		}
	}
	public void AnimEnd() // Triggered through events in Unity animations
	{
		playerState = PlayerState.Normal;
	}
}
