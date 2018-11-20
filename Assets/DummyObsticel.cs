using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyObsticel : MonoBehaviour {

	public GameObject bloodParticels;

	// Use this for initialization
	void Start () {
		bloodParticels.GetComponent<ParticleSystem>().Stop();
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (!bloodParticels.GetComponent<ParticleSystem>().isPlaying)
		{
			bloodParticels.GetComponent<ParticleSystem>().Play();
		}
		
	}
}
