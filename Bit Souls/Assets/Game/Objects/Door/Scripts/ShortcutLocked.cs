using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutLocked : MonoBehaviour
{
	public GameObject lockedHitbox;
	public bool interactLocked = false;

	private void OnTriggerEnter2D(Collider2D enter)
	{
		if (enter.CompareTag("Player"))
		{
			interactLocked = true;
		}
	}
	private void OnTriggerExit2D(Collider2D exit)
	{
		if (exit.CompareTag("Player"))
		{
			interactLocked = false;
		}
	}
}
