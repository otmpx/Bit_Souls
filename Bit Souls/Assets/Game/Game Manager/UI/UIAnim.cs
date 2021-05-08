using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnim : MonoBehaviour
{
	public Animator anim;
	public void AnimPlayerDied()
	{
		anim.Play("PlayerDied");
	}
	public void AnimRetrieveSouls()
	{
		anim.Play("RetrieveSouls");
	}
}
