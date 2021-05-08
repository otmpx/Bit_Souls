using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;
	public AudioSource audioSource;

	public AudioClip menu;
	public AudioClip theme;
	public AudioClip miniboss;
	public AudioClip boss;

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else
		{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
	}
	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}
	public void PlayMenu()
	{
		if (audioSource.clip != menu)
		{
			audioSource.clip = menu;
			audioSource.Play();
		}
	}
	public void PlayTheme()
	{
		if (audioSource.clip != theme)
		{
			audioSource.clip = theme;
			audioSource.Play();
		}
	}
	public void PlayMiniboss()
	{
		if (audioSource.clip != miniboss)
		{
			audioSource.clip = miniboss;
			audioSource.Play();
		}
	}
	public void PlayBoss()
	{
		if (audioSource.clip != boss)
		{
			audioSource.clip = boss;
			audioSource.Play();
		}
	}
}
