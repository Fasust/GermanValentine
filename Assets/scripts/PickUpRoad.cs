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
	public GameObject throwableTree;
	private bool throwing = false;

	[Header("Misc")]
	public StateManagerMain StateManager;

	private bool moved = false;
	private bool hit = false;
	private bool breaking = false;
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

		throwableTree.active = false;
	}
	void Update () {

		if (!hit && !breaking)
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
				animator.SetTrigger("throw");
				
			}
		}

		yMove = 0;

	}
	void OnCollisionEnter2D(Collision2D col)
	{
		//Particals
		if (!hit)
		{
			Instantiate(hitParitecels, col.transform.position, Quaternion.identity);
			smokeBigParitecels.GetComponent<ParticleSystem>().Play();
			FindObjectOfType<AudioManager>().play("Hit");

			lose();
		}
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
	public void throwTree()
	{
		
		cam.centerX();
		FindObjectOfType<AudioManager>().stop("Car");

		throwableTree.active = true;
		throwableTree.GetComponent<ThrownTree>().activateThrow();

		breaking = true;
	}
	public void win()
	{
		animator.SetTrigger("win");
	}
	public void lose()
	{
		if (!hit)
		{
			//Animation
			if (throwing)
			{
				animator.SetTrigger("loseNoTree");
			}
			else
			{
				animator.SetTrigger("lose");
			}
			
			cam.centerX();
			camShake.shakeDrive();

			FindObjectOfType<AudioManager>().stop("Car");
			FindObjectOfType<AudioManager>().play("Dunz");

			StateManager.showReplay();
		}

		hit = true;
		
	}
}
