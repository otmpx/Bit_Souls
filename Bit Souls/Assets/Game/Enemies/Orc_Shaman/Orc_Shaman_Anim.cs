using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Shaman_Anim : MonoBehaviour
{
	public Animator anim;
	private Orc_Shaman_AI ai;
	public GameObject summonPrefab;
	public Transform summonPoint;
	public GameObject projectilePrefab;
	public Transform firePoint;
	private EnemyStats stats;

	public int summonCap;
	public int summonCount;
	private bool attackVariation = false;

	private enum AnimationState
	{
		Orc_Shaman_Idle_Left, Orc_Shaman_Idle_Right,
		Orc_Shaman_Run_Left, Orc_Shaman_Run_Right,
		Staff_Summon,
		Staff_Shoot
	}
	private AnimationState animationState;

	private void Start()
	{
		ai = GetComponent<Orc_Shaman_AI>();
		stats = GetComponent<EnemyStats>();
		summonCount = 0;
	}
	public void Summon()
	{
		if (summonCount <= summonCap)
		{
			summonCount += 1;
		}
		Instantiate(summonPrefab, summonPoint.position, Quaternion.identity, transform);
	}
	public void Shoot()
	{
		Instantiate(projectilePrefab, firePoint.position, firePoint.rotation).GetComponent<Projectile>().projectileDamage = stats.enemyDamage;
	}

	public void AnimIdle()
	{
		if (ai.animDir < 0)
		{
			animationState = AnimationState.Orc_Shaman_Idle_Left;
		}
		else if (ai.animDir >= 0)
		{
			animationState = AnimationState.Orc_Shaman_Idle_Right;
		}
	}
	public void AnimRun()
	{
		if (ai.animDir < 0)
		{
			animationState = AnimationState.Orc_Shaman_Run_Left;
		}
		else if (ai.animDir >= 0)
		{
			animationState = AnimationState.Orc_Shaman_Run_Right;
		}
	}
	public void AnimAttack()
	{
		if (summonCount < summonCap && !attackVariation)
		{
			animationState = AnimationState.Staff_Summon;
			attackVariation = true;
		}
		else if (summonCount == summonCap || attackVariation)
		{
			animationState = AnimationState.Staff_Shoot;
			attackVariation = false;
		}
	}
	private void Update()
	{
		anim.Play(animationState.ToString());
	}
}
