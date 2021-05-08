using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCounter : MonoBehaviour
{
    private EnemyStats stats;
	private RoomClear room;
	private void Start()
	{
		stats = GetComponent<EnemyStats>();
		room = GetComponentInParent<RoomClear>();
	}
	private void Update()
	{
		if (stats.currentHealth <= 0)
		{
			room.currentCount += 1;
		}
	}
}
