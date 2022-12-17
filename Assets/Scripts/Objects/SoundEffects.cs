using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    Dictionary<string, AudioSource> audios = new Dictionary<string, AudioSource>();
    string[] sound_effects = new string[] { "counter", "blood", "ECG", "urine", "Exit", "height", "visual"};

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] tmp = GetComponents<AudioSource>();
        if (tmp.Length != 0)
        {
            for (int i=0; i<Math.Min(tmp.Length, sound_effects.Length); i++)
                audios.Add(sound_effects[i], tmp[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySoundEffect(string name)
    {
        if (audios.ContainsKey(name))
            audios[name].Play();
    }
}
