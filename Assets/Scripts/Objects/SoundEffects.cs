using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    Dictionary<string, AudioSource> audios = new Dictionary<string, AudioSource>();

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] tmp = GetComponents<AudioSource>();
        if (tmp.Length != 0)
        {
            audios.Add("blood", tmp[0]);
            audios.Add("ECG", tmp[1]);
            audios.Add("urine", tmp[2]);
            audios.Add("Exit", tmp[3]);
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
