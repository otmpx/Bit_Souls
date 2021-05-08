using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	public HealthBar healthBar;
	public ManaBar manaBar;
	public StaminaBar staminaBar;
	private GameMaster gm;
	private Animator anim;

	public enum WeaponState { Sword = 1, Greatsword = 2, Katana = 3 }
	public static WeaponState weaponState;

	// Player stats
    public int maxHealth = 1000;
    public int currentHealth;
	public int maxMana = 1000;
	public int currentMana;
	public int maxStamina = 1000;
	public int currentStamina;

	// Stamina regen
	private float regenTickTimer;
	public float regenTickDur = 0.01f;
	private float timeBeforeRegenTimer;
	private float timeBeforeRegenDur = 0.5f;
	private bool staminaActionTaken = false;
	public bool isInvulnerable = false;

	// Weapon stats
	public int playerDamage;
	public int staminaCost;
	public int manaCost;
	public float atkMovementMultiplier;

	// Heal
	public int maxHeals = 3;
	public int currentHeals;

	// Souls
	public static int currentSouls = 0;

	private void Start()
	{
		/*gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();*/
		gm = GameMaster.Instance;
		anim = GetComponent<Animator>();

		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);

		currentMana = maxMana;
		manaBar.SetMaxMana(maxMana);

		currentStamina = maxStamina;
		staminaBar.SetMaxStamina(maxStamina);

		if (gm.lastWeaponIndex == 0)
		{
			weaponState = WeaponState.Sword;
		}
		if (gm.healCount == 0)
		{
			currentHeals = maxHeals;
		}
		else
		{
			currentHeals = gm.healCount;
			maxHeals = gm.healCount;
		}
		currentSouls = gm.soulCount;
	}

	private void Update()
	{
		#region Weapon Stats
		switch (weaponState)
		{
			case WeaponState.Sword:
				playerDamage = 100;
				staminaCost = 175;
				manaCost = 500;
				atkMovementMultiplier = 1.5f;
				break;
			case WeaponState.Greatsword:
				playerDamage = 150;
				staminaCost = 200;
				manaCost = 300;
				atkMovementMultiplier = 1f;
				break;
			case WeaponState.Katana:
				playerDamage = 75;
				staminaCost = 150;
				manaCost = 250;
				atkMovementMultiplier = 4f;
				break;
		}
		#endregion
		#region Stamina regen
		timeBeforeRegenTimer -= Time.deltaTime;
		if (timeBeforeRegenTimer <= 0)
		{
			staminaActionTaken = false;
		}
		regenTickTimer -= Time.deltaTime;
		if (regenTickTimer <= 0 && currentStamina < maxStamina && staminaActionTaken == false)
		{
			currentStamina += 5;
			staminaBar.SetStamina(currentStamina);
			regenTickTimer = regenTickDur;
		}
		#endregion
		#region Max health/mana limiter
		if (currentHealth > maxHealth)
		{
			currentHealth = maxHealth;
		}
		if (currentMana > maxMana)
		{
			currentMana = maxMana;
		}
		#endregion

		_ = (WeaponState)gm.lastWeaponIndex;
		gm.healCount = maxHeals;
		gm.soulCount = currentSouls;

		LifeShardText.textValue = currentHeals + " / " + maxHeals;
		SoulText.textValue = currentSouls.ToString();

		if (Input.GetKeyDown(KeyCode.Q) && currentHeals > 0 && currentHealth < maxHealth) // Heal button
		{
			Heal(500);
			currentHeals -= 1;
		}

		if (currentHealth <= 0)
		{
			FindObjectOfType<GameMaster>().PlayerDied();
		}
	}
	public void LoseHealth(int healthChange)
	{
		if (isInvulnerable == false)
		{
			currentHealth -= healthChange;
			if (healthChange > 0)
			{
				anim.Play("Hit");
			}
			healthBar.SetHealth(currentHealth);
		}
	}
	public void Heal (int healthChange)
	{
		currentHealth += healthChange;
		if (healthChange < 0)
		{
			anim.Play("Heal");
		}
		healthBar.SetHealth(currentHealth);
	}
	public void LoseMana(int manaChange)
	{
		currentMana -= manaChange;
		manaBar.SetMana(currentMana);
	}
	public void LoseStamina(int staminaChange)
	{
		staminaActionTaken = true;
		currentStamina -= staminaChange;
		staminaBar.SetStamina(currentStamina);
		timeBeforeRegenTimer = timeBeforeRegenDur;
	}
	public void IFrame() // Triggered through anim events
	{
		if (isInvulnerable)
		{
			isInvulnerable = false;
		}
		else if (!isInvulnerable)
		{
			isInvulnerable = true;
		}
	}
}
