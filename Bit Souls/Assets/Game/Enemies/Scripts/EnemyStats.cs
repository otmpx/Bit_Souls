using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
	public int maxHealth;
    public int currentHealth;
	public int enemyDamage;
	public int soulDrop;
	public GameObject skullPrefab;
	private Animator anim;

	private void Start()
	{
		anim = GetComponent<Animator>();
		currentHealth = maxHealth;
	}
	private void Update()
	{
		if (currentHealth > maxHealth)
		{
			currentHealth = maxHealth;
		}
		if (currentHealth < 0)
		{
			currentHealth = 0;
		}
	}
	private void LateUpdate()
	{
		if(currentHealth <= 0)
		{
			PlayerStats.currentSouls += soulDrop;
			Destroy(gameObject);
			Instantiate(skullPrefab, transform.position, Quaternion.identity);
		}
	}

	public void LoseHealth(int healthChange)
	{
		currentHealth -= healthChange;
		if (healthChange > 0)
		{
			anim.Play("Hit");
		}
		/*else if (healthChange < 0)
		{
			anim.Play("Heal");
		}*/
	}
}
