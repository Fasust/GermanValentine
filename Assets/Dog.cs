using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour {
    [Header("Movement")]
    public bool isFacingRight = true;
    public float movingSpeed = 100;
    private Vector3 initialPosition;

    [Header("Attack")]
    public LayerMask playerMask;
    public LayerMask coliderMask;
    [Header("Visual")]
    public Animator dogAnimator;

    [Header("Sound")]
    public AudioSource dogSource;
    private bool walkingSoundPlaying = false;

    void Start() {
        initialPosition = transform.position;
    }

    private void FixedUpdate() {
        //Applie Movement
        GetComponent<Rigidbody2D>().velocity = new Vector2(movingSpeed * 100 * Time.fixedDeltaTime, GetComponent<Rigidbody2D>().velocity.y);

    }
}
