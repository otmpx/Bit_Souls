using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_WASD : MonoBehaviour
{
    public GameObject hitbox;
	private void OnTriggerEnter2D(Collider2D enter)
	{
		if (enter.CompareTag("Player"))
		{
			TextPrompt.textValue = "WASD to move.\nAim with mouse.";
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
