using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    private GameMaster gm;
	public Animator anim;

	private enum BonfireState { Inactive, Active }
	private BonfireState bonfireState;

	public bool ableToRest = false;

	private void Start()
	{
		/*gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();*/
		gm = GameMaster.Instance;
		bonfireState = BonfireState.Inactive;
	}
	private void Update()
	{
		anim.Play(bonfireState.ToString());
		switch (bonfireState)
		{
			case BonfireState.Inactive:
				break;
			case BonfireState.Active:
				if(Vector2.Distance(gm.lastCheckpointPos, transform.position) > 0)
				{
					bonfireState = BonfireState.Inactive;
				}
				if (ableToRest && Input.GetKeyDown(KeyCode.E))
				{
					gm.Restart();
				}
				break;
		}
	}

	private void OnTriggerEnter2D(Collider2D enter)
	{
		if (enter.CompareTag("Player"))
		{
			ableToRest = true;
			TextPrompt.textValue = "Press E to rest.";
			switch (bonfireState)
			{
				case BonfireState.Inactive:
					gm.lastCheckpointPos = transform.position;
					bonfireState = BonfireState.Active;
					break;
			}
		}
	}
	private void OnTriggerExit2D(Collider2D exit)
	{
		if (exit.CompareTag("Player"))
		{
			ableToRest = false;
			TextPrompt.textValue = "";
		}
	}
}
