using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
	private void Start()
	{
		Invoke("Wait", 6.5f);
	}
	private void Wait()
	{
		SceneManager.LoadScene(1);
	}
}
