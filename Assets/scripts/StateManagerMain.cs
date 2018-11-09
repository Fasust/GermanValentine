using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManagerMain : MonoBehaviour
{
	public Button replayButton;
	void Start()
	{
		//FindObjectOfType<AudioManager>().play("Background");
		replayButton.onClick.AddListener(relode);
		replayButton.enabled = false;
		replayButton.image.enabled = false;
	}

	private void relode()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	public void showReplay()
	{
		replayButton.enabled = true;
		replayButton.image.enabled = true;
	}

}
