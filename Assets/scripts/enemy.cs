using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class enemy : MonoBehaviour {
    [Header("Stats")]
    public int MAX_HEALTH = 100;
    public int POINTS = 1000;
    public int health;
    public float playerMinRange = 200;
    [Header("World")]
    public LayerMask playerLayer;
    [Header("Visual")]
    public GameObject particalEffect;
    public Animator enemyAnimator;
    private camShake camShake;
    public Image healthBar;
    public Canvas healthDisplay;
    public GameObject points;
    public GameObject pointsGain;

    //Audio
    public float chopSoundDelay = 0.23f;

    // Use this for initialization
    void Start() {
		health = MAX_HEALTH;
        healthDisplay.enabled = false;

        points.GetComponent<TextMeshPro>().text = POINTS.ToString();
        pointsGain.GetComponent<TextMeshPro>().text = POINTS.ToString();

        points.SetActive(false);
        pointsGain.SetActive(false);

        camShake = GameObject.FindGameObjectWithTag("CamController").GetComponent<camShake>();
    }

    void Update() {
        //Check if Player in range && Not Damaged
        if (Physics2D.OverlapCircleAll(transform.position, playerMinRange, playerLayer).Length > 0 &&
         health == MAX_HEALTH
         ) {
            points.SetActive(true);
        } else {
            points.SetActive(false);
        }
    }

    public bool takeDamage(int amount) {
        //Particals
        Instantiate(particalEffect, transform.position, Quaternion.identity);

        //Animation
        bool facingRight = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().isFacingRight();
        if (facingRight) {
            enemyAnimator.SetTrigger("hit_fl");
        } else {
            enemyAnimator.SetTrigger("hit");
        }

        //screen shake
        camShake.shake();

        //Sound
        FindObjectOfType<AudioManager>().play("Chop", chopSoundDelay);

        //Damage
        health -= amount;

        //Healthbar
        if (healthDisplay.enabled == false) { healthDisplay.enabled = true; }
        healthBar.fillAmount = (float)health / (float)MAX_HEALTH;

        if (health <= 0) {
            die();
            return true;
        }
        return false;

    }

    private void die() {
		 pointsGain.SetActive(true);
        enemyAnimator.SetTrigger("die");
        healthDisplay.enabled = false;

        FindObjectOfType<Score>().add(POINTS);
    }
}
