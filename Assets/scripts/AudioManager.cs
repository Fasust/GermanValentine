using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    private void Awake()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    public void play(string name)
    {
        getSound(name).source.Play();
    }
    public void play(string name, float delay)
    {
        getSound(name).source.PlayDelayed(delay);
    }
    public void stop(string name)
    {
        getSound(name).source.Stop();
    }

    public Sound getSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        return s;
    }
    public void setPitch(String name, float val)
    {
        getSound(name).source.pitch = val;
    }
}
