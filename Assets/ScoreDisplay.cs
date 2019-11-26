using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour {
    private string PrefKeyPrefix = "german_valentine_score_";
    private string PrefKeySize = "german_valentine_score_size";
    private TextMeshProUGUI display;

    private List<ScoreData> scores = new List<ScoreData>();
    void Start() {
        display = GetComponent<TextMeshProUGUI>();

        ScoreData dummy = new ScoreData(1002, "Fasust");
        PlayerPrefs.SetInt(PrefKeySize, 1);
        PlayerPrefs.SetString(PrefKeyPrefix + 0.ToString(), dummy.ToString());

        loadScores();
        sortScores();
        fillDisplay();
    }

    private void sortScores() {
        scores.Sort((x, y) => x.score.CompareTo(y.score));
    }

    private void loadScores() {
        if (PlayerPrefs.HasKey(PrefKeySize)) {
            int size = PlayerPrefs.GetInt(PrefKeySize);

            for (int i = 0; i < size; i++) {
                string scoreString = PlayerPrefs.GetString(PrefKeyPrefix + i.ToString());
                scores.Add(new ScoreData(scoreString));
            }
        }
    }
    private void fillDisplay() {
        string displayText = "";
        for (int i = 0; i < scores.Count; i++) {
            displayText += scores[i].getFormatedScore() + " - " + scores[i].name;
        }
        display.text = displayText;
    }
}
