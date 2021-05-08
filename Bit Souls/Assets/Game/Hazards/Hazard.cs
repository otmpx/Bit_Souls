using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public Animator anim;
	public int hazardDamage;
    
    private enum HazardState { Idle, Triggered }
    private HazardState hazardState;

	private void Start()
	{
        hazardState = HazardState.Idle;
	}
	private void OnTriggerEnter2D(Collider2D enter)
	{
		if (enter.CompareTag("Player"))
		{
			AnimTriggered();
		}

	}
	private void AnimIdle()
	{
		hazardState = HazardState.Idle;
	}
	private void AnimTriggered()
	{
		hazardState = HazardState.Triggered;
		anim.Play(hazardState.ToString());
	}
}
