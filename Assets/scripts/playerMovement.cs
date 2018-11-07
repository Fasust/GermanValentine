using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
	public CharacterController2D controller;

	//Finals
	public float runSpeed = 40f;
	public float sprintMultiplier = 1.5f;
	public float moveSensetivety = .2f;
	public float sneakSensetivety = .5f;

	//Animation
	public Animator playerAnimator;
	public GameObject particalEffect;

	//Input
	public Joystick joystick;

	private float horizontalMove = 0f;
	private bool sneak = false;
	private bool soundPlaying = false;
	private bool particelsActive = false;


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

		//Special cases ----------------------------------------------

		//When Chopping block the movement
		if (chopping)
		{
			horizontalMove = 0;
		}

		//When Carrying Block Sneaking and Display Leaves
		if (carrying)
		{
			sneak = false;
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
		
		runSpeed *= sprintMultiplier;
		playerAnimator.SetTrigger("carrying");

	}

}
