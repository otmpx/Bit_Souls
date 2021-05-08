using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Ranger_Anim : MonoBehaviour
{
	public Animator anim;
	private Orc_Ranger_AI ai;
	public GameObject projectilePrefab;
	public Transform firePoint;
	private EnemyStats stats;

	private enum AnimationState
	{
		Orc_Ranger_Idle_Left, Orc_Ranger_Idle_Right,
		Orc_Ranger_Run_Left, Orc_Ranger_Run_Right,
		Bow_Shoot,
	}
	private AnimationState animationState;

	private void Start()
	{
		ai = GetComponent<Orc_Ranger_AI>();
		stats = GetComponent<EnemyStats>();
	}
	public void Shoot()
	{
		Instantiate(projectilePrefab, firePoint.position, firePoint.rotation).GetComponent<Projectile>().projectileDamage = stats.enemyDamage;
	}

	public void AnimIdle()
	{
		if (ai.animDir < 0)
		{
			animationState = AnimationState.Orc_Ranger_Idle_Left;
		}
		else if (ai.animDir >= 0)
		{
			animationState = AnimationState.Orc_Ranger_Idle_Right;
		}
	}
	public void AnimRun()
	{
		if (ai.animDir < 0)
		{
			animationState = AnimationState.Orc_Ranger_Run_Left;
		}
		else if (ai.animDir >= 0)
		{
			animationState = AnimationState.Orc_Ranger_Run_Right;
		}
	}
	public void AnimAttack()
	{
		animationState = AnimationState.Bow_Shoot;
	}
	private void Update()
	{
		anim.Play(animationState.ToString());
	}
}
