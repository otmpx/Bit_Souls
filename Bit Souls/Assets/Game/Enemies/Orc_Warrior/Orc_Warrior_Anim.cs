using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Warrior_Anim : MonoBehaviour
{
	public Animator anim;
	private Orc_Warrior_AI ai;

	private enum AnimationState
	{
		Orc_Warrior_Idle_Left, Orc_Warrior_Idle_Right,
		Orc_Warrior_Run_Left, Orc_Warrior_Run_Right,
		Club_Swing,
	}
	private AnimationState animationState;

	private void Start()
	{
		ai = GetComponent<Orc_Warrior_AI>();
	}

	public void AnimIdle()
	{
		if (ai.animDir < 0)
		{
			animationState = AnimationState.Orc_Warrior_Idle_Left;
		}
		else if(ai.animDir >= 0)
		{
			animationState = AnimationState.Orc_Warrior_Idle_Right;
		}
	}
	public void AnimRun()
	{
		if (ai.animDir < 0)
		{
			animationState = AnimationState.Orc_Warrior_Run_Left;
		}
		else if (ai.animDir >= 0)
		{
			animationState = AnimationState.Orc_Warrior_Run_Right;
		}
	}
	public void AnimAttack()
	{
		animationState = AnimationState.Club_Swing;
	}
	private void Update()
	{
		anim.Play(animationState.ToString());
	}
}
