using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifespring : MonoBehaviour
{
	private PlayerStats stats;
	public GameObject lifespring;
	private bool canInteract = false;
	public int soulCost1;
	public int soulCost2;
	public int soulCost3;
	public int soulCost4;
	public int soulPrice;
	private void Start()
	{
		stats = GameMaster.Instance.playerStats;
	}
	private void Update()
	{
		if (stats.maxHeals == 3)
		{
			soulPrice = soulCost1;
		}
		else if (stats.maxHeals == 4)
		{
			soulPrice = soulCost2;
		}
		else if (stats.maxHeals == 5)
		{
			soulPrice = soulCost3;
		}
		else if (stats.maxHeals == 6)
		{
			soulPrice = soulCost4;
		}

		if (canInteract && Input.GetKeyDown(KeyCode.E) && PlayerStats.currentSouls >= soulPrice)
		{
			PlayerStats.currentSouls -= soulPrice;
			stats.maxHeals += 1;
			TextPrompt.textValue = "Life shard has been strengthened.";
		}
		else if (canInteract && Input.GetKeyDown(KeyCode.E) && PlayerStats.currentSouls < soulPrice)
		{
			TextPrompt.textValue = "Not enough souls.";
		}
	}
	private void OnTriggerEnter2D(Collider2D enter)
	{
		if (enter.gameObject.CompareTag("Player"))
		{
			if (stats.maxHeals >= 7)
			{
				TextPrompt.textValue = "Life shard is at maximum strength.";
			}
			else if (stats.maxHeals < 7)
			{
				TextPrompt.textValue = "Press E to strengthen life shard for " + soulPrice + " souls.";
				canInteract = true;
			}
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
