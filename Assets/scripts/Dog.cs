﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour {
    [Header("Movement")]
    private float OG_SPEED;
    public float movingSpeed = 100;
    public bool isFacingRight;

    [Header("Detection")]
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private float wallCheckRadius;
    [SerializeField] private Transform leftWallCheck;
    [SerializeField] private Transform rightWallCheck;
    private bool hitRight;

    [Header("Attack")]
    public float viewRange = 100;
    public float aggroDelay = 100;
    public float aggroDecay = 100;
    public float aggroTimer;
    public float relaxTimer;
    private bool aggro;
    private bool detected;
    public float chargeSpeed = 200;
    public float eyeLevelMargin = 10;
    public LayerMask playerMask;
    [SerializeField] private float attackRange;
    [SerializeField] private Transform attackPos;
    public float closeProxAggroRange = 10;

    [Header("Visual")]
    public Animator dogAnimator;
    public Canvas alertDisplay;

    [Header("Sound")]
    public AudioSource pantingSource;
    public AudioSource chargingSound;
    private bool pantingSourcePlaying = true;
    private bool chargingSoundPlaying;

    [Header("Misc")]
    private Rigidbody2D rigbody;


    void Start() {
        alertDisplay.enabled = false;
        OG_SPEED = movingSpeed;
        rigbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        float flipper = isFacingRight ? 1 : -1;

        //Applie Movement
        rigbody.velocity = new Vector2(movingSpeed * flipper, rigbody.velocity.y);
    }

    private void Update() {

        dogAnimator.SetBool("aggro", aggro || detected);

        if (detected) return;

        checkForWalls();

        if (hitRight) flip();

        //Collision when Hidden
        PlayerState player = FindObjectOfType<PlayerState>();
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), player.GetComponent<Collider2D>(), player.isHidden());

        findPlayer();
        findPlayerCloseProx();

        if (aggro) {
            charge();
        } else {
            relax();
        }

        attack();
    }

    private void findPlayerCloseProx() {
        Collider2D[] playerColliders = Physics2D.OverlapCircleAll(transform.position, closeProxAggroRange, playerMask);

        if (playerColliders.Length != 0) {
            //He is close
            PlayerState playerState = playerColliders[0].GetComponent<PlayerState>();

            //check if he hides
            if (playerState.isHidden()) return;

            raiseAggression();
        }
    }

    private void attack() {
        Collider2D[] playerColliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, playerMask);


        foreach (Collider2D col in playerColliders) {
            PlayerState playerState = col.GetComponent<PlayerState>();

            if (playerState.isHidden()) continue;
            if (!aggro) continue;

            //Detected
            detected = true;

            pantingSource.Stop();
            chargingSound.Stop();
            pantingSourcePlaying = false;
            chargingSoundPlaying = false;

            //Movement
            movingSpeed = 0;

            playerState.detect();
        }
    }

    private void relax() {
        if (!pantingSourcePlaying) {
            pantingSourcePlaying = true;
            pantingSource.Play();
        }
        if (chargingSoundPlaying) {
            chargingSoundPlaying = false;
            chargingSound.Stop();
        }

        movingSpeed = OG_SPEED;
        alertDisplay.enabled = false;
    }

    private void charge() {
        if (pantingSourcePlaying) {
            pantingSourcePlaying = false;
            pantingSource.Stop();
        }
        if (!chargingSoundPlaying) {
            chargingSoundPlaying = true;
            chargingSound.Play();
        }

        movingSpeed = chargeSpeed;
        alertDisplay.enabled = true;
    }

    private void findPlayer() {
        Collider2D[] playerColliders = Physics2D.OverlapCircleAll(transform.position, viewRange, playerMask);



        if (playerColliders.Length == 0) {
            lowerAggression();
            return;
        }

        foreach (Collider2D col in playerColliders) {
            PlayerState playerState = col.GetComponent<PlayerState>();
            Vector3 playerPos = col.transform.position;

            //check if he is on eye level
            float topEyeLevel = transform.position.y + eyeLevelMargin;
            float bottomEyeLevel = transform.position.y - eyeLevelMargin;

            if (!(playerPos.y <= topEyeLevel && playerPos.y >= bottomEyeLevel)) {
                lowerAggression();
                continue;
            }

            //check if he hides
            if (playerState.isHidden()) {
                lowerAggression();
                continue;
            }

            //check if we are looking the right way
            if ((isFacingRight && playerPos.x >= transform.position.x) ||
            (!isFacingRight && playerPos.x <= transform.position.x)) {
                //We can detect Him
                raiseAggression();
            } else {
                lowerAggression();
            }
        }
    }
    private void raiseAggression() {
        if (aggro) return;

        if (aggroTimer >= aggroDelay) {
            aggro = true;
            relaxTimer = 0;
        } else {
            aggroTimer += Time.deltaTime;
        }
    }
    private void lowerAggression() {
        if (!aggro) return;

        if (relaxTimer >= aggroDecay) {
            aggro = false;
            aggroTimer = 0;
        } else {
            relaxTimer += Time.deltaTime;
        }

    }

    private void flip() {
        //GetComponent<SpriteRenderer>().flipX = isFacingRight;
        transform.localScale =
                new Vector3(
                    -transform.localScale.x,
                    +transform.localScale.y,
                    +transform.localScale.z);

        isFacingRight = transform.localScale.x > 0;
    }

    private void checkForWalls() {
        hitRight = false;

        //Right
        Collider2D[] collidersRight = Physics2D.OverlapCircleAll(rightWallCheck.position, wallCheckRadius, whatIsWall);
        for (int i = 0; i < collidersRight.Length; i++) {
            if (collidersRight[i].gameObject != gameObject) {
                hitRight = true;
            }
        }
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, closeProxAggroRange);
    }
}
