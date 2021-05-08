using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_Melee : MonoBehaviour
{
	public float speed;
	private Transform player;
	private Vector2 target;

	void Start()
	{
		player = GameMaster.Instance.playerTransform;
		target = new Vector2(player.position.x, player.position.y);
		Invoke("Expire", 0.333f);
	}
	private void Expire()
	{
		Destroy(gameObject);
	}
	private void FixedUpdate()
	{
		transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
	}
}
