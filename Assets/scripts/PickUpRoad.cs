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

	[Header("Visuals")]
	public Animator animator;
	private SpriteRenderer sprite;
	public GameObject hitParitecels;


	private bool moved = false;
	private bool hit = false;
	private float yMove = 0;

	private void Start()
	{
		//Sound------------------
		FindObjectOfType<AudioManager>().play("Car");

		sprite = GetComponent<SpriteRenderer>();

		buttonUp.onClick.AddListener(moveUp);
		buttonDown.onClick.AddListener(moveDown);
	}
	void Update () {

		if (!hit)
		{
			//Move ------------------
			Vector3 target = new Vector3(
				transform.position.x + speed * Time.deltaTime,
				transform.position.y + yMove,
				transform.position.z
				);
			transform.position = Vector3.Lerp(transform.position, target, 1);
		}
		

		yMove = 0;
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		if (!hit)
		{
			//Particals
			Instantiate(hitParitecels, col.transform.position , Quaternion.identity);
		}

		hit = true;
		FindObjectOfType<AudioManager>().stop("Car");
		FindObjectOfType<AudioManager>().play("Hit");
		FindObjectOfType<AudioManager>().play("Dunz");

	}
	void moveUp()
	{

		if (hit) { return; }

		//Sound------------------
		FindObjectOfType<AudioManager>().play("Button");

		//Input -----------------
		yMove = 0;

		if (transform.position.y + yMoveDistance < UpMoveLimet) //UP
		{
			//Sound
			FindObjectOfType<AudioManager>().play("Dash");

			//Animaton
			if (!animator.GetCurrentAnimatorStateInfo(0).IsName("pickup_german_turn_up"))
			{
				animator.SetTrigger("up");
			}

			//Sprite
			sprite.sortingOrder--;

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
		if (hit) { return; }

		//Sound------------------
		FindObjectOfType<AudioManager>().play("Button");

		//Input -----------------
		yMove = 0;

		if (transform.position.y - yMoveDistance > DownMoveLimet) //DOWN
		{
			//Sound
			FindObjectOfType<AudioManager>().play("Dash");

			//Animaton
			if (!animator.GetCurrentAnimatorStateInfo(0).IsName("pickup_german_turn_down"))
			{
				animator.SetTrigger("down");
			}
			//Sprite
			sprite.sortingOrder++;


			yMove = -yMoveDistance;
		}
		else
		{
			//Sound------------------
			FindObjectOfType<AudioManager>().play("Hit");
		}
	}
}
