using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownTree : MonoBehaviour {

	public float ySpeed;
	public float xSpeed;
	private bool hit = false;
	private bool throwing = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!hit && throwing)
		{
			Vector3 target = new Vector3(
				transform.position.x + xSpeed * Time.deltaTime,
				transform.position.y + ySpeed * Time.deltaTime,
				transform.position.z
				);
			transform.position = Vector3.Lerp(transform.position, target, 1);
		}
		
	}

	public void activateThrow()
	{
		throwing = true;
	}
}
