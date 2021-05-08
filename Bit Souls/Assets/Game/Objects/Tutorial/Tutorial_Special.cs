using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Special : MonoBehaviour
{
    public GameObject hitbox;
	private void OnTriggerEnter2D(Collider2D enter)
	{
		if (enter.CompareTag("Player"))
		{
			TextPrompt.textValue = "Right click to use special.\nConsumes mana.";
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
