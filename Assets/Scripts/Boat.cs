using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
	AudioSource gotTreasure;
	GameObject diverObj;

	void Start()
	{
		gotTreasure = GetComponent<AudioSource>();
		diverObj = GameObject.FindGameObjectWithTag("Player");
	}

	void Update()
	{
		if (diverObj == null)
		{
			diverObj = GameObject.FindGameObjectWithTag("Player");
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		DiverController diver = collision.transform.GetComponent<DiverController>();
		bool isDiver = diver != null;
		if (isDiver)
		{
			switch (diver.carrying)
			{
				case LootCarried.BAG:
					Debug.Log("You brought in a coin bag!");
					GameController.curScore += 2;
					diverObj.GetComponent<Rigidbody>().mass -= 5f;
					break;
				case LootCarried.CHEST:
					Debug.Log("You brought in a treasure chest!");
					GameController.curScore += 10;
					diverObj.GetComponent<Rigidbody>().mass -= 10f;
					break;
				case LootCarried.COIN:
					Debug.Log("You brought in a coin!");
					GameController.curScore += 1;
					diverObj.GetComponent<Rigidbody>().mass -= 1f;
					break;
				case LootCarried.NONE:
					Debug.Log("You brought in nothing!");
					break;
			}
		}
	}
}
