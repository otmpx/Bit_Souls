using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUI: MonoBehaviour
{
	public GameObject showUI;
    public HealthBar healthBar;
	public Barrier barrier;
	private EnemyStats stats;

	private AudioManager am;
	public int musicIndex = 1;
	private void Start()
	{
		stats = GetComponent<EnemyStats>();
		healthBar.SetMaxHealth(stats.maxHealth);
		am = AudioManager.instance;
	}
	private void Update()
	{
		if (barrier.bossActive)
		{
			showUI.SetActive(true);

			if (musicIndex == 1)
			{
				am.PlayMiniboss();
			}
			else if (musicIndex == 2)
			{
				am.PlayBoss();
			}
		}
		healthBar.SetHealth(stats.currentHealth);
		if (stats.currentHealth <= 0)
		{
			barrier.bossActive = false;
			showUI.SetActive(false);
			am.PlayTheme();
		}
	}
}
