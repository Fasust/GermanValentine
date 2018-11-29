using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrezelTruck : MonoBehaviour
{

	[Header("Movement")]
	private bool hit;
	public float speed;
	private float yMove;

	[Header("Horn")]
	public Transform detectionPos;
	public float detectionYRange;
	public GameObject player;
	public AudioSource horn;
	private bool honked = false;

	public GameObject smokeBigParitecels;

	// Use this for initialization
	void Start()
	{
		smokeBigParitecels.GetComponent<ParticleSystem>().Stop();
	}

	// Update is called once per frame
	void Update()
	{
		//Horn----------------------
		if (player.transform.position.x >= detectionPos.transform.position.x &&
			player.transform.position.x <= this.transform.position.x &&
			player.transform.position.y <= this.transform.position.y + detectionYRange &&
			player.transform.position.y >= this.transform.position.y - detectionYRange &&
			!honked)
		{
			horn.Play();
			honked = true;
		}

		//Movement -----------------------
		if (!hit)
		{
			//Move 
			Vector3 target = new Vector3(
				transform.position.x + -speed * Time.deltaTime,
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
			smokeBigParitecels.GetComponent<ParticleSystem>().Play();
			FindObjectOfType<AudioManager>().play("Hit");
			hit = true;
		}
		
	}
}
