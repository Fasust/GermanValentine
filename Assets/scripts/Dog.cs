using System;
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
    public bool hitLeft;
    public bool hitRight;

    [Header("Attack")]
    public float viewRange = 100;
    private bool aggro;
    public float chargeSpeed = 200;
    public float eyeLevelMargin = 10;
    public LayerMask playerMask;

    [Header("Visual")]
    public Animator dogAnimator;

    [Header("Sound")]
    public AudioSource pantingSource;
    public AudioSource chargingSound;
    private bool pantingSourcePlaying = true;
    private bool chargingSoundPlaying;

    [Header("Misc")]
    private Rigidbody2D rigbody;


    void Start() {
        OG_SPEED = movingSpeed;
        rigbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        float flipper = isFacingRight ? 1 : -1;

        //Applie Movement
        rigbody.velocity = new Vector2(movingSpeed * flipper, rigbody.velocity.y);
    }

    private void Update() {
        checkForWalls();

        if (hitRight && isFacingRight) flip();

        if (hitLeft && !isFacingRight) flip();

        findPlayer();

        dogAnimator.SetBool("aggro", aggro);

        if (aggro) {
            charge();
        } else {
            relax();
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
    }

    private void findPlayer() {
        Collider2D[] playerColliders = Physics2D.OverlapCircleAll(transform.position, viewRange, playerMask);

        aggro = false;

        foreach (Collider2D col in playerColliders) {
            PlayerState playerState = col.GetComponent<PlayerState>();
            Vector3 playerPos = col.transform.position;

            //check if he is on eye level
            float topEyeLevel = transform.position.y + eyeLevelMargin;
            float bottomEyeLevel = transform.position.y - eyeLevelMargin;

            if (!(playerPos.y <= topEyeLevel && playerPos.y >= bottomEyeLevel)) continue;

            //check if he hides
            if (playerState.isHidden()) continue;

            //check if we are looking the right way
            if (isFacingRight && playerPos.x >= transform.position.x) aggro = true;

            if (!isFacingRight && playerPos.x <= transform.position.x) aggro = true;
        }
    }

    private void flip() {
        isFacingRight = !isFacingRight;
        GetComponent<SpriteRenderer>().flipX = isFacingRight;
    }

    private void checkForWalls() {
        hitRight = false;
        hitLeft = false;

        //left
        Collider2D[] collidersLeft = Physics2D.OverlapCircleAll(leftWallCheck.position, wallCheckRadius, whatIsWall);
        for (int i = 0; i < collidersLeft.Length; i++) {
            if (collidersLeft[i].gameObject != gameObject) {
                hitLeft = true;
            }
        }

        //Right
        Collider2D[] collidersRight = Physics2D.OverlapCircleAll(rightWallCheck.position, wallCheckRadius, whatIsWall);
        for (int i = 0; i < collidersRight.Length; i++) {
            if (collidersRight[i].gameObject != gameObject) {
                hitRight = true;
            }
        }
    }
}
