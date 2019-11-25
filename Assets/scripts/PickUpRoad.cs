using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PickUpRoad : MonoBehaviour {

    [Header("Movement")]
    public float BASE_SPEED = 400;
    public float SPEED_LIMIT = 1000;
    private float currentSpeed;
    public float acceloration;
    public float breakSpeed;
    public float yMoveDistance;
    public float UpMoveLimet;
    public float DownMoveLimet;

    [Header("Input")]
    public Button buttonUp;
    public Button buttonDown;
    private GasPaddle paddle;

    [Header("Visuals")]
    public Animator animator;
    private SpriteRenderer sprite;
    public GameObject hitParitecels;
    public GameObject smokeBigParitecels;
    public SmoothCamera2D cam;
    public camShake camShake;

    [Header("Throw")]
    public GameObject lever;
    public GameObject throwableTree;
    private bool throwing = false;

    [Header("Misc")]
    public StateManagerMain StateManager;

    private bool moved = false;
    private bool hit = false;
    private bool breaking = false;
    private float yMove = 0;

    [Header("Sound")]
    public float pitchIncrease = .1f;
    public float pitchFallOff = .1f;
    private float carPitch = 1f;
    private float BASE_CAR_PITCH = 1f;
    private float MAX_CAR_PITCH = 1.8f;

    private void Start() {
        //Sound------------------
        FindObjectOfType<AudioManager>().play("Car");

        //Visuals------------------
        sprite = GetComponent<SpriteRenderer>();
        smokeBigParitecels.GetComponent<ParticleSystem>().Stop();

        //Buttons-----------------
        buttonUp.onClick.AddListener(moveUp);
        buttonDown.onClick.AddListener(moveDown);

        throwableTree.active = false;
        currentSpeed = BASE_SPEED;

        paddle = FindObjectOfType<GasPaddle>();
    }
    void Update() {
        if (!hit && !breaking) {
            //Speed------------------
            if (paddle.pressed) {
                if (currentSpeed + acceloration * Time.deltaTime >= SPEED_LIMIT) {
                    currentSpeed = SPEED_LIMIT;
                } else {
                    currentSpeed += acceloration * Time.deltaTime;
                }
                currentSpeed += acceloration * Time.deltaTime;
            } else {
                if (currentSpeed - breakSpeed * Time.deltaTime < BASE_SPEED) {
                    currentSpeed = BASE_SPEED;
                } else {
                    currentSpeed -= breakSpeed * Time.deltaTime;

                }

            }
            print(currentSpeed);

            //Sound ------------
            if (paddle.pressed) {
                if (carPitch + pitchIncrease > MAX_CAR_PITCH) {
                    FindObjectOfType<AudioManager>().setPitch("Car", MAX_CAR_PITCH);
                } else {
                    FindObjectOfType<AudioManager>().setPitch("Car", carPitch += pitchIncrease);
                }

            } else {
                if (carPitch - pitchFallOff < BASE_CAR_PITCH) {
                    FindObjectOfType<AudioManager>().setPitch("Car", BASE_CAR_PITCH);
                } else {
                    FindObjectOfType<AudioManager>().setPitch("Car", carPitch -= pitchFallOff);
                }

            }

            //Move ------------------
            Vector3 target = new Vector3(
                transform.position.x + currentSpeed * Time.deltaTime,
                transform.position.y + yMove,
                transform.position.z
                );
            transform.position = Vector3.Lerp(transform.position, target, 1);


            //Throw -----------------
            if (lever.GetComponent<leverControl>().wasHit && !throwing) {
                throwing = true;
                animator.SetTrigger("throw");

            }
        }
        yMove = 0;

    }
    void OnCollisionEnter2D(Collision2D col) {
        //Particals
        if (!hit) {
            Instantiate(hitParitecels, col.transform.position, Quaternion.identity);
            smokeBigParitecels.GetComponent<ParticleSystem>().Play();
            FindObjectOfType<AudioManager>().play("Hit");

            lose();
        }
    }
    void moveUp() {

        if (hit || throwing) { return; }

        //Sound------------------
        FindObjectOfType<AudioManager>().play("Button");

        //Input -----------------
        yMove = 0;

        if (transform.position.y + yMoveDistance < UpMoveLimet) //UP
        {
            //Sound
            FindObjectOfType<AudioManager>().play("Dash");

            //Animaton
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("pickup_german_turn_up")) {
                animator.SetTrigger("up");
            }

            //Sprite
            sprite.sortingOrder--;

            yMove = yMoveDistance;
        } else {
            //Sound------------------
            FindObjectOfType<AudioManager>().play("Hit");
        }
    }
    void moveDown() {
        if (hit || throwing) { return; }

        //Sound------------------
        FindObjectOfType<AudioManager>().play("Button");

        //Input -----------------
        yMove = 0;

        if (transform.position.y - yMoveDistance > DownMoveLimet) //DOWN
        {
            //Sound
            FindObjectOfType<AudioManager>().play("Dash");

            //Animaton
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("pickup_german_turn_down")) {
                animator.SetTrigger("down");
            }
            //Sprite
            sprite.sortingOrder++;


            yMove = -yMoveDistance;
        } else {
            //Sound------------------
            FindObjectOfType<AudioManager>().play("Hit");
        }
    }

    public void throwTree() {

        cam.centerX();
        FindObjectOfType<AudioManager>().stop("Car");

        throwableTree.active = true;
        throwableTree.GetComponent<ThrownTree>().activateThrow();

        breaking = true;
    }
    public void win() {
        FindObjectOfType<TimerMain>().stop();
        animator.SetTrigger("win");
    }
    public void lose() {
        if (!hit) {
            //Animation
            if (throwing) {
                animator.SetTrigger("loseNoTree");
            } else {
                animator.SetTrigger("lose");
            }

            cam.centerX();
            camShake.shakeDrive();

            FindObjectOfType<AudioManager>().stop("Car");
            FindObjectOfType<AudioManager>().play("Dunz");

            FindObjectOfType<TimerMain>().stop();
            StateManager.showReplay();
        }

        hit = true;

    }
}
