using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;
	public static GameMaster Instance {	get { return instance; } }
	public Vector2 lastCheckpointPos;
	public int lastWeaponIndex;
	public int healCount;
	public int soulCount;
	public Transform playerTransform;
	public PlayerStats playerStats;
	public UIAnim animUI;
	private AudioManager am;

	// Player Death
	public PlayerSkull playerSkull;
	public Vector2 prevDeathPoint;
	public int prevSoulCount;
	public bool droppedSouls = false; // If multiple scenes, set bool to false on advancing to next level
	public int deathCount = 0;

	// Doors
	public bool hasKey = false;
	public bool bossDoorUnlocked = false;
	public bool shortcutDoorUnlocked = false;
	// create separate bool for each door

	// Targeting indicator
	public bool range = true;
	public bool indicator = true;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(instance);
		}
		else
		{
			Destroy(gameObject);
		}
		am = AudioManager.instance;
	}
	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		playerStats = playerTransform.GetComponent<PlayerStats>();
		animUI = GameObject.Find("Player UI").GetComponent<UIAnim>();

		am.PlayTheme();

		if (droppedSouls)
		{
			Instantiate(playerSkull, prevDeathPoint, Quaternion.identity);
		}
		Debug.Log(scene.name);
	}
	public void PlayerDied()
	{
		droppedSouls = true;
		Debug.Log("Restart game");
		prevDeathPoint = playerTransform.position;
		prevSoulCount = soulCount;
		soulCount = 0;
		deathCount += 1;
		animUI.AnimPlayerDied();
		if (playerTransform.gameObject != null)
		{
			Invoke("Restart", 2f);
			playerTransform.gameObject.SetActive(false);
			Instantiate(playerSkull, prevDeathPoint, Quaternion.identity);
		}
	}
	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
