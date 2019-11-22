using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour {
    public int MILLISECOND_LOSE = 1;
    public int startScore = 10000;
    private int currentScore;
    private bool block;
    private TextMeshProUGUI ui;

    // Use this for initialization
    void Start() {
        ui = this.GetComponent<TextMeshProUGUI>();
        currentScore = startScore;

        InvokeRepeating("timeDecal", 1f, 0.1f);
    }

    // Update is called once per frame
    void FixedUpdate() {
        ui.text = currentScore.ToString();
    }

    public void add(int val) {
        if (block) return;
        currentScore += val;
    }
    public void subtract(int val) {
        if (block) return;

        if (currentScore - val < 0) {
            currentScore = 0;
        } else {
            currentScore -= val;
        }
    }
    private void timeDecal() {
        subtract(MILLISECOND_LOSE);
    }
    public void setBlocked() {
        block = true;
        this.GetComponent<TextMeshProUGUI>().color = Color.yellow;
    }
}
