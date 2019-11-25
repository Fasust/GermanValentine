using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {
    private PlayerState player;
    private AudioSource impale;
    private bool soundPlaying;
    void Start() {
        player = FindObjectOfType<PlayerState>();
        impale = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.tag == "Player" && player.isFalling()) {
            player.detect();

            if (!soundPlaying) {
                impale.Play();
                soundPlaying = true;
            }
        }
    }
}
