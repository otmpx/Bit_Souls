using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public float speed;
	private Rigidbody2D rb;
	public int projectileDamage;

	void Start()
	{
		// player = GameObject.FindGameObjectWithTag("Player").transform;
		// target = new Vector2(player.position.x, player.position.y);
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = transform.up * speed;

	}

	void Update()
	{
		// transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
		// target -> player.position to make homing bullets
		// if (transform.position.x == target.x && transform.position.y == target.y)
	}

	/*public void Init(int _projectileDamage)
	{
		projectileDamage = _projectileDamage;
	}*/

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") || collision.CompareTag("Wall") || collision.CompareTag("Breakable"))
		{
			Destroy(gameObject);
		}
	}
}
