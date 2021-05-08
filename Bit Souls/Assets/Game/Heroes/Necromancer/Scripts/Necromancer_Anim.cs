using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer_Anim : MonoBehaviour
{
	public Animator anim;
	private Necromancer_AI ai;

	private enum AnimationState
	{
		Necromancer_Idle_Left, Necromancer_Idle_Right,
		Necromancer_Run_Left, Necromancer_Run_Right
	}
	private AnimationState animationState;

	private void Start()
	{
		ai = GetComponent<Necromancer_AI>();
	}

	public void AnimIdle()
	{
		if (ai.animDir < 0)
		{
			animationState = AnimationState.Necromancer_Idle_Left;
		}
		else if (ai.animDir >= 0)
		{
			animationState = AnimationState.Necromancer_Idle_Right;
		}
	}
	public void AnimRun()
	{
		if (ai.animDir < 0)
		{
			animationState = AnimationState.Necromancer_Run_Left;
		}
		else if (ai.animDir >= 0)
		{
			animationState = AnimationState.Necromancer_Run_Right;
		}
	}
	private void Update()
	{
		anim.Play(animationState.ToString());
	}
}
