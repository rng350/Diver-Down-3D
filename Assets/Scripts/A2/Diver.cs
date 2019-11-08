using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diver : MonoBehaviour
{
	float dMoveSpeed;
	float dSwimUpForce;

	[SerializeField]
	float dMoveSpeedNormal;

	[SerializeField]
	float dSwimUpForceNormal;
	
	[SerializeField]
	float dSwimUpForceSpecial;
	[SerializeField]
	float dSwimSpeedSpecial;

	[SerializeField]
	float swimUpCooldownTimer;
	[SerializeField]
	float swimUpCooldownTimerCur;

	// Invincibility timer
	[SerializeField]
	float dInvincibleTimer;
	[SerializeField]
	float startInvincibleTimer;
	public float dInvincibleTimerCur;

	[SerializeField]
	float superTimer;
	[SerializeField]
	float superTimerCur;
	[SerializeField]
	float superTimerCooldown;

	// states for animator
	bool isSwimming;
	bool isSwimmingFast;
	bool isSwimmingUp;

	// References to other components and game objects
	Animator dAnimator;
	Rigidbody2D dRigidBody2D;
	Transform dSpriteChild;
	Transform dGroundCheck;
	
	Vector2 dFacingDirection;

	[SerializeField]
	public GameObject[] ghosts = new GameObject[2];

	AudioSource hurtSound;

	[SerializeField]
	GameObject deathSound;
	
	[SerializeField]
	float swimSpeedFactorTreasureChest;
	[SerializeField]
	float swimSpeedFactorCoinBag;
	[SerializeField]
	float swimSpeedFactorSingleCoin;

	// Start is called before the first frame update
	void Start()
	{
		// Get references to other components and game objects
		dAnimator = GetComponent<Animator>();
		dRigidBody2D = GetComponent<Rigidbody2D>();

		// initialize facing direction
		dFacingDirection = Vector2.right;

		dMoveSpeed = dMoveSpeedNormal;
		dSwimUpForce = dSwimUpForceNormal;

		swimUpCooldownTimerCur = 0f;
		superTimerCur = 0f;
		
		ghosts[0] = null;
		ghosts[1] = null;

		dInvincibleTimerCur = startInvincibleTimer;

		hurtSound = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update()
    {
		GameController.superTimer = superTimer - superTimerCur;

		if (swimUpCooldownTimerCur > 0f)
		{
			swimUpCooldownTimerCur -= Time.deltaTime;
		}

		if (dInvincibleTimerCur > 0f)
		{
			dInvincibleTimerCur -= Time.deltaTime;
		}
		if ((superTimerCur > 0f) && !(Input.GetKey(KeyCode.LeftControl)))
		{
			superTimerCur -= Time.deltaTime / superTimerCooldown;
		}

		SpriteBlink();

		// false by default
		isSwimming = false;
		isSwimmingUp = false;

		// to prevent diver from pushing past top & bottom colliders
		bool diverWithinVerticalBounds = transform.position.y < 450f && transform.position.y > -300f;

		if ((Input.GetKey("up")) && (swimUpCooldownTimerCur <= 0f) && diverWithinVerticalBounds)
		{
			dRigidBody2D.AddForce(Vector2.up * dSwimUpForce, ForceMode2D.Impulse);
			//transform.Translate(Vector2.up * dMoveSpeed * Time.deltaTime);
			swimUpCooldownTimerCur = swimUpCooldownTimer;
			isSwimming = true;
			isSwimmingUp = true;
		}
		if (Input.GetKey("down") && diverWithinVerticalBounds)
		{
			dRigidBody2D.AddForce(Vector2.down * dSwimUpForce * Time.deltaTime, ForceMode2D.Impulse);
			isSwimming = true;
		}
		if (Input.GetKey("left"))
		{
			transform.Translate(Vector2.left * dMoveSpeed * calcSwimSpeedFactor() * Time.deltaTime);
			FaceDirection(Vector2.left);
			isSwimming = true;
		}
		if (Input.GetKey("right"))
		{
			transform.Translate(Vector2.right * dMoveSpeed * calcSwimSpeedFactor() * Time.deltaTime);
			FaceDirection(Vector2.right);
			isSwimming = true;
		}
		if (Input.GetKey(KeyCode.Space))
		{
			DropTreasure();
		}
		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			if ((GameController.isSpecialMode) && (superTimerCur < superTimer))
			{
				dMoveSpeed = dSwimSpeedSpecial;
				dSwimUpForce = dSwimUpForceSpecial;
				isSwimmingFast = true;
			}
		}
		if ((Input.GetKeyUp(KeyCode.LeftControl)) || (superTimerCur >= superTimer))
		{
			dMoveSpeed = dMoveSpeedNormal;
			dSwimUpForce = dSwimUpForceNormal;
			isSwimmingFast = false;
		}
		if (Input.GetKey(KeyCode.LeftControl) && (superTimerCur < superTimer))
		{
			superTimerCur += Time.deltaTime;
			isSwimmingFast = true;
		}

		dAnimator.SetBool("isSwimming", isSwimming);
		dAnimator.SetBool("isSwimmingUp", isSwimmingUp);
		dAnimator.SetBool("isSwimmingFast", isSwimmingFast);
	}


	private void FaceDirection(Vector2 direction)
	{
		dFacingDirection = direction;

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

	// how carrying loot affects horizontal swimming
	private float calcSwimSpeedFactor()
	{
		float defaultFactor = 1f;

		Treasure loot = this.transform.GetComponentInChildren<Treasure>();
		if (loot != null)
		{
			if (loot.value == 1)
			{
				return swimSpeedFactorSingleCoin;
			}
			else if (loot.value == 2)
			{
				return swimSpeedFactorCoinBag;
			}
			else if (loot.value == 10)
			{
				return swimSpeedFactorTreasureChest;
			}
		}
		return defaultFactor;
	}

	public void getHit()
	{
		DropTreasure();

		if (dInvincibleTimerCur <= 0f)
		{
			GameController.tanksRemaining--;

			if (GameController.tanksRemaining > 0)
			{
				print("Ouch!");
				hurtSound.Play();
				//isHurt = true;
				dInvincibleTimerCur = dInvincibleTimer; // start invincibility timer
			}
			else
			{
				Instantiate(deathSound);
				Die();
			}
		}
	}

	private void DropTreasure()
	{
		Treasure loot = this.transform.GetComponentInChildren<Treasure>();
		if (loot != null)
		{
			Rigidbody2D diverRigid = this.transform.GetComponent<Rigidbody2D>();
			diverRigid.mass -= loot.mass;
			this.transform.DetachChildren();
		}
		else
		{
			print("No treasure to drop!");
		}
	}

	public bool hasTreasure()
	{
		Treasure loot = this.transform.GetComponentInChildren<Treasure>();
		return loot != null;
	}

	private void Die()
	{
		GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject ghost in ghosts)
		{
			Destroy(ghost);
		}
		GameController.livesRemaining--;
		GameController.DiverAmt = 0;
		print(this.transform.gameObject.name + " is dead!");
	}
	
	private void SpriteBlink()
	{
		if (dInvincibleTimerCur > 0f)
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

	private void OnCollisionEnter2D(Collision2D collision)
	{
		PlatformEffector2D bottomPlat = collision.transform.GetComponent<PlatformEffector2D>();
		bool isBottom = bottomPlat != null;
		if (isBottom)
		{
			dRigidBody2D.gravityScale = 1f;
		}
	}

	// Testing for ground
	/*
	private void OnCollisionStay2D(Collision2D collision)
	{
		PlatformEffector2D plat = collision.transform.GetComponent<PlatformEffector2D>();
		if (plat != null)
		{
			dRigidBody2D.AddForce(-Physics.gravity, ForceMode2D.Force);
		}
	}
	*/
}
