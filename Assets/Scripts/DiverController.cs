using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiverController : MonoBehaviour
{
	[SerializeField] float moveSpeed;
	[SerializeField] float swimUpSpeed;
	[SerializeField] Rigidbody diverRigidbody;
	[SerializeField] public LootCarried carrying;

	void Start()
	{
		// turn off cursor
		Cursor.lockState = CursorLockMode.Locked;
		carrying = LootCarried.NONE;
	}

	void Update()
	{
		// forward & backward movement
		float translation = Input.GetAxis("Vertical") * moveSpeed;
		float strafe = Input.GetAxis("Horizontal") * moveSpeed;
		translation *= Time.deltaTime;
		strafe *= Time.deltaTime;

		transform.Translate(strafe, 0f, translation);

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (Cursor.lockState == CursorLockMode.Locked)
				Cursor.lockState = CursorLockMode.None;
			else Cursor.lockState = CursorLockMode.Locked;
		}

		// TODO: space to swim up, remember to put a cooldown timer
		if (Input.GetKey(KeyCode.Space))
		{
			diverRigidbody.velocity = new Vector3(0f, swimUpSpeed, 0f);
		}

		// TODO: E to grab/drop loot
		if (Input.GetKeyDown(KeyCode.E))
		{
			if (carrying == LootCarried.NONE)
			{
				// TODO: take loop if in range
			}
			else
			{
				// TODO: drop loot
			}
		}

		// TODO: Left CTRL to throw distraction
		if (Input.GetKeyDown(KeyCode.LeftControl))
		{

		}
	}
}