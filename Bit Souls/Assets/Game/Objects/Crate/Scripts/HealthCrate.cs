using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCrate : MonoBehaviour
{
	public GameObject destroyedCrate;
	public GameObject healthDrop;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("PlayerWeapon") || collision.CompareTag("EnemyWeapon") || collision.CompareTag("EnemyProjectile"))
		{
			Instantiate(destroyedCrate, transform.position, transform.rotation);
			Instantiate(healthDrop, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}
