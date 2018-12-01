using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FactCard : MonoBehaviour {

	public Image loadingAnimation;
	public TextMeshProUGUI text;
	public TextMeshProUGUI titelText;
	public float delay;
	private float currentDelay;
	private bool showing;

	public UnityEvent afterDisplay;

	public string[] facts;
	private int index;

	void Start () {
		GetComponent<Image>().enabled = false;
		loadingAnimation.enabled = false;
		text.enabled = false;
		titelText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (showing)
		{
			if(delay > currentDelay)
			{
				currentDelay += Time.deltaTime;
			}
			else
			{
				afterDisplay.Invoke();
			}
		}
	}

	public void show()
	{
		showing = true;
		index = Random.Range(0, facts.Length - 1);

		text.enabled = true;
		titelText.enabled = true;
		GetComponent<Image>().enabled = true;
		GetComponent<Image>().color = Color.white;
		loadingAnimation.enabled = true;
		
		Debug.Log(facts[index].Replace("\\n","\n"));
		text.text = facts[index].Replace("\\n", "\n");
		titelText.text = "Fact #00" + (index+1);
	}
}
