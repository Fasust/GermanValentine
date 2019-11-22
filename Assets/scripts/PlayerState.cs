using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {
    [Header("Hiding")]
    public LayerMask hideMask;

    [Header("General")]
    public StateManagerMain StateManager;
    public TimerMain timer;
    private Movement movement;

    [Header("Internal")]
    private bool carrying;
    private bool detected;

    void Start(){
         movement = this.GetComponent<Movement>();
    }
    
    public void makeCarry() {
        carrying = true;
        FindObjectOfType<AudioManager>().play("Pickup");
    }

    public void detect(){
        detected = true;
        FindObjectOfType<AudioManager>().play("Gameover");
        StateManager.showReplay();
        movement.setBlocked(true);
        timer.stop();
    }

    public bool isHidden() {
        return movement.isCrouching() && GetComponent<Collider2D>().IsTouchingLayers(hideMask);
    }

    public bool isCarrying() => carrying;
    public bool isCrouching() => movement.isCrouching();
    public bool isDetected() => detected;
}
