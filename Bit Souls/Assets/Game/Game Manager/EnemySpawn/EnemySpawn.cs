using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
	public GameObject enemySpawn;
	private void Start()
	{
		Instantiate(enemySpawn, transform.position, Quaternion.identity);
	}
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position, 0.3f);
	}
}
