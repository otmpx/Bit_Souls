using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomClear : MonoBehaviour
{
	private RoomCounter[] stats;
	public GameObject barrier;
	public int roomCount = 0;
	public int currentCount = 0;

	private void Start()
	{
		stats = GetComponentsInChildren<RoomCounter>();
		foreach (RoomCounter count in stats)
		{
			roomCount += 1;
		}
	}
	private void LateUpdate()
	{
		if (currentCount >= roomCount)
		{
			Destroy(barrier);
		}
	}
}
