using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Ogre_Anim : MonoBehaviour
{
	public Animator anim;
	private Orc_Ogre_AI ai;

	private enum AnimationState
	{
		Orc_Ogre_Idle_Left, Orc_Ogre_Idle_Right,
		Orc_Ogre_Run_Left, Orc_Ogre_Run_Right,
		Double, Barrage
	}
	private AnimationState animationState;

	private void Start()
	{
		ai = GetComponent<Orc_Ogre_AI>();
	}

	public void AnimIdle()
	{
		if (ai.animDir < 0)
		{
			animationState = AnimationState.Orc_Ogre_Idle_Left;
		}
		else if(ai.animDir >= 0)
		{
			animationState = AnimationState.Orc_Ogre_Idle_Right;
		}
	}
	public void AnimRun()
	{
		if (ai.animDir < 0)
		{
			animationState = AnimationState.Orc_Ogre_Run_Left;
		}
		else if (ai.animDir >= 0)
		{
			animationState = AnimationState.Orc_Ogre_Run_Right;
		}
	}
	public void AnimAttackShort()
	{
		animationState = AnimationState.Double;
	}
	public void AnimAttackLong()
	{
		animationState = AnimationState.Barrage;
	}
	private void Update()
	{
		anim.Play(animationState.ToString());
	}
}
