using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpRoad : MonoBehaviour {

	[Header("Movement")]
	public float speed;
	public float yMoveDistance;
	public float UpMoveLimet;
	public float DownMoveLimet;

	[Header("Input")]
	public Button buttonUp;
	public Button buttonDown;

	private bool moved = false;
	private float yMove = 0;

	private void Start()
	{
		//Sound------------------
		FindObjectOfType<AudioManager>().play("Car");

		buttonUp.onClick.AddListener(moveUp);
		buttonDown.onClick.AddListener(moveDown);
	}
	void Update () {
		//Move ------------------
		Vector3 target =  new Vector3(
			transform.position.x + speed * Time.deltaTime, 
			transform.position.y + yMove, 
			transform.position.z
			);
		transform.position = Vector3.Lerp(transform.position, target, 1);

		yMove = 0;
	}
	void moveUp()
	{
		//Sound------------------
		FindObjectOfType<AudioManager>().play("Button");

		//Input -----------------
		yMove = 0;

		if (transform.position.y + yMoveDistance < UpMoveLimet) //UP
		{
			//Sound
			FindObjectOfType<AudioManager>().play("Dash");

			yMove = yMoveDistance;
		}
		else
		{
			//Sound------------------
			FindObjectOfType<AudioManager>().play("Hit");
		}
	}
	void moveDown()
	{
		//Sound------------------
		FindObjectOfType<AudioManager>().play("Button");

		//Input -----------------
		yMove = 0;

		if (transform.position.y - yMoveDistance > DownMoveLimet) //DOWN
		{
			//Sound
			FindObjectOfType<AudioManager>().play("Dash");

			yMove = -yMoveDistance;
		}
		else
		{
			//Sound------------------
			FindObjectOfType<AudioManager>().play("Hit");
		}
	}
}
