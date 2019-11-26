using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSaver : MonoBehaviour {
    private string PrefKeyPrefix = "german_valentine_score_";
    private string PrefKeySize = "german_valentine_score_size";

    public TMP_InputField input;
    public StateManagerMain manager;

    void Start(){
        FindObjectOfType<Score>().setBlocked();
    }

    public void SaveScore() {
        string name;
        if(input.text == "" || input.text == null){
            name = "Mr. Placeholder";
        }else{
            name = input.text;
        }

        //create score
        ScoreData score = new ScoreData(FindObjectOfType<Score>().getCurrentScore(), name);

        //Increase score list size
        int size = PlayerPrefs.GetInt(PrefKeySize);
        PlayerPrefs.SetInt(PrefKeySize, size + 1);

        //Add score to list
        PlayerPrefs.SetString(PrefKeyPrefix + size.ToString(), score.ToString());

        manager.loadLevel("menu");
    }
}
