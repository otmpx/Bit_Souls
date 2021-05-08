using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
	private Transform player;
	public Transform teleportPos;
	private bool canInteract = false;
	public bool bossActive = false;
	private void Start()
	{
		player = GameMaster.Instance.playerTransform;
	}
	private void Update()
	{
		if (canInteract && Input.GetKeyDown(KeyCode.E))
		{
			player.position = teleportPos.position;
			bossActive = true;
		}
	}
	private void OnTriggerEnter2D(Collider2D enter)
	{
		if (enter.gameObject.CompareTag("Player"))
		{
			TextPrompt.textValue = "Press E to enter through fog.";
			canInteract = true;
		}
	}
	private void OnTriggerExit2D(Collider2D exit)
	{
		if (exit.gameObject.CompareTag("Player"))
		{
			TextPrompt.textValue = "";
			canInteract = false;
		}
	}
}
