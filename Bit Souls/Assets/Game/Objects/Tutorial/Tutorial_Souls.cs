using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Souls : MonoBehaviour
{
    public GameObject hitbox;
	private void OnTriggerEnter2D(Collider2D enter)
	{
		if (enter.CompareTag("Player"))
		{
			TextPrompt.textValue = "Collect souls by killing enemies and use them to strengthen your healing shard.\nIf you die, you can retrieve them from your previous death point.";
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
