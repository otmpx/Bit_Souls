using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
	public GameObject hitbox;
	private GameMaster gm;
	private void Start()
	{
		gm = GameMaster.Instance;
	}
	private void OnTriggerEnter2D(Collider2D enter)
	{
		if (enter.CompareTag("Player"))
		{
			TextPrompt.textValue = "Congratulations, you beat the game ! \n Total deaths : " + gm.deathCount.ToString();
		}
	}
	private void OnTriggerExit2D(Collider2D exit)
	{
		if (exit.CompareTag("Player"))
		{
			TextPrompt.textValue = "";
		}
	}
}
