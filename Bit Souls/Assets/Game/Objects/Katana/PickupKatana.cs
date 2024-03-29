using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupKatana : MonoBehaviour
{
	public GameObject katanaPickup;
	private bool canPickup = false;
	private void Update()
	{
		if (canPickup && Input.GetKeyDown(KeyCode.E))
		{
			PlayerStats.weaponState = PlayerStats.WeaponState.Katana;
		}
		if (PlayerStats.weaponState == PlayerStats.WeaponState.Katana)
		{
			katanaPickup.SetActive(false);
		}
		else
		{
			katanaPickup.SetActive(true);
		}
	}
	private void OnTriggerEnter2D(Collider2D enter)
	{
		if (enter.gameObject.CompareTag("Player"))
		{
			TextPrompt.textValue = "Press E to switch weapons.";
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
