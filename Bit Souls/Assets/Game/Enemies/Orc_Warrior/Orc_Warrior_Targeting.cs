using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Warrior_Targeting : MonoBehaviour
{
	private Transform player;
	private Orc_Warrior_AI ai;
	public float rotationSpeed;

	private void Start()
	{
		player = GameMaster.Instance.playerTransform;
		ai = GetComponentInParent<Orc_Warrior_AI>();
	}
	private void Update()
	{
		if (!ai.attackAnim)
		{
			Vector3 lookDir = player.position - transform.position;
			float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
			Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
		}
	}
}
