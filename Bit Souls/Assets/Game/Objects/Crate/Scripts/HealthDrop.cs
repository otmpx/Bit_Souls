using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : MonoBehaviour
{
	private PlayerStats stats;
	private void Start()
	{
		stats = GameMaster.Instance.playerStats;
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			stats.LoseHealth(-200);
			Destroy(gameObject);
		}
	}
}
