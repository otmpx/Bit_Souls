using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
	public GameObject pauseUI;
	public GameObject pauseScreen;
	public GameObject settingsScreen;
	private AudioManager am;
	public Slider volumeSlider;
    public static bool gamePaused = false;
	private void Start()
	{
		am = AudioManager.instance;
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (gamePaused)
			{
				Resume();
			}
			else if (!gamePaused)
			{
				Pause();
			}
		}
	}
	public void Resume()
	{
		pauseUI.SetActive(false);
		Time.timeScale = 1;
		gamePaused = false;
		am.audioSource.pitch = 1f;
	}
	public void Pause()
	{
		pauseUI.SetActive(true);
		Time.timeScale = 0;
		gamePaused = true;
		am.audioSource.pitch = 0.75f;
	}
	public void SettingsMenu()
	{
		pauseScreen.SetActive(false);
		settingsScreen.SetActive(true);
		volumeSlider.value = am.audioSource.volume;
	}
	public void SettingsBack()
	{
		pauseScreen.SetActive(true);
		settingsScreen.SetActive(false);
	}
	public void SettingsVolume()
	{
		am.audioSource.volume = volumeSlider.value;
	}
	public void RestartGame()
	{
		Resume();
		pauseUI.SetActive(false);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	public void LoadMenu()
	{
		Resume();
		SceneManager.LoadScene("Menu");
	}
	public void QuitGame()
	{
		Debug.Log("Quitting game...");
		Application.Quit();
	}
}
