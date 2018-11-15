using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StateManagerMain : MonoBehaviour
{
	public Button replayButton;

	void Awake()
	{
		DontDestroyOnLoad(this);
	}
	void Start()
	{
		//Music ------------------
		//FindObjectOfType<AudioManager>().play("Background");

		//Buttons -------------------
		replayButton.enabled = false;
		replayButton.image.enabled = false;
	}
	public void relode()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	public void showReplay()
	{
		replayButton.enabled = true;
		replayButton.image.enabled = true;
	}

	public void loadLevel(string name)
	{
		SceneManager.LoadScene(name);
	}
}
