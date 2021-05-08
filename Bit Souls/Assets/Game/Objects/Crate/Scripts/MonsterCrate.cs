using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCrate : MonoBehaviour
{
	public GameObject destroyedCrate;
	public GameObject monsterSpawn;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("PlayerWeapon") || collision.CompareTag("EnemyWeapon") || collision.CompareTag("EnemyProjectile"))
		{
			Instantiate(destroyedCrate, transform.position, transform.rotation);
			Instantiate(monsterSpawn, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}
