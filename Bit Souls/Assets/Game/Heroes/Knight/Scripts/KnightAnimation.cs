using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAnimation : MonoBehaviour
{
	public Animator anim;
	private PlayerKnight player;
	private PlayerStats stats;
	private Rigidbody2D rb;

	private enum AnimationState
	{
		Idle_Left, Idle_Right,
		Run_Left, Run_Right,
		Roll_Left, Roll_Right,
		WeaponIdle,
		Sword_Left_Slash, Sword_Right_Slash, Sword_Parry, Sword_Counter,
		Greatsword_Left_Slash, Greatsword_Right_Slash, Greatsword_Charge,
		Katana_Left_Slash, Katana_Right_Slash, Katana_Shuriken,
	}
	[SerializeField]
	private AnimationState animationState;

	public bool attackTransition = false;

	private void Start()
	{
		player = GetComponent<PlayerKnight>();
		stats = GetComponent<PlayerStats>();
		rb = GetComponent<Rigidbody2D>();
	}

	public void AnimMovement()
	{
		if (player.mousePos.x - rb.position.x < 0f && player.moveDir.sqrMagnitude == 0f)
		{
			animationState = AnimationState.Idle_Left;
		}
		else if (player.mousePos.x - rb.position.x >= 0f && player.moveDir.sqrMagnitude == 0f)
		{
			animationState = AnimationState.Idle_Right;
		}
		else if (player.mousePos.x - rb.position.x < 0f && player.moveDir.sqrMagnitude > 0f)
		{
			animationState = AnimationState.Run_Left;
		}
		else if (player.mousePos.x - rb.position.x >= 0f && player.moveDir.sqrMagnitude > 0f)
		{
			animationState = AnimationState.Run_Right;
		}
	}
	public void AnimRoll()
	{
		if (player.mousePos.x - rb.position.x < 0f)
		{
			animationState = AnimationState.Roll_Left;
		}
		else if(player.mousePos.x - rb.position.x >= 0f)
		{
			animationState = AnimationState.Roll_Right;
		}
	}
	public void AnimAttack()
	{
		if (PlayerStats.weaponState == PlayerStats.WeaponState.Sword)
		{
			if (!attackTransition)
			{
				animationState = AnimationState.Sword_Right_Slash;
			}
			else if (attackTransition)
			{
				animationState = AnimationState.Sword_Left_Slash;
			}
			return;
		}

		else if (PlayerStats.weaponState == PlayerStats.WeaponState.Greatsword)
		{
			if (!attackTransition)
			{
				animationState = AnimationState.Greatsword_Right_Slash;
			}
			else if (attackTransition)
			{
				animationState = AnimationState.Greatsword_Left_Slash;
			}
			return;
		}
		else if (PlayerStats.weaponState == PlayerStats.WeaponState.Katana)
		{
			if (!attackTransition)
			{
				animationState = AnimationState.Katana_Right_Slash;
			}
			else if (attackTransition)
			{
				animationState = AnimationState.Katana_Left_Slash;
			}
			return;
		}
	}
	// Shuriken
	public GameObject projectilePrefab;
	public Transform firePoint;
	public void AnimSpecial()
	{
		if (PlayerStats.weaponState == PlayerStats.WeaponState.Sword)
		{
			animationState = AnimationState.Sword_Parry;
		}
		else if (PlayerStats.weaponState == PlayerStats.WeaponState.Greatsword)
		{
			animationState = AnimationState.Greatsword_Charge;
		}
		else if (PlayerStats.weaponState == PlayerStats.WeaponState.Katana)
		{
			Instantiate(projectilePrefab, firePoint.position, firePoint.rotation).GetComponent<PlayerProjectile>().projectileDamage = stats.playerDamage;
			player.AnimEnd();
		}
	}
	private void OnTriggerEnter2D(Collider2D parry)
	{
		switch (animationState)
		{
			case AnimationState.Sword_Parry:
				if (parry.gameObject.CompareTag("EnemyWeapon") || parry.gameObject.CompareTag("EnemyProjectile"))
				{
					animationState = AnimationState.Sword_Counter;
				}
				break;
		}
	}
	public void AttackAnimTransition() // Triggered through events in Unity animations
	{
		if (!attackTransition)
		{
			attackTransition = true;
		}
		else if (attackTransition)
		{
			attackTransition = false;
		}
	}
	public void AnimReset()
	{
		animationState = AnimationState.WeaponIdle;
	}

	private void Update()
	{
		anim.Play(animationState.ToString());
	}
}
