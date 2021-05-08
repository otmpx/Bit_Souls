using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    public Camera cam;
	public Animator anim;
	private PlayerStats stats;
	public float rotationSpeed = 10f;

	private enum TargetingState { Targeting_Normal, Targeting_NoStam, Targeting_NoMana, Targeting_NoStamNoMana }
	private TargetingState targetingState;
	private void Start()
	{
		stats = GetComponentInParent<PlayerStats>();
	}

	private void Update()
	{
		GameMaster gm = GameMaster.Instance;
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			if (gm.range && gm.indicator)
			{
				gm.range = false;
			}
			else if (!gm.range && gm.indicator)
			{
				gm.indicator = false;
			}
			else if (!gm.range && !gm.indicator)
			{
				gm.range = true;
				gm.indicator = true;
			}
		}

		anim.Play(targetingState.ToString());

		if (stats.currentStamina <= 0 && stats.currentMana >= stats.manaCost)
		{
			targetingState = TargetingState.Targeting_NoStam;
		}
		else if (stats.currentMana < stats.manaCost && stats.currentStamina > 0)
		{
			targetingState = TargetingState.Targeting_NoMana;
		}
		else if (stats.currentStamina <= 0 && stats.currentMana < stats.manaCost)
		{
			targetingState = TargetingState.Targeting_NoStamNoMana;
		}
		else
		{
			targetingState = TargetingState.Targeting_Normal;
		}

		if (!gm.range && gm.indicator)
		{
			anim.Play("Range_Off");
		}
		else if (!gm.indicator)
		{
			anim.Play("Indicator_Off");
		}
		else if (gm.range)
		{
			switch (PlayerStats.weaponState)
			{
				case PlayerStats.WeaponState.Sword:
					anim.Play("Range_Sword");
					break;
				case PlayerStats.WeaponState.Greatsword:
					anim.Play("Range_Greatsword");
					break;
				case PlayerStats.WeaponState.Katana:
					anim.Play("Range_Katana");
					break;
			}
		}

		switch (PlayerKnight.playerState)
		{
			case PlayerKnight.PlayerState.Normal:
				Vector3 lookDir = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
				float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
				Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
				transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
				break;
		}
	}
}
