using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Golem_Anim : MonoBehaviour
{
	public Animator anim;
	private Boss_Golem_AI ai;
	public GameObject meleePrefabLeft;
	public GameObject meleePrefabRight;
	public GameObject rangedPrefab;
	public GameObject shockwavePrefab;
	public Transform firepoint;
	public Transform leftFirepoint;
	public Transform rightFirepoint;

	private enum AnimationState
	{
		Golem1_Idle_Left, Golem1_Idle_Right,
		Golem1_Walk_Left, Golem1_Walk_Right,
		Golem2_Idle_Left, Golem2_Idle_Right,
		Golem2_Walk_Left, Golem2_Walk_Right,
		Melee_Left, Melee_Right,
		Ranged_Left, Ranged_Right,
		Jump1_Left, Jump1_Right,
		Jump2_Left, Jump2_Right,
		Death
	}
	private AnimationState animationState;

	private void Start()
	{
		ai = GetComponent<Boss_Golem_AI>();
	}

	public void AnimIdle1()
	{
		if (ai.animDir < 0)
		{
			animationState = AnimationState.Golem1_Idle_Left;
		}
		else if (ai.animDir >= 0)
		{
			animationState = AnimationState.Golem1_Idle_Right;
		}
	}
	public void AnimRun1()
	{
		if (ai.animDir < 0)
		{
			animationState = AnimationState.Golem1_Walk_Left;
		}
		else if (ai.animDir >= 0)
		{
			animationState = AnimationState.Golem1_Walk_Right;
		}
	}
	public void AnimIdle2()
	{
		if (ai.animDir < 0)
		{
			animationState = AnimationState.Golem2_Idle_Left;
		}
		else if (ai.animDir >= 0)
		{
			animationState = AnimationState.Golem2_Idle_Right;
		}
	}
	public void AnimRun2()
	{
		if (ai.animDir < 0)
		{
			animationState = AnimationState.Golem2_Walk_Left;
		}
		else if (ai.animDir >= 0)
		{
			animationState = AnimationState.Golem2_Walk_Right;
		}
	}
	public void AnimJump1()
	{
		if (ai.animDir < 0)
		{
			animationState = AnimationState.Jump1_Left;
		}
		else if (ai.animDir >= 0)
		{
			animationState = AnimationState.Jump1_Right;
		}
	}
	public void AnimJump2()
	{
		if (ai.animDir < 0)
		{
			animationState = AnimationState.Jump2_Left;
		}
		else if (ai.animDir >= 0)
		{
			animationState = AnimationState.Jump2_Right;
		}
	}
	public void AnimMelee()
	{
		if (ai.animDir < 0)
		{
			animationState = AnimationState.Melee_Left;
		}
		else if (ai.animDir >= 0)
		{
			animationState = AnimationState.Melee_Right;
		}
	}
	public void AnimShoot()
	{
		if (ai.animDir < 0)
		{
			animationState = AnimationState.Ranged_Left;
		}
		else if (ai.animDir >= 0)
		{
			animationState = AnimationState.Ranged_Right;
		}
	}
	public void AnimDeath()
	{
		animationState = AnimationState.Death;
	}
	public void PunchLeft()
	{
		Instantiate(meleePrefabLeft, leftFirepoint.position, Quaternion.identity, transform);
	}
	public void PunchRight()
	{
		Instantiate(meleePrefabRight, rightFirepoint.position, Quaternion.identity, transform);
	}
	public void Shoot()
	{
		Instantiate(rangedPrefab, firepoint.position, firepoint.rotation);
	}
	public void Shockwave1()
	{
		Instantiate(shockwavePrefab, firepoint.position, Quaternion.Euler(new Vector3(0, 0, 0)));
		Instantiate(shockwavePrefab, firepoint.position, Quaternion.Euler(new Vector3(0, 0, 90)));
		Instantiate(shockwavePrefab, firepoint.position, Quaternion.Euler(new Vector3(0, 0, 180)));
		Instantiate(shockwavePrefab, firepoint.position, Quaternion.Euler(new Vector3(0, 0, 270)));
	}
	public void Shockwave2()
	{
		Instantiate(shockwavePrefab, firepoint.position, Quaternion.Euler(new Vector3(0, 0, 0)));
		Instantiate(shockwavePrefab, firepoint.position, Quaternion.Euler(new Vector3(0, 0, 45)));
		Instantiate(shockwavePrefab, firepoint.position, Quaternion.Euler(new Vector3(0, 0, 90)));
		Instantiate(shockwavePrefab, firepoint.position, Quaternion.Euler(new Vector3(0, 0, 135)));
		Instantiate(shockwavePrefab, firepoint.position, Quaternion.Euler(new Vector3(0, 0, 180)));
		Instantiate(shockwavePrefab, firepoint.position, Quaternion.Euler(new Vector3(0, 0, 225)));
		Instantiate(shockwavePrefab, firepoint.position, Quaternion.Euler(new Vector3(0, 0, 270)));
		Instantiate(shockwavePrefab, firepoint.position, Quaternion.Euler(new Vector3(0, 0, 315)));
	}
	private void Update()
	{
		anim.Play(animationState.ToString());
	}
}
