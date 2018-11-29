using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverControl : MonoBehaviour {

	public swipe swipeControl;
	public float yLimit;
	public float upSpeed;
	public float downSpeed;

	public bool wasHit = false;

	private bool movingUp = false;
	private bool movingDown = false;
	private float restingY;

	// Use this for initialization
	void Start () {
		restingY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (swipeControl.swipeUp && !movingDown &&!movingUp && !wasHit)
		{
			movingUp = true;
			wasHit = true;
			FindObjectOfType<AudioManager>().play("LeverUp");
		}

		if (movingUp)
		{
			if(transform.position.y < yLimit)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y + upSpeed * Time.deltaTime, transform.position.z);

			}
			else
			{
				transform.position = new Vector3(transform.position.x, yLimit, transform.position.z);

				movingUp = false;
				movingDown = true;
				FindObjectOfType<AudioManager>().play("LeverHit");
			}
			
		}
		if (movingDown)
		{
			if(transform.position.y > restingY)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y - downSpeed * Time.deltaTime, transform.position.z);

			}
			else
			{
				movingDown = false;
				transform.position = new Vector3(transform.position.x, restingY, transform.position.z);

			}
		}

	}
}
