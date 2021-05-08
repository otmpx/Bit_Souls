using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupKey : MonoBehaviour
{
	public GameObject keyPickup;
	private bool canPickup = false;
	
	private void Update()
	{
		if (canPickup && Input.GetKeyDown(KeyCode.E))
		{
			GameMaster.Instance.hasKey = true;
		}
		if (GameMaster.Instance.hasKey)
		{
			keyPickup.SetActive(false);
		}
		else
		{
			keyPickup.SetActive(true);
		}
	}
	private void OnTriggerEnter2D(Collider2D enter)
	{
		if (enter.gameObject.CompareTag("Player"))
		{
			TextPrompt.textValue = "Press E to take key.";
			canPickup = true;
		}
	}
	private void OnTriggerExit2D(Collider2D exit)
	{
		if (exit.gameObject.CompareTag("Player"))
		{
			TextPrompt.textValue = "";
			canPickup = false;
		}
	}
}
