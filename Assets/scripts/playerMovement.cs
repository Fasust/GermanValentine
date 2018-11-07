using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
	public CharacterController2D controller;

	[Header("Movement")]
	public float runSpeed = 40f;
	public float carryMultiplier = 1.5f;

	[Header("Animation")]
	public Animator playerAnimator;
	public GameObject particalEffect;

	[Header("Input")]
	public Joystick joystick;
	public float moveSensetivety = .2f;
	public float sneakSensetivety = .5f;

	[Header("Hiding")]
	public LayerMask hideMask;

	private float horizontalMove = 0f;
	private bool sneak = false;
	private bool soundPlaying = false;
	private bool particelsActive = false;
	private bool detectedAnimationStarted = false;


	private void Start()
	{
		particalEffect.GetComponent<ParticleSystem>().Stop();
	}

	void Update()
	{

		// Movement -------------------------------------------------------
		horizontalMove = joystick.Horizontal;
		if(Mathf.Abs(joystick.Horizontal) >= moveSensetivety)
		{
			if (joystick.Horizontal > 0)
			{
				horizontalMove = runSpeed;
			}
			else
			{
				horizontalMove = -runSpeed;
			}
		}
		else
		{
			horizontalMove = 0;
		}
		
		//Sneak -------------------------------------------------------------
		if(joystick.Vertical <= sneakSensetivety)
		{
			sneak = true;
		}
		else
		{
			sneak = false;
		}

		//Update Animator-----------------------------------------------------
		playerAnimator.SetFloat("speed", Mathf.Abs(horizontalMove));
		playerAnimator.SetBool("sneak", sneak);
	}

	void FixedUpdate() {

		//Get Animation States
		bool chopping = playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("german_chop");
		bool carrying = playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("german_carrying");
		bool beingATree = playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("german_carrying_sneak");
		bool detected = playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("german_detected");

		//Special cases ----------------------------------------------

		if (chopping || beingATree || detected)
		{
			horizontalMove = 0;
		}

		//When Carrying Display Leaves and make immobile when sneaking
		if (carrying)
		{
			if (!particelsActive)
			{
				particalEffect.GetComponent<ParticleSystem>().Play();
				particelsActive = true;
			}
		}

		//Movement -----------------------------------------------------

		controller.Move(horizontalMove * runSpeed * Time.fixedDeltaTime, sneak, false);

		//Sound --------------------------------------------------------
		if (horizontalMove != 0)
		{
			if (!soundPlaying)
			{
				soundPlaying = true;
				FindObjectOfType<AudioManager>().play("Step");
			}

		}
		else
		{
			soundPlaying = false;
			FindObjectOfType<AudioManager>().stop("Step");
		}
	
		//End Step Sound when sneaking
		if (sneak)
		{
			soundPlaying = false;
			FindObjectOfType<AudioManager>().stop("Step");
		}

	}

	public void makeCarry()
	{
		
		runSpeed *= carryMultiplier;
		playerAnimator.SetTrigger("carrying");

	}

	public bool isSneaking()
	{
		return sneak;
	}

	public bool isHidden()
	{
		return sneak && GetComponent<Collider2D>().IsTouchingLayers(hideMask);
	}

	public void detect()
	{
		if (!detectedAnimationStarted)
		{
			playerAnimator.SetTrigger("detect");
			detectedAnimationStarted = true;
		}
		
	}

}
