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
    public AudioSource dogSource;
    private bool walkingSoundPlaying = false;
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
            movingSpeed = chargeSpeed;
        } else {
            movingSpeed = OG_SPEED;
        }
    }


    private void findPlayer() {
        Collider2D[] playerColliders = Physics2D.OverlapCircleAll(transform.position, viewRange, playerMask);
        PlayerState playerState = null;

        aggro = false;

        foreach (Collider2D col in playerColliders) {
            playerState = col.GetComponent<PlayerState>();

            //check if he is on eye level
            float topEyeLevel = transform.position.y + eyeLevelMargin;
            float bottomEyeLevel = transform.position.y - eyeLevelMargin;
            
            if (playerState.transform.position.y <= topEyeLevel && playerState.transform.position.y >= bottomEyeLevel) {

                //check if we are looking the right way
                if (!playerState.isHidden()) {
                    if (isFacingRight && playerState.transform.position.x >= transform.position.x) {
                        aggro = true;
                    }
                    if (!isFacingRight && playerState.transform.position.x <= transform.position.x) {
                        aggro = true;
                    }
                }
            }

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
