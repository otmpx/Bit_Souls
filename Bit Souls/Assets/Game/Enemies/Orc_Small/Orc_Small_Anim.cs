using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Small_Anim : MonoBehaviour
{
    public Animator anim;
    private Orc_Small_AI ai;
	private enum AnimationState
	{
		Orc_Small_Idle_Left, Orc_Small_Idle_Right,
		Orc_Small_Run_Left, Orc_Small_Run_Right,
		Orc_Small_Charge_Left, Orc_Small_Charge_Right,
	}
	private AnimationState animationState;

	private void Start()
	{
		ai = GetComponent<Orc_Small_AI>();
	}

	public void AnimIdle()
	{
		if (ai.animDir < 0)
		{
			animationState = AnimationState.Orc_Small_Idle_Left;
		}
		else if (ai.animDir >= 0)
		{
			animationState = AnimationState.Orc_Small_Idle_Right;
		}
	}
	public void AnimRun()
	{
		if (ai.animDir < 0)
		{
			animationState = AnimationState.Orc_Small_Run_Left;
		}
		else if (ai.animDir >= 0)
		{
			animationState = AnimationState.Orc_Small_Run_Right;
		}
	}
	public void AnimAttack()
	{
		if (ai.animAttackDir < 0)
		{
			animationState = AnimationState.Orc_Small_Charge_Left;
		}
		else if (ai.animAttackDir >= 0)
		{
			animationState = AnimationState.Orc_Small_Charge_Right;
		}
	}
	private void Update()
	{
		anim.Play(animationState.ToString());
	}
}
