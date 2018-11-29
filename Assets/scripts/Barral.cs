using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barral : MonoBehaviour {

	[Header("Visuals")]
	public Animator animator;
	public GameObject player;
	public GameObject playerAncor;
	public GameObject ancor;
	private SpriteRenderer sprite;

	[Header("Movment")]
	private bool hit = false;
	public float yspeed;

	[Header("Audio")]
	public AudioSource audio;

	// Use this for initialization
	void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		//Movement -----------------------
		if (!hit)
		{
			//Move 
			Vector3 target = new Vector3(
				transform.position.x,
				transform.position.y + yspeed * Time.deltaTime,
				transform.position.z
				);
			transform.position = Vector3.Lerp(transform.position, target, 1);
		}

		//Render ---------------------------
		if (playerAncor.transform.position.y > ancor.transform.position.y)
		{
			sprite.sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder - 1;
		}
		else
		{
			sprite.sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder + 1;
		}

	}
	void OnCollisionEnter2D(Collision2D col)
	{

		if (!hit)
		{
			hit = true;
			animator.SetTrigger("hit");
			FindObjectOfType<AudioManager>().play("Hit");
			audio.Stop();
		}

	}
}
