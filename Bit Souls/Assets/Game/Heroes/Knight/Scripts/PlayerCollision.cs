using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
	private PlayerStats stats;
	private void Start()
	{
		stats = GetComponent<PlayerStats>();
		/*anim = GetComponent<Animator>();*/
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("EnemyWeapon"))
		{
			EnemyStats enemyStats = collision.gameObject.GetComponentInParent<EnemyStats>();
			if (enemyStats != null)
			{
				stats.LoseHealth(enemyStats.enemyDamage);
			}
		}
		if (collision.gameObject.CompareTag("EnemyProjectile"))
		{
			Projectile projectile = collision.gameObject.GetComponent<Projectile>();
			if (projectile != null)
			{
				stats.LoseHealth(projectile.projectileDamage);
			}
		}
		if (collision.gameObject.CompareTag("Hazard"))
		{
			Hazard hazard = collision.gameObject.GetComponentInParent<Hazard>();
			if (hazard != null)
			{
				stats.LoseHealth(hazard.hazardDamage);
			}
		}
	}
}
