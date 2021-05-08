using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTeamUI: MonoBehaviour
{
	public GameObject showUI;
    public HealthBar healthBar;
	public Barrier barrier;
	public EnemyStats[] stats;

	private AudioManager am;
	private void Start()
	{
		stats = GetComponentsInChildren<EnemyStats>();
		healthBar.SetMaxHealth(stats[0].maxHealth + stats[1].maxHealth + stats[2].maxHealth);

		am = AudioManager.instance;
	}
	private void Update()
	{
		if (barrier.bossActive)
		{
			showUI.SetActive(true);

			am.PlayMiniboss();
		}
		healthBar.SetHealth(stats[0].currentHealth + stats[1].currentHealth + stats[2].currentHealth);
		if (stats[0].currentHealth + stats[1].currentHealth + stats[2].currentHealth <= 0)
		{
			barrier.bossActive = false;
			showUI.SetActive(false);
			am.PlayTheme();
		}
	}
}
