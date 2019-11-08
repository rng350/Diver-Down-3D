using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject[] enemies;
	public Vector3 spawnValues;
	public float spawnWait;
	public float spawnWaitMin;
	public float spawnWaitMax;

	public int startWait;
	public bool stop;
	public int spawnMax;

	private int randEnemy;              // index number for which enemy is going to be spawned
	private float spawnTimer;

	[SerializeField]
	public int curEnemiesAmt;
	
	void Start()
    {
		spawnTimer = Random.Range(spawnWaitMin, spawnWaitMax);
	}
	
    void Update()
    {
		spawnTimer -= Time.deltaTime;

		if ((GameController.EnemiesAmt < spawnMax) && (spawnTimer <= 0f))
		{
			spawnEnemy();
			spawnTimer = Random.Range(spawnWaitMin, spawnWaitMax);
		}
		curEnemiesAmt = GameController.EnemiesAmt;
	}
	
	void spawnEnemy()
	{
		randEnemy = Random.Range(0, 3);     // number between 0 & 2, inclusive

		float xPosEnemy = Random.Range(-spawnValues.x, spawnValues.x);
		float yPosEnemy = Random.Range(-spawnValues.y, spawnValues.y);
		float zPosEnemy = Random.Range(-spawnValues.z, spawnValues.z);
		Vector3 spawnPosition = new Vector3(xPosEnemy, yPosEnemy, zPosEnemy);

		GameObject enemyCreated = Instantiate(enemies[randEnemy], spawnPosition + transform.TransformPoint(0, 0, 0), Quaternion.identity);
		print("spawned enemy #" + randEnemy + " at (" + xPosEnemy + "," + yPosEnemy + "," + spawnPosition.z + ")");
		GameController.EnemiesAmt++;
	}
}
