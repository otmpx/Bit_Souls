using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteract : MonoBehaviour
{
	private void Start()
	{
		transform.position = GameMaster.Instance.lastCheckpointPos;
	}
}
