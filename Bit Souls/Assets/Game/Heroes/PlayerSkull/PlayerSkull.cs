using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkull : MonoBehaviour
{
	private GameMaster gm;
	private void Start()
	{
		gm = GameMaster.Instance;
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("PlayerWeapon"))
		{
			PlayerStats.currentSouls += gm.prevSoulCount;
			gm.droppedSouls = false;
			gm.animUI.AnimRetrieveSouls();
			Destroy(gameObject);
		}
	}
}
