using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StateManagerMain : MonoBehaviour {
    public Button replayButton;
    public Button startOverButton;
    public bool hideReplay = true;
    void Start() {

        if (hideReplay) {
            replayButton.enabled = false;
            replayButton.image.enabled = false;
            replayButton.GetComponentInChildren<TextMeshProUGUI>().enabled = false;

            startOverButton.enabled = false;
            startOverButton.image.enabled = false;
            startOverButton.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        }

    }
    public void relode() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void showReplay() {
        replayButton.enabled = true;
        replayButton.image.enabled = true;
        replayButton.GetComponentInChildren<TextMeshProUGUI>().enabled = true;

        startOverButton.enabled = true;
        startOverButton.image.enabled = true;
        startOverButton.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
    }

    public void loadLevel(string name) {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }
    public void startOver() {
        loadLevel("chop");
    }
}
