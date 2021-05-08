using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject menuScreen;
    public GameObject settingsScreen;
    private AudioManager am;
    public Slider volumeSlider;
    private void Start()
    {
        am = AudioManager.instance;
		am.PlayMenu();
    }
	public void StartGame()
	{
		SceneManager.LoadScene("Game");
	}
	public void QuitGame()
	{
		Debug.Log("Quitting game...");
		Application.Quit();
	}
	public void SettingsMenu()
	{
		menuScreen.SetActive(false);
		settingsScreen.SetActive(true);
		volumeSlider.value = am.audioSource.volume;
	}
	public void SettingsBack()
	{
		menuScreen.SetActive(true);
		settingsScreen.SetActive(false);
	}
	public void SettingsVolume()
	{
		am.audioSource.volume = volumeSlider.value;
	}
}
