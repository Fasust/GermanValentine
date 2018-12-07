using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fireworks : MonoBehaviour {

	public GameObject paparticalEffectBlue;
	public GameObject paparticalEffectRed;
	public Transform particelSpawn1;
	public Transform particelSpawn2;
	public Transform particelSpawn3;
	public float paticelDelay = 0.5f;
	public int fierworkCicels = 2;

	private float paTimeCount = 0;
	private bool playingWinSound = false;
	private bool pSpawned1 = false;
	private bool pSpawned2 = false;
	private bool pSpawned3 = false;

	private bool playing;


	public UnityEvent end;


	// Update is called once per frame
	void Update () {
		if (playing)
		{

			if (!playingWinSound)
			{
				//carAnimator.SetTrigger("drive");
				FindObjectOfType<AudioManager>().play("Win");
				playingWinSound = true;
			}

			//Particals-------------------------------------
			paTimeCount += Time.deltaTime;

			if (paTimeCount >= paticelDelay && !pSpawned1)
			{

				if (fierworkCicels == 0)
				{
					end.Invoke();
					playing = false;
					return;
				}

				pSpawned1 = true;
				Instantiate(paparticalEffectRed, particelSpawn1.transform.position, Quaternion.identity);
				FindObjectOfType<AudioManager>().play("BlowUp");

			}
			if (paTimeCount >= paticelDelay * 2 && !pSpawned2)
			{
				pSpawned2 = true;
				Instantiate(paparticalEffectBlue, particelSpawn2.transform.position, Quaternion.identity);
				FindObjectOfType<AudioManager>().play("BlowUp");

			}
			if (paTimeCount >= paticelDelay * 3 && !pSpawned3)
			{
				pSpawned3 = true;
				Instantiate(paparticalEffectRed, particelSpawn3.transform.position, Quaternion.identity);
				FindObjectOfType<AudioManager>().play("BlowUp");

				pSpawned1 = false;
				pSpawned2 = false;
				pSpawned3 = false;
				paTimeCount = 0;

				fierworkCicels--;
			}
		}
	}
	public void play()
	{
		playing = true;
	}
}
