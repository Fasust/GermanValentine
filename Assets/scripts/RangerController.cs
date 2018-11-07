using UnityEngine;
using System.Collections;

public class RangerController : MonoBehaviour
{
	
	[Header("Movement")]
	public bool isFacingRight = true;
	public float rigthMoveDistance = 1000;
	public float leftMoveDistance = 1000;
	public float movingSpeed = 100;

	private Vector3 initialPosition;
	private float velocity;

	[Header("Detection")]
	public float lightRange;
	public Transform lightPos;
	public LayerMask playerMask;

	void Start()
	{
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

			bool hidden = col.GetComponent<playerMovement>().isHidden();

			if (!hidden){
				
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
					isFacingRight = false;
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
					isFacingRight = true;
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
		transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		isFacingRight = transform.localScale.x > 0;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(lightPos.position, lightRange);
	}

}
