using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerMain : MonoBehaviour {

    private float startTime;
    private bool finish = false;
    public int milisecondDesimels = 0;
    private Score score;

    // Use this for initialization
    void Start() {
        startTime = Time.time;
        score = FindObjectOfType<Score>();
    }

    // Update is called once per frame
    void Update() {

        if (finish) {
            return;
        }

        float timeSinceStart = Time.time - startTime;

        string minutes = ((int)timeSinceStart / 60).ToString();
        string seconds = (timeSinceStart % 60).ToString("f" + milisecondDesimels);

        if (minutes.Length == 1) {
            minutes = "0" + minutes;
        }

        if (seconds.Length == 2 + milisecondDesimels) {
            seconds = "0" + seconds;
        }

        this.GetComponent<TextMeshProUGUI>().text = minutes + ":" + seconds;

    }

    public void stop() {
        finish = true;
        this.GetComponent<TextMeshProUGUI>().color = Color.yellow;

        FindObjectOfType<Score>().setBlocked();
    }
}
