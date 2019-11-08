using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
	[SerializeField]
	public int value;
	[SerializeField]
	public float mass;
	[SerializeField]
	public float fallSpeed;
	[SerializeField]
	public int curTreasureAmt;

	float timer;

    // Start is called before the first frame update
    void Start()
	{
		GameController.TreasureAmt++;
		timer = Random.Range(20, 40);
	}

    // Update is called once per frame
    void Update()
    {
		bool isntCarriedByDiver = this.GetComponentInParent<Diver>() == null;
		if (isntCarriedByDiver)
		{
			CheckTimer();
			FallToBottom();
		}
		curTreasureAmt = GameController.TreasureAmt;
    }

	// TODO: Notify spawner when treasure is destroyed
	void CheckTimer()
	{
		timer -= Time.deltaTime;

		if (timer <= 0f)
		{
			print("Current # of treasures: " + GameController.TreasureAmt);
			Destroy(this.gameObject);
		}
	}
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Diver diver = collision.transform.GetComponent<Diver>();
		if (diver != null)
		{
			// to prevent instantly picking up loot dropped from getting hurt
			if (diver.dInvincibleTimerCur <= 0f)
			{
				print("Diver got treasure!");
				int amtOfTreasureCarried = diver.GetComponentsInChildren<Treasure>().Length;
				if (amtOfTreasureCarried == 0)
				{
					this.transform.SetParent(diver.transform, true);
					Rigidbody2D divRigid = diver.transform.GetComponentInChildren<Rigidbody2D>();
					divRigid.mass += this.mass;
				}
			}
		}
	}
	
	public void FallToBottom()
	{
		//this.transform.SetParent(null, true);
		if (this.transform.position.y > -250f)
		{
			transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
		}
	}

	private void OnDestroy()
	{
		GameController.TreasureAmt--;
	}
}
