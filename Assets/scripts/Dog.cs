using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour {
    [Header("Movement")]
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
    public LayerMask coliderMask;
    [Header("Visual")]
    public Animator dogAnimator;

    [Header("Sound")]
    public AudioSource dogSource;
    private bool walkingSoundPlaying = false;
    [Header("Misc")]
    private Rigidbody2D rigbody;


    void Start() {
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
