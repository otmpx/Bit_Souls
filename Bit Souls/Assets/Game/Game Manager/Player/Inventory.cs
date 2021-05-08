using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public GameObject sword;
	public GameObject greatsword;
	public GameObject katana;

	private void Update()
	{
		if (PlayerStats.weaponState == PlayerStats.WeaponState.Sword)
		{
			sword.SetActive(true);
			GameMaster.Instance.lastWeaponIndex = 1;
		}
		else
		{
			sword.SetActive(false);
		}

		if (PlayerStats.weaponState == PlayerStats.WeaponState.Greatsword)
		{
			greatsword.SetActive(true);
			GameMaster.Instance.lastWeaponIndex = 2;
		}
		else
		{
			greatsword.SetActive(false);
		}

		if (PlayerStats.weaponState == PlayerStats.WeaponState.Katana)
		{
			GameMaster.Instance.lastWeaponIndex = 3;
			katana.SetActive(true);
		}
		else
		{
			katana.SetActive(false);
		}
	}
}
