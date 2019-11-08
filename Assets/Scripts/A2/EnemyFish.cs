using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFish : MonoBehaviour
{
	[SerializeField]
	float speed;
	[SerializeField]
	float gracePeriodTimer;
	float patrolTime;
	bool movingRight;
	[SerializeField]
	public float enemyDestroyTimer;
	public GameObject[] ghosts = new GameObject[2];
	[SerializeField]
	float speedUpPerLevelMultiplier;
	[SerializeField]
	float curSpeed;
	
	Animator fAnimator;

	// Start is called before the first frame update
	void Start()
	{
		patrolTime = Random.Range(3, 8);
		bool movingRight = Random.Range(0, 2) == 1 ? true : false;
		ghosts[0] = null;
		ghosts[1] = null;
		fAnimator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
		curSpeed = speed * speedUpFactor();

		if (gracePeriodTimer > 0f)
		{
			gracePeriodTimer -= Time.deltaTime;
		}
		SpriteBlink();

		enemyDestroyTimer -= Time.deltaTime;

		if (enemyDestroyTimer <= 0f)
		{
			if (ghosts[0] != null)
			{
				Destroy(ghosts[0]);
				Destroy(ghosts[1]);
				Destroy(this.gameObject);
				GameController.EnemiesAmt--;
				print(this.name + " no longer exists!");
			}
		}
		Patrol();
    }

	// TODO: patrol script
	void Patrol()
	{
		if (patrolTime <= 0f)
		{
			// set new patrol time
			patrolTime = Random.Range(3, 8);
			// swap moving direction
			movingRight = movingRight ? false : true;
		}
		else
		{
			if (movingRight)
			{
				transform.position = new Vector2(transform.position.x + curSpeed * Time.deltaTime, transform.position.y);
				FaceDirection(Vector2.right);
			}
			else
			{
				transform.position = new Vector2(transform.position.x - curSpeed * Time.deltaTime, transform.position.y);
				FaceDirection(Vector2.left);
			}
			patrolTime -= Time.deltaTime;
		}
	}
	
	void OnTriggerStay2D(Collider2D col)
	{
		Diver div = col.transform.GetComponent<Diver>();
		if (div != null)
		{
			if (!OnGracePeriod())
			{
				print("Fish hit diver!");
				div.getHit();
			}
		}
	}

	// todo: grace period
	bool OnGracePeriod()
	{
		if (gracePeriodTimer > 0f)
		{
			return true;
		}
		else
		{
			return false;
		}

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

	private void SpriteBlink()
	{
		if (gracePeriodTimer > 0f)
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

		for (int i = 0; i < GameController.Level; i++)
		{
			ini *= speedUpPerLevelMultiplier;
		}
		return ini;
	}
}
