using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    [Header("Visuals")]
    private Animator playerAnimator;
    public GameObject particalEffect;
    public Canvas hiddenDisplay;
    private camShake camShake;

    [Header("General")]
    private Movement movement;
    private PlayerState state;
    private bool pickUpTriggered = false;
    private bool detectionTriggered = false;

    void Start() {
        //Get own Components
        movement = this.GetComponent<Movement>();
        state = this.GetComponent<PlayerState>();
        playerAnimator = this.GetComponent<Animator>();

        particalEffect.GetComponent<ParticleSystem>().Stop();
        camShake = GameObject.FindGameObjectWithTag("CamController").GetComponent<camShake>();

        hiddenDisplay.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate() {
        playerAnimator.SetFloat("speed", Mathf.Abs(movement.getHorizontalMove()));
        playerAnimator.SetBool("sneak", movement.isCrouching());
        playerAnimator.SetBool("jump", movement.isAirborn());

        hiddenDisplay.enabled = state.isHidden();

        if (state.isCarrying() && !pickUpTriggered) {
            pickUpTriggered = true;
            playerAnimator.SetTrigger("carrying");
            particalEffect.GetComponent<ParticleSystem>().Play();
        }

        if (state.isDetected() && !detectionTriggered) {
            detectionTriggered = true;
            playerAnimator.SetTrigger("detect");
            particalEffect.GetComponent<ParticleSystem>().Stop();
        }

    }

    public void onLand() {
        playerAnimator.SetBool("jump", false);
        camShake.shakeTiny();

        FindObjectOfType<AudioManager>().play("Land");
    }
}
