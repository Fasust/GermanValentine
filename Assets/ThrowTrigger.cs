using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowTrigger : MonoBehaviour {

	public GameObject leverBase;
	public GameObject lever;
	public GameObject player;
	public camShake camShake;
	public SmoothCamera2D smoothCamera;

	// Use this for initialization
	void Start () {
		lever.active = false;
		leverBase.active = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(player.transform.position.x >= this.transform.position.x)
		{
			lever.active = true;
			leverBase.active = true;

			smoothCamera.yOverwrite = 500;
			camShake.zoomOut();
		}
	}
}
