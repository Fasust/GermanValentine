using UnityEngine;
using System.Collections;

public class RangerController : MonoBehaviour
{

	private Vector3 initialPosition;

	private float velocity;
	public bool isFacingRight = true;

	//Finals
	public float rigthMoveDistance = 1000;
	public float leftMoveDistance = 1000;
	public float movingSpeed = 100;

	void Start()
	{
		initialPosition = transform.position;

		rigthMoveDistance = initialPosition.x + rigthMoveDistance;
		leftMoveDistance = initialPosition.x - leftMoveDistance;
	}

	void Update()
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
		GetComponent<Rigidbody2D>().velocity = new Vector2(velocity * Time.fixedDeltaTime * 100, GetComponent<Rigidbody2D>().velocity.y);
		
	}

	public void flip()
	{
		transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		isFacingRight = transform.localScale.x > 0;
	}

}
