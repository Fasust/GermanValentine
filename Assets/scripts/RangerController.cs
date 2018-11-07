using UnityEngine;
using System.Collections;

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
	public LayerMask playerMask;

	[Header("Visual")]
	public Animator rangerAnimator;
	public Canvas display;

	[Header("Sound")]
	public AudioSource walkingSource;

	void Start()
	{
		display.enabled = false;
		initialPosition = transform.position;

		rigthMoveDistance = initialPosition.x + rigthMoveDistance;
		leftMoveDistance = initialPosition.x - leftMoveDistance;
	}

	void Update()
	{
		//Detection ---------------------------------
		Collider2D[] playerColliders = Physics2D.OverlapCircleAll(lightPos.position, lightRange, playerMask);
	
		foreach (Collider2D col in playerColliders)
		{
			playerMovement player = col.GetComponent<playerMovement>();

			if (!player.isHidden()){
				rangerAnimator.SetTrigger("detect");
				player.detect();
				display.enabled = true;
				velocity = 0;
				walkingSource.Stop();
				return;
			}

		}

		regularMove();
	}

	private void regularMove()
	{
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

	private void FixedUpdate()
	{
		//Applie Movement
		GetComponent<Rigidbody2D>().velocity = new Vector2(velocity * Time.fixedDeltaTime * 100, GetComponent<Rigidbody2D>().velocity.y);
		
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
