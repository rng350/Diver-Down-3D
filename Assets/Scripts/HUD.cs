using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	Transform numOfTanksObj, numOfLivesObj, scoreObj, levelObj, levelTimerObj, superTimerLabelObj, superTimerObj, gameOverObj;
	Text numOftanks, numOfLives, score, curLevel, levelTimerText, superTimerLabelText, superTimerText, gameOverText;

	[SerializeField]
	float levelTimerSet;
	float levelTimerCur;

    // Start is called before the first frame update
    void Start()
    {
		numOfTanksObj = transform.Find("NumOfTanks");
		numOfLivesObj = transform.Find("NumOfLives");
		scoreObj = transform.Find("ScoreNum");
		levelObj = transform.Find("LevelNum");
		levelTimerObj = transform.Find("LevelTimerNum");
		superTimerLabelObj = transform.Find("SuperTimerLabel");
		superTimerObj = transform.Find("SuperTimerNum");
		gameOverObj = transform.Find("GameOver");

		numOftanks = numOfTanksObj.GetComponent<Text>();
		numOfLives = numOfLivesObj.GetComponent<Text>();
		score = scoreObj.GetComponent<Text>();
		curLevel = levelObj.GetComponent<Text>();
		levelTimerText = levelTimerObj.GetComponent<Text>();
		gameOverText = gameOverObj.GetComponent<Text>();

		levelTimerCur = levelTimerSet;
	}

    // Update is called once per frame
    void Update()
	{
		numOftanks.text = "x " + GameController.tanksRemaining;
		numOfLives.text = "x " + GameController.livesRemaining;
		score.text = GameController.curScore.ToString();

		if (!GameController.GameOver)
		{
			curLevel.text = GameController.Level.ToString();

			float truncatedSuperTimer = (float)Math.Truncate(100 * GameController.superTimer) / 100;
			superTimerText.text = truncatedSuperTimer.ToString();

			float truncatedLevelTimer = (float)Math.Truncate(100 * levelTimerCur) / 100;

			levelTimerText.text = truncatedLevelTimer.ToString();

			levelTimerCur -= Time.fixedDeltaTime;

			if (levelTimerCur <= 0f)
			{
				GameController.Level++;
				levelTimerCur = levelTimerSet;
			}
		}

		if (GameController.GameOver)
		{
			gameOverText.enabled = true;
		}
	}
}
