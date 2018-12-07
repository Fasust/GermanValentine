using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMain : MonoBehaviour {

	[Header("General")]
	public StateManagerMain stateManager;
	public TimerMain timer;
	public Fireworks fireworks;

	[Header("Detection")]
	public float zoneRange;
	public Transform zonePosition;
	public LayerMask playerMask;

	[Header("Visuals")]
	public Animator carAnimator;


	
	// Update is called once per frame
	void FixedUpdate () {
		//Detection ---------------------------------
		Collider2D[] playerColliders = Physics2D.OverlapCircleAll(zonePosition.position, zoneRange, playerMask);

		foreach (Collider2D col in playerColliders)
		{
			playerMovement player = col.GetComponent<playerMovement>();

			if (player.hasTree() && player.enabled)
			{
				fireworks.play();
				player.enabled = false;
				timer.stop();
			}

		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(zonePosition.position, zoneRange);
	}
}
