using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : MonoBehaviour
{
    private EnemyStats stats;
	public GameObject barrier;

	private void Start()
	{
		stats = GetComponent<EnemyStats>();
	}
	private void LateUpdate()
	{
		if (stats.currentHealth <= 0)
		{
			Destroy(barrier);
		}
	}
}
