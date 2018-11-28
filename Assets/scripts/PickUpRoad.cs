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
	public GameObject smokeBigParitecels;
	public SmoothCamera2D cam;
	public camShake camShake;

	[Header("Throw")]
	public GameObject lever;
	public GameObject throwTree;
	private bool throwing = false;

	private bool moved = false;
	private bool hit = false;
	private float yMove = 0;

	private void Start()
	{
		//Sound------------------
		FindObjectOfType<AudioManager>().play("Car");

		//Visuals------------------
		sprite = GetComponent<SpriteRenderer>();
		smokeBigParitecels.GetComponent<ParticleSystem>().Stop();

		//Buttons-----------------
		buttonUp.onClick.AddListener(moveUp);
		buttonDown.onClick.AddListener(moveDown);

		throwTree.active = false;
	}
	void Update () {

		if (!hit && !throwing)
		{
			//Move ------------------
			Vector3 target = new Vector3(
				transform.position.x + speed * Time.deltaTime,
				transform.position.y + yMove,
				transform.position.z
				);
			transform.position = Vector3.Lerp(transform.position, target, 1);


			//Throw -----------------
			if(lever.GetComponent<leverControl>().wasHit && !throwing)
			{
				throwing = true;
				cam.centerX();
				camShake.zoomOut();
				FindObjectOfType<AudioManager>().stop("Car");

				throwTree.active = true;
				throwTree.GetComponent<ThrownTree>().activateThrow();
		
			}
		}
		

		yMove = 0;
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		if (!hit)
		{
			//Particals
			Instantiate(hitParitecels, col.transform.position , Quaternion.identity);
			smokeBigParitecels.GetComponent<ParticleSystem>().Play();

			//Animation
			animator.SetTrigger("lose");
			cam.centerX();
			camShake.shakeDrive();
		}

		hit = true;
		FindObjectOfType<AudioManager>().stop("Car");
		FindObjectOfType<AudioManager>().play("Hit");
		FindObjectOfType<AudioManager>().play("Dunz");

	}
	void moveUp()
	{

		if (hit || throwing) { return; }

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
		if (hit || throwing) { return; }

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
