using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPrompt : MonoBehaviour
{
    public Text txt;
    public static string textValue;
	private void Start()
	{
		txt = GetComponent<Text>();
	}
	private void Update()
	{
		txt.text = textValue;
	}
}
