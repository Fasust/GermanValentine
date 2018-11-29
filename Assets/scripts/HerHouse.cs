using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerHouse : MonoBehaviour {

	public PickUpRoad player;
	public Fireworks fireworks;
	public GameObject arrow;
	public float minStayTime;
	private float stayTime;
	private bool won;

	void OnTriggerStay2D(Collider2D col)
	{
		if(stayTime < minStayTime)
		{
			stayTime += Time.deltaTime;
		}
		else
		{
			win(col.GetComponent<ThrownTree>());
		}
	}

	void win(ThrownTree tree)
	{
		if (won)
		{
			return;
		}
		fireworks.play();
		tree.settel();
		arrow.active = false;
		won = true;

		player.win();
	}
}
