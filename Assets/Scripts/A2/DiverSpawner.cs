using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiverSpawner : MonoBehaviour
{
	[SerializeField]
	public Diver diverPrefab;

	public int diverAmt;
	public int livesLeft;
	public int tanksRemaining;

	void Start()
	{
		//print("Diver spawner says hello!");
	}

	// Update is called once per frame
	void Update()
	{
		diverAmt = GameController.DiverAmt;
		livesLeft = GameController.livesRemaining;
		tanksRemaining = GameController.tanksRemaining;

		if (GameController.DiverAmt == 0)
		{
			if (GameController.livesRemaining > 0)
			{
				Diver newDiver = Instantiate(diverPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
				GameController.tanksRemaining = 2;
				GameController.DiverAmt++;
				print("New " + newDiver.name + "spawned at " + newDiver.transform.position.x + "," + newDiver.transform.position.y + "," + newDiver.transform.position.z + "!");
			}
			else if (GameController.livesRemaining == 0)
			{
				print("Game over!");

				AudioSource bgm = GameObject.Find("HUD").transform.GetComponent<AudioSource>();
				bgm.Stop();

				AudioSource gameOver = GetComponent<AudioSource>();
				gameOver.PlayDelayed(3.5f);

				GameController.GameOver = true;

				GameController.DiverAmt--;	// so the console doesn't get spammed with "Game over"
			}
		}
    }
}
