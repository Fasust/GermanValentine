using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreData {
    private const int SCORE_NUMBER_LENGTH = 6;
    public int score;
    public string name;
    public ScoreData(string input) {
        score = int.Parse(input.Substring(0, SCORE_NUMBER_LENGTH - 1));
        name = input.Substring(SCORE_NUMBER_LENGTH, input.Length - SCORE_NUMBER_LENGTH);
    }
    public ScoreData(int score, string name) {
        this.score = score;
        this.name = name;
    }

    public string ToString() {
        String scoreString = getFormatedScore();
        return scoreString + name;
    }
    public String getFormatedScore() {
        double scoreDigits = Math.Floor(Math.Log10(score) + 1);
        String scoreString = score.ToString();
        int neededDigits = SCORE_NUMBER_LENGTH - (int)scoreDigits;

        for (int i = 0; i < neededDigits; i++) {
            scoreString = "0" + scoreString;
        }
        return scoreString;
    }

}
