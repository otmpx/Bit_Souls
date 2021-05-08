using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_GitGud : MonoBehaviour
{
    public GameObject hitbox;
	private void OnTriggerEnter2D(Collider2D enter)
	{
		if (enter.CompareTag("Player"))
		{
			TextPrompt.textValue = "Good luck and git gud.";
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
