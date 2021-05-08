using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Heal : MonoBehaviour
{
    public GameObject hitbox;
	private void OnTriggerEnter2D(Collider2D enter)
	{
		if (enter.CompareTag("Player"))
		{
			TextPrompt.textValue = "Q to heal.\nConsumes a charge of your healing shard.";
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
