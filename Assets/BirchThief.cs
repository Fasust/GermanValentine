﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirchThief : MonoBehaviour {

	[Header("Visuals")]
	public GameObject bloodParticels;
	public Animator animator;

	[Header("Movment")]
	private bool hit = false;
	public float xspeed;
	public float yspeed;


	// Use this for initialization
	void Start () {
		bloodParticels.GetComponent<ParticleSystem>().Stop();
	}
	
	// Update is called once per frame
	void Update () {
		//Movement -----------------------
		if (!hit)
		{
			//Move 
			Vector3 target = new Vector3(
				transform.position.x + xspeed * Time.deltaTime,
				transform.position.y + yspeed * Time.deltaTime,
				transform.position.z
				);
			transform.position = Vector3.Lerp(transform.position, target, 1);
		}

	}
	void OnCollisionEnter2D(Collision2D col)
	{

		if (!hit)
		{
			bloodParticels.GetComponent<ParticleSystem>().Play();
			hit = true;
			animator.SetTrigger("lose");
		}

	}

}
