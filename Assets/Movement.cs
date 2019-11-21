﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Movement : MonoBehaviour {
    [Header("Horizontal Movement")]
    public float walkSpeed = 40f;

    [Header("Vertical Movement")]
    public float jumpForce = 400f;
    public float stumpForce = 100f;
    public float fallMultiplier = 2.5f;

    [Header("General Movement")]
    [Range(0, 1)] public float crouchSpeedMultiplier = .36f;
    [Range(0, .3f)] public float movementSmoothing = .05f;

    [Header("World")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;

    [Header("Landing")]
    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    [Header("Input")]
    public Joystick joystick;
    public KeyboardInput keyboard;
    public float moveSensitivity = .2f;
    public float crouchSensitivity = .5f;
    public float jumpSensitivity = .5f;

    [Header("Internal")]
    const float groundedRadius = 1f;
    private Vector3 refVector = Vector3.zero;
    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    private Rigidbody2D m_Rigidbody2D;
    private bool facingRight = true;
    private bool grounded = true;
    private bool blocked = false;


    // Start is called before the first frame update
    void Awake() {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null) OnLandEvent = new UnityEvent();
    }

    void FixedUpdate() {
        checkIfGrounded();
    }

    void Update() {
        getInputs();

        if (blocked) return;
        move();
        changeDirection();
    }
    private void getInputs() {
        horizontalMove = keyboard.Horizontal;
        if (Mathf.Abs(horizontalMove) >= moveSensitivity) {
            if (horizontalMove > 0) horizontalMove = walkSpeed;
            else horizontalMove = -walkSpeed;

        } else {
            horizontalMove = 0;
        }

        if (keyboard.Vertical >= jumpSensitivity) verticalMove = keyboard.Vertical;
        else if (keyboard.Vertical <= crouchSensitivity) verticalMove = keyboard.Vertical;
        else verticalMove = 0;
    }

    private void move() {
        // If crouching
        if (isCrouching()) horizontalMove *= crouchSpeedMultiplier;

        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(horizontalMove, m_Rigidbody2D.velocity.y);
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(
            m_Rigidbody2D.velocity,
            targetVelocity,
            ref refVector,
            movementSmoothing
        );

        //Faster Fall
        if (m_Rigidbody2D.velocity.y < 0) {
            m_Rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

            //Canceling the Jump
        } else if (m_Rigidbody2D.velocity.y > 0 && verticalMove <= 0) {
            m_Rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        if (isStartingJump()) {
            grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, jumpForce));
        }
        if (isStumping()) m_Rigidbody2D.AddForce(new Vector2(0f, -stumpForce));
    }

    private void checkIfGrounded() {
        bool wasGrounded = grounded;
        grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++) {
            if (colliders[i].gameObject != gameObject) {
                grounded = true;
                if (!wasGrounded && verticalMove <= 0)
                    OnLandEvent.Invoke();
            }
        }
    }


    private void changeDirection() {
        if (horizontalMove > 0 && !facingRight) Flip();
        else if (horizontalMove < 0 && facingRight) Flip();
    }

    private void Flip() {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void setBlocked(bool val) => blocked = val;

    public bool isFacingRight() => facingRight;
    public bool isStartingJump() => grounded && verticalMove > 0;
    public bool isJumping() => !grounded && verticalMove > 0;
    public bool isAirborn() => !grounded;
    public bool isStumping() => !grounded && verticalMove < 0;
    public bool isCrouching() => grounded && verticalMove < 0;

    public float getHorizontalMove() => horizontalMove;
    public float getVerticalMove() => verticalMove;


}