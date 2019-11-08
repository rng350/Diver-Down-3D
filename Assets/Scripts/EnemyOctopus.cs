using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOctopus : MonoBehaviour
{
	[SerializeField]
	float speed;
	[SerializeField]
	float gracePeriodTimer;
	float patrolTime;
	bool movingRight;
	GameObject diver;
	[SerializeField]
	float speedUpPerLevelMultiplier;
	[SerializeField]
	float curSpeed;

	// Start is called before the first frame update
	void Start()
	{
		GameController.OctopusAmt++;
		diver = GameObject.FindWithTag("Player");
	}

	// Update is called once per frame
	void Update()
	{
		gracePeriodTimer -= Time.deltaTime;
		SpriteBlink();
		Chase();
	}
	
	void Chase()
	{
		curSpeed = speed * speedUpFactor();

		if (diver == null)
		{
			diver = GameObject.FindWithTag("Player");
		}
		if (diver != null)
		{
			bool diverIsAtRight = diver.transform.position.x > this.transform.position.x;
			bool diverIsAbove = diver.transform.position.y > this.transform.position.y;

			if (diverIsAtRight)
			{
				if (diverIsAbove)
				{
					this.transform.position = new Vector2(this.transform.position.x + curSpeed * Time.deltaTime, this.transform.position.y + curSpeed * Time.deltaTime);
				}
				else
				{
					this.transform.position = new Vector2(this.transform.position.x + curSpeed * Time.deltaTime, this.transform.position.y - curSpeed * Time.deltaTime);
				}
			}
			else
			{
				if (diverIsAbove)
				{
					this.transform.position = new Vector2(this.transform.position.x - curSpeed * Time.deltaTime, this.transform.position.y + curSpeed * Time.deltaTime);
				}
				else
				{
					this.transform.position = new Vector2(this.transform.position.x - curSpeed * Time.deltaTime, this.transform.position.y - curSpeed * Time.deltaTime);
				}
			}
		}
	}

	void OnTriggerStay2D(Collider2D col)
	{
		Diver div = col.transform.GetComponent<Diver>();
		if (div != null)
		{
			if (!OnGracePeriod())
			{
				print("Octopus hit diver!");
				div.getHit();
			}
		}
	}
	
	bool OnGracePeriod()
	{
		if (gracePeriodTimer > 0f)
		{
			return true;
		}
		else return false;
	}

	private void FaceDirection(Vector2 direction)
	{
		if (direction == Vector2.right)
		{
			Vector3 newScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			transform.localScale = newScale;
		}
		else
		{
			Vector3 newScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			transform.localScale = newScale;
		}
	}

	void OnDestroy()
	{
		print("Octo destroyed!");
		GameController.OctopusAmt--;
	}
	private void SpriteBlink()
	{
		if (OnGracePeriod())
		{
			if (this.gameObject.GetComponent<SpriteRenderer>().enabled == true)
			{
				this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
			}
			else
			{
				this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
			}
		}
		else
		{
			this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
			return;
		}
	}

	private float speedUpFactor()
	{
		float ini = 1f;

		for (int i = 1; i < GameController.Level; i++)
		{
			ini *= speedUpPerLevelMultiplier;
		}
		return ini;
	}
}
