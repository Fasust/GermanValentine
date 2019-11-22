using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HerHouse : MonoBehaviour {

    public PickUpRoad player;
    public Fireworks fireworks;
    public GameObject arrow;
    public TextMeshPro pointDisplay;
    public Animator pointDisplayAnimator;
    public float minStayTime;
    public int BASE_SCORE = 5000;
    public int SCORE_STEPS = 1000;
    private float stayTime;
    private bool won;

    void OnTriggerStay2D(Collider2D col) {
        if (stayTime < minStayTime) {
            stayTime += Time.deltaTime;
        } else {
            win(col.GetComponent<ThrownTree>());
        }
    }

    void win(ThrownTree tree) {
        if (won) {
            return;
        }
        won = true;
        fireworks.play();
        tree.settel();

        int currentPoints = 0;
        float distanz = Mathf.Abs(tree.transform.position.x - arrow.transform.position.x);
        if (distanz < 100) currentPoints = BASE_SCORE;
        if (distanz > 200) currentPoints = BASE_SCORE - SCORE_STEPS;
        if (distanz > 300) currentPoints = BASE_SCORE - SCORE_STEPS * 2;
        if (distanz > 400) currentPoints = BASE_SCORE - SCORE_STEPS * 3;
        if (distanz > 500) currentPoints = BASE_SCORE - SCORE_STEPS * 4;

		FindObjectOfType<Score>().add(currentPoints);
		pointDisplay.text = currentPoints.ToString();

        arrow.active = false;

		pointDisplayAnimator.SetTrigger("show");
		
        player.win();
    }
}
