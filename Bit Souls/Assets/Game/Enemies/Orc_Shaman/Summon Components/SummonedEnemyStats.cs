using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonedEnemyStats : MonoBehaviour
{
	private Orc_Shaman_Anim summon;
	private EnemyStats summoner;
	private EnemyStats stats;

	private void Start()
	{
		summon = GetComponentInParent<Orc_Shaman_Anim>();
		summoner = GetComponentInParent<EnemyStats>();
		stats = GetComponent<EnemyStats>();
		transform.SetParent(null);
	}
	private void Update()
	{
		if(stats.currentHealth <= 0)
		{
			if (summon.summonCount > 0)
			summon.summonCount -= 1;
		}
	}
}
