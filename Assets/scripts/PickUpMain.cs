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

	private bool playingWinSound = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
			carAnimator.SetTrigger("drive");
			FindObjectOfType<AudioManager>().play("Win");
			playingWinSound = true;
		}
		
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(zonePosition.position, zoneRange);
	}
}
