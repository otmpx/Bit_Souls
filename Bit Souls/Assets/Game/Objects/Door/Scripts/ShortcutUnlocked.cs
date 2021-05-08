using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutUnlocked : MonoBehaviour
{
	public GameObject unlockedHitbox;
	public bool interactUnlocked = false;

	private void OnTriggerEnter2D(Collider2D enter)
	{
		if (enter.CompareTag("Player"))
		{
			interactUnlocked = true;
		}
	}
	private void OnTriggerExit2D(Collider2D exit)
	{
		if (exit.CompareTag("Player"))
		{
			interactUnlocked = false;
		}
	}
}
