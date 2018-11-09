﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RangerController : MonoBehaviour
{
	
	[Header("Movement")]
	public bool isFacingRight = true;
	private bool nowSearching = false;
	public float rigthMoveDistance = 1000;
	public float leftMoveDistance = 1000;
	public float movingSpeed = 100;

	private Vector3 initialPosition;
	private float velocity;

	[Header("Detection")]
	public float lightRange;
	public Transform lightPos;

	public float instantDetectionRange;

	public LayerMask playerMask;
	public float SERACHING_TIME = 1;
	public float SNEAKING_DETECTION_MULTIPLIER = .5f;
	private float currentSearchingTime;
	private bool detecting = false;

	[Header("Visual")]
	public Animator rangerAnimator;
	public Canvas alertDisplay;
	public Canvas searchingDisplay;
	public Image searchingBar;

	[Header("Sound")]
	public AudioSource walkingSource;
	private bool walkingSoundPlaying = false;
	private bool searchSoundPlaying = false;

	void Start()
	{
		alertDisplay.enabled = false;
		searchingDisplay.enabled = false;
		initialPosition = transform.position;

		rigthMoveDistance = initialPosition.x + rigthMoveDistance;
		leftMoveDistance = initialPosition.x - leftMoveDistance;
	}
	void Update()
	{
		//Detection ---------------------------------
		Collider2D[] playerColliders = Physics2D.OverlapCircleAll(lightPos.position, lightRange, playerMask);
		playerMovement player = null;

		detecting = false;
		foreach (Collider2D col in playerColliders)
		{
			player = col.GetComponent<playerMovement>();

			if (!player.isHidden()){
				detecting = true;
			}

		}

		if (detecting)
		{
			//Searching
			searchForPlayer(player);
		}
		else
		{
			//Move 
			regularMove();
		}
		
	}
	private void FixedUpdate()
	{
		//Applie Movement
		GetComponent<Rigidbody2D>().velocity = new Vector2(velocity * Time.fixedDeltaTime * 100, GetComponent<Rigidbody2D>().velocity.y);

	}

	private void searchForPlayer(playerMovement player)
	{ 
		//Sound
		walkingSource.Stop();
		walkingSoundPlaying = false;
		if (!searchSoundPlaying)
		{
			FindObjectOfType<AudioManager>().play("Searching");
			searchSoundPlaying = true;
		}
		

		//Visuals
		rangerAnimator.SetBool("detecting",true);
		searchingDisplay.enabled = true;

		//Movement
		velocity = 0;

		//Instant Detect
		if(Vector2.Distance( this.transform.position, player.transform.position) <= instantDetectionRange){
			detect(player);
		}

		//Timeing
		float curMul = 1;
		if (player.isSneaking())
		{
			curMul = SNEAKING_DETECTION_MULTIPLIER;
		}
		currentSearchingTime += Time.deltaTime * curMul;

		float progress = currentSearchingTime / SERACHING_TIME;
		searchingBar.fillAmount = progress;

		float greenValue = 255 - (progress * 255);
		searchingBar.color = new Color32(255, (byte) greenValue, 0, 255);

		if (currentSearchingTime >= SERACHING_TIME)
		{		
			detect(player);
		}
}
	private void detect(playerMovement player)
	{
		//Sound
		walkingSource.Stop();
		walkingSoundPlaying = false;
	
		
		//Movement
		velocity = 0;

		searchingDisplay.enabled = false;
		rangerAnimator.SetTrigger("detect");
		player.detect();
		alertDisplay.enabled = true;
		
		
	}
	private void regularMove()
	{
		//Resets-------------------------

		//Sound
		if (!walkingSoundPlaying)
		{
			walkingSource.Play();
			walkingSoundPlaying = true;
		}
		FindObjectOfType<AudioManager>().stop("Searching");
		searchSoundPlaying = false;

		//Visual
		rangerAnimator.SetBool("detecting", false);
		searchingDisplay.enabled = false;
		currentSearchingTime = 0;

		//Movement-------------------------
		switch (isFacingRight)
		{
			case true:
				//Moving Right
				if (transform.position.x <= rigthMoveDistance)
				{
					velocity = movingSpeed;
				}
				else
				{
					flip();
					
				}
				break;
			case false:
				// Moving Left
				if (transform.position.x >= leftMoveDistance)
				{
					velocity = -movingSpeed;
				}
				else
				{
					flip();
				
				}
				break;
		}
	}
	public void flip()
	{
		if (!nowSearching)
		{
			nowSearching = true;
			rangerAnimator.SetTrigger("search");
			walkingSource.Stop();
			velocity = 0;
			return;
		}

		if (!rangerAnimator.GetCurrentAnimatorStateInfo(0).IsName("ranger_searching"))
		{

			transform.localScale =
				new Vector3(
					-transform.localScale.x,
					+transform.localScale.y,
					+transform.localScale.z);

			isFacingRight = transform.localScale.x > 0;

			walkingSource.Play();
			nowSearching = false;

		}
	
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(lightPos.position, lightRange);
	}

}
