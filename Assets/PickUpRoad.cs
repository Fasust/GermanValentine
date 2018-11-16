using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRoad : MonoBehaviour {
	public float speed;

	// Update is called once per frame
	void Update () {
		Vector3 target;

		target =  new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);

		transform.position = Vector3.Lerp(transform.position, target, 1);
	}
}
