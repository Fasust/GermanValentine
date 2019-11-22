using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy : MonoBehaviour {

	public int MAX_HEALTH = 100;
	public int health;

	//Visuals
	public GameObject particalEffect;
	public Animator enemyAnimator;
	private camShake camShake;
	public Image healthBar;
	public Canvas display;

	//Audio
	public float chopSoundDelay = 0.23f;

	// Use this for initialization
	void Start () {
		
		health = MAX_HEALTH;
		display.enabled = false;
		camShake = GameObject.FindGameObjectWithTag("CamController").GetComponent<camShake>();
	}

	public bool takeDamage(int amount){
		//Particals
		Instantiate(particalEffect, transform.position, Quaternion.identity);

		//Animation
		bool facingRight = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().isFacingRight();
		if (facingRight)
		{
			enemyAnimator.SetTrigger("hit_fl");
		}
		else
		{
			enemyAnimator.SetTrigger("hit");
		}

		//screen shake
		camShake.shake();

		//Sound
		FindObjectOfType<AudioManager>().play("Chop", chopSoundDelay);

		//Damage
		health -= amount;

		//Healthbar
		if(display.enabled == false) { display.enabled = true; }
		healthBar.fillAmount = (float) health / (float) MAX_HEALTH;

		if(health <= 0){
			die();
			return true;
		}
		return false;
		
	}

	private void die(){
		enemyAnimator.SetTrigger("die");
		display.enabled = false;
	}
}
