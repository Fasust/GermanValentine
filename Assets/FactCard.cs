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
	public float delay = 6;
	public bool showPermanent = false;
	private float currentDelay;
	private bool showing;

	public UnityEvent afterDisplay;

	public string[] facts = {
		"\"schandMaien\" are a variant of \"LiebesMaien\" that People Attach to to houses of people they dislike. \n\n" +
			"They are traditionally decorated with tampons and black ribbons.",
		"One month after placing there tree, the german bealors come back to the houses of their crushes and put down their tree. \n\n" +
			"Their efforts will be rewarded with a cake or a beer.",
		"Every Leap year, the gender roles of the original tradition switch. The girls go out to steal the trees and the boys eagerly lay in their beds. ",
		"Legally Buying a tree for this tradition is looked down upon. \n\n" +
			"Thusly Security in german forests on the 1. Of May is on High Alert. ",
		"Usually the whole process of stealing, dressing and placing the trees is planed days in advance to the first of May. \n\n" +
			"Beer (as you would expect from Germany) plays a big role in this. ",
		"Many Bachelors try to avoid the hassle of stealing a tree in the forest by stealing trees that other bachelors did not properly secure on/at the houses of their crushes. ",
		"The \"Libesmaies\" are usually taped, tied or thrown on to the roofs of the german beachloretts to make them less accessible for potential thieves."
	};
	private int index;

	void Start () {
		GetComponent<Image>().enabled = false;
		loadingAnimation.enabled = false;
		text.enabled = false;
		titelText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (showPermanent)
		{
			return;
		}

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
		
		text.text = facts[index].Replace("\\n", "\n");
		titelText.text = "Fact #00" + ((index) + 1);
	}
	public void hide()
	{
		showing = false;

		text.enabled = false;
		titelText.enabled = false;
		GetComponent<Image>().enabled = false;
		loadingAnimation.enabled = false;
	}
	public void showNext()
	{
		if (!showing)
		{
			show();
		}
		index++;
		index = index % facts.Length;

		text.text = facts[index ].Replace("\\n", "\n");
		titelText.text = "Fact #00" + (index+ 1);

	}
}
