using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour {
    public int MILLISECOND_LOSE = 1;
    public int startScore = 10000;
    private int currentScore;
    private bool block;
    private TextMeshProUGUI ui;

    private string PrefKeyPrefix = "german_valentine_score_";
    private string PrefKeySize = "german_valentine_score_size";


    // Use this for initialization
    void Start() {
        ui = this.GetComponent<TextMeshProUGUI>();

        //Load Score
        if (SceneManager.GetActiveScene().name == "drive") {
            currentScore += DataHolder.score;
        } else {
            currentScore += startScore;
        }

        //Set Point Decay
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

    public void saveScore() {
        DataHolder.score = currentScore;
    }
    public void gameComplete() {
        ScoreData score = new ScoreData(currentScore, "Mr. Placeholder");
        int size = PlayerPrefs.GetInt(PrefKeySize);
        size++;
        PlayerPrefs.SetInt(PrefKeySize, size);
        PlayerPrefs.SetString(PrefKeyPrefix + (size -1).ToString(), score.ToString());
    }
}
