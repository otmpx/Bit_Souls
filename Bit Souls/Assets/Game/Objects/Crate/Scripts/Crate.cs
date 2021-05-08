using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
	public GameObject destroyedCrate;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("PlayerWeapon") || collision.CompareTag("EnemyWeapon") || collision.CompareTag("EnemyProjectile"))
		{
			Instantiate(destroyedCrate, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}
