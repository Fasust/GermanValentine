using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMain : MonoBehaviour {

	[Header("Detection")]
	public float zoneRange;
	public Transform zonePosition;
	public LayerMask playerMask;

	[Header("Visuals")]
	public Animator carAnimator;
	public GameObject paparticalEffectBlue;
	public GameObject paparticalEffectRed;
	public Transform particelSpawn1;
	public Transform particelSpawn2;
	public Transform particelSpawn3;
	public float paticelDelay = 0.5f;

	private float paTimeCount = 0;

	private bool playingWinSound = false;
	private bool pSpawned1 = false;
	private bool pSpawned2 = false;
	private bool pSpawned3 = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Detection ---------------------------------
		Collider2D[] playerColliders = Physics2D.OverlapCircleAll(zonePosition.position, zoneRange, playerMask);

		foreach (Collider2D col in playerColliders)
		{
			playerMovement player = col.GetComponent<playerMovement>();

			if (player.hasTree())
			{
				gameWin();
			}

		}
	}

	void gameWin()
	{
		
		if (!playingWinSound)
		{
			//carAnimator.SetTrigger("drive");
			FindObjectOfType<AudioManager>().play("Win");
			playingWinSound = true;
		}

		//Particals-------------------------------------
		paTimeCount += Time.deltaTime;

		if (paTimeCount >= paticelDelay && !pSpawned1)
		{
			pSpawned1 = true;
			Instantiate(paparticalEffectRed, particelSpawn1.transform.position, Quaternion.identity);
			FindObjectOfType<AudioManager>().play("BlowUp");
			
		}
		if (paTimeCount >= paticelDelay * 2 && !pSpawned2)
		{
			pSpawned2 = true;
			Instantiate(paparticalEffectBlue, particelSpawn2.transform.position, Quaternion.identity);
			FindObjectOfType<AudioManager>().play("BlowUp");
			
		}
		if (paTimeCount >= paticelDelay * 3 && !pSpawned3)
		{
			pSpawned3 = true;
			Instantiate(paparticalEffectRed, particelSpawn3.transform.position, Quaternion.identity);
			FindObjectOfType<AudioManager>().play("BlowUp");

			pSpawned1 = false;
			pSpawned2 = false;
			pSpawned3 = false;
			paTimeCount = 0;
		}

		
		
		
		
		

	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(zonePosition.position, zoneRange);
	}
}
