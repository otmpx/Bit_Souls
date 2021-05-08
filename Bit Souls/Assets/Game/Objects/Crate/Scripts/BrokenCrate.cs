using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenCrate : MonoBehaviour
{ 
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("PlayerWeapon") || collision.CompareTag("EnemyWeapon") || collision.CompareTag("EnemyProjectile"))
		{
			Destroy(gameObject, 0.5f);
		}
	}
}
