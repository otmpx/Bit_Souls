using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
	public GameObject hitboxall;
	public Animator anim;
	private GameMaster gm;

	private enum DoorState { Door_Closed, Door_Open }
	private DoorState doorState;

	private bool ableToInteract = false;

	private void Start()
	{
		gm = GameMaster.Instance;
		if (gm.bossDoorUnlocked)
		{
			doorState = DoorState.Door_Open;
		}
		else if (!gm.bossDoorUnlocked)
		{
			doorState = DoorState.Door_Closed;
		}
	}
	private void Update()
	{
		anim.Play(doorState.ToString());
		switch (doorState)
		{
			case DoorState.Door_Closed:
				if (ableToInteract && Input.GetKeyDown(KeyCode.E))
				{
					if (gm.hasKey)
					{
						doorState = DoorState.Door_Open;
					}
					else if (!gm.hasKey)
					{
						TextPrompt.textValue = "You need a key to unlock this door.";
					}
				}
				break;
			case DoorState.Door_Open:
				gm.bossDoorUnlocked = true;
				break;
		}
	}
	private void OnTriggerEnter2D(Collider2D enter)
	{
		if (enter.CompareTag("Player"))
		{
			ableToInteract = true;
			TextPrompt.textValue = "Press E to open door.";
		}
	}
	private void OnTriggerExit2D(Collider2D exit)
	{
		if (exit.CompareTag("Player"))
		{
			ableToInteract = false;
			TextPrompt.textValue = "";
		}
	}
}
