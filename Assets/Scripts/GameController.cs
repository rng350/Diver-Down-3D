using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameController
{
	private static int
		curLevel = 1,
		curEnemiesAmt = 0,
		curTreasureAmt = 0,
		curOctopusAmt = 0,
		lives = 3,
		score = 0,
		tanks = 2,
		curHighScore = 0,
		curDiverAmt = 0;
	private static bool
		specialModeToggled = false,
		gameIsOver = false;
	private static float
		curSuperModeTimer = 0f;

	public static int Level
	{
		get
		{
			return curLevel;
		}
		set
		{
			curLevel = value;
		}
	}

	// tracks number of enemies in world
	public static int EnemiesAmt
	{
		get
		{
			return curEnemiesAmt;
		}
		set
		{
			curEnemiesAmt = value;
		}
	}

	// tracks amount of treasures present in game
	public static int TreasureAmt
	{
		get
		{
			return curTreasureAmt;
		}
		set
		{
			curTreasureAmt = value;
		}
	}

	public static int OctopusAmt
	{
		get
		{
			return curOctopusAmt;
		}
		set
		{
			curOctopusAmt = value;
		}
	}

	public static int DiverAmt
	{
		get
		{
			return curDiverAmt;
		}
		set
		{
			curDiverAmt = value;
		}
	}

	public static int livesRemaining
	{
		get
		{
			return lives;
		}
		set
		{
			lives = value;
		}
	}

	public static int tanksRemaining
	{
		get
		{
			return tanks;
		}
		set
		{
			tanks = value;
		}
	}

	public static int curScore
	{
		get
		{
			return score;
		}
		set
		{
			score = value;
		}
	}

	public static int highScore
	{
		get
		{
			return curHighScore;
		}
		set
		{
			curHighScore = value;
		}
	}

	public static bool isSpecialMode
	{
		get
		{
			return specialModeToggled;
		}
		set
		{
			specialModeToggled = value;
		}
	}

	public static float superTimer
	{
		get
		{
			return curSuperModeTimer;
		}
		set
		{
			curSuperModeTimer = value;
		}
	}

	public static void Reset()
	{
		curLevel = 1;
		curEnemiesAmt = 0;
		curTreasureAmt = 0;
		curOctopusAmt = 0;
		lives = 3;
		score = 0;
		tanks = 2;
		curHighScore = 0;
		curDiverAmt = 0;
		gameIsOver = false;
	}

	public static bool GameOver
	{
		get
		{
			return gameIsOver;
		}
		set
		{
			gameIsOver = value;
		}
	}
}