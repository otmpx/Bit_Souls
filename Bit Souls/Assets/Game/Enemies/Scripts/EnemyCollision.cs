using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private PlayerStats player;
    private EnemyStats enemy;
	private void Start()
	{
		/*player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();*/
		player = GameMaster.Instance.playerStats;
		enemy = GetComponent<EnemyStats>();
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("PlayerWeapon"))
		{
			enemy.LoseHealth(player.playerDamage);
			player.LoseMana(-player.playerDamage);
		}
		if (collision.gameObject.CompareTag("Hazard"))
		{
			Hazard hazard = collision.gameObject.GetComponentInParent<Hazard>();
			if (hazard != null)
			{
				enemy.LoseHealth(hazard.hazardDamage);
			}
		}
	}
}
