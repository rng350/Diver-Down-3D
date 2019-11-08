using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	private GameObject exitButtonObject;
	private GameObject normalModeButtonObj;
	private GameObject specialModeButtonObj;

	void Start()
	{
		normalModeButtonObj = GameObject.Find("PlayButton");
		exitButtonObject = GameObject.Find("ExitButton");
	}

	public void PlayGame()
	{
		GameController.Reset();
		SceneManager.LoadScene("Game");
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
