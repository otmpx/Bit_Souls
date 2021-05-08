using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutDoor : MonoBehaviour
{
	public GameObject hitboxall;
	public Animator anim;
	private ShortcutUnlocked unlockedSide;
	private ShortcutLocked lockedSide;

	private enum DoorState { Door_Closed, Door_Open }
	private DoorState doorState;

	private bool ableToInteract = false;

	private void Start()
	{
		unlockedSide = GetComponentInChildren<ShortcutUnlocked>();
		lockedSide = GetComponentInChildren<ShortcutLocked>();

		if (GameMaster.Instance.shortcutDoorUnlocked)
		{
			doorState = DoorState.Door_Open;
		}
		else if (!GameMaster.Instance.shortcutDoorUnlocked)
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
					if (lockedSide.interactLocked)
					{
						TextPrompt.textValue = "Door cannot be opened from this side.";
					}
					if (unlockedSide.interactUnlocked)
					{
						doorState = DoorState.Door_Open;
					}
				}
				break;
			case DoorState.Door_Open:
				GameMaster.Instance.shortcutDoorUnlocked = true;
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
