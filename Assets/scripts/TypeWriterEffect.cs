using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class TypeWriterEffect : MonoBehaviour
{

	public float delay = 0.1f;
	public string fullText;
	private string currentText = "";
	public AudioSource typeSound;
	public bool typeSoundActive = true;

	// Use this for initialization
	void Start()
	{
		StartCoroutine(ShowText());
	}

	IEnumerator ShowText()
	{
		for (int i = 0; i < fullText.Length; i++)
		{
			if (typeSoundActive)
			{
				typeSound.Play();
			}
			currentText = fullText.Substring(0, i);
			this.GetComponent<TextMeshProUGUI>().text = currentText;
			yield return new WaitForSeconds(delay);
		}
	}
}
