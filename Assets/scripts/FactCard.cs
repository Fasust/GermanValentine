using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FactCard : MonoBehaviour {

    [Header("Visuals")]
    public Image loadingAnimation;
    public Button continueButton;
    public TextMeshProUGUI text;
    public TextMeshProUGUI buttonText;
    public TextMeshProUGUI titelText;
    public Color yellow;

    [Header("Logic")]
    public float delay = 6;
    public bool showPermanent = false;
    public UnityEvent afterDisplay;

    private float currentDelay;
    private bool showing;
    private bool showContinueButton = false;
    private AudioSource readySound;

    private string[] facts = {
        "\"Schandmaien\" are a variant of \"Liebesmaien\" that people attach to houses of people they dislike. \n\n" +
            "They are traditionally decorated with tampons and black ribbons.",
        "One month after placing their tree, the german bachelors come back to the houses of their crushes and put down their tree. \n\n" +
            "Their efforts will be rewarded with a cake or a beer.",
        "Every leap year, the gender roles of the original tradition switch. The girls go out to steal the trees and the boys eagerly lay in their beds. ",
        "Legally buying a tree for this tradition is looked down upon. \n\n" +
            "Thusly security in german forests on the 1. of may is on high alert. ",
        "Usually, the whole process of stealing, dressing and placing the trees is planned days in advance to the first of may. \n\n" +
            "Beer (as you would expect from Germany) plays a big role in this. ",
        "Many bachelors try to avoid the hassle of stealing a tree in the forest by stealing trees that other bachelors did not properly secure on/at the houses of their crushes. ",
        "The \"Libesmaies\" are usually taped, tied or thrown on to the roofs of the german bachelorettes to make them less accessible for potential thieves."
    };
    private int index;

    void Start() {
        readySound = GetComponent<AudioSource>();
        continueButton.onClick.AddListener(invokeAfterDisplay);

        GetComponent<Image>().enabled = false;
        loadingAnimation.enabled = false;
        text.enabled = false;
        titelText.enabled = false;

        continueButton.enabled = false;
        continueButton.image.enabled = false;
        buttonText.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        if (showPermanent) {
            return;
        }

        if (showing) {
            if (delay > currentDelay) {
                currentDelay += Time.deltaTime;
            } else {
                if (!continueButton.enabled) {
                    continueButton.enabled = true;
                    continueButton.image.enabled = true;
                    continueButton.image.color = yellow;
                    buttonText.enabled = true;
                    buttonText.color = Color.black;
                    readySound.Play();
                    loadingAnimation.enabled = false;
                }
            }
        }
    }

    public void show() {
        showing = true;
        index = Random.Range(0, facts.Length - 1);

        text.enabled = true;
        titelText.enabled = true;
        GetComponent<Image>().enabled = true;
        GetComponent<Image>().color = Color.white;
        loadingAnimation.enabled = true;
        loadingAnimation.color = Color.black;

        text.text = facts[index].Replace("\\n", "\n");
        titelText.text = "Fact #00" + ((index) + 1);
    }
    public void hide() {
        showing = false;

        text.enabled = false;
        titelText.enabled = false;
        GetComponent<Image>().enabled = false;
        loadingAnimation.enabled = false;
    }
    public void showNext() {
        if (!showing) {
            show();
        }
        index++;
        index = index % facts.Length;

        text.text = facts[index].Replace("\\n", "\n");
        titelText.text = "Fact #00" + (index + 1);

    }
    private void invokeAfterDisplay() {
        FindObjectOfType<AudioManager>().play("Button");
        afterDisplay.Invoke();
    }
}
