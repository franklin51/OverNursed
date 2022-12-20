using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2P : MonoBehaviour
{
    [SerializeField] bool is_winned;
    Dictionary<string, AudioSource> audios = new Dictionary<string, AudioSource>();
    string[] sound_effects_win = new string[] { "08", "11", "13", "20", "29" };
    string[] sound_effects_lose = new string[] { "07", "12", "22", "24", "26" };
    int[] rotation = new int[] { 0, 180, 270, 90 };
    string[] sound_effects;
    UnityEngine.KeyCode[] keys = new UnityEngine.KeyCode[] { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow};

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] tmp = GetComponents<AudioSource>();

        if (!is_winned)
        {
            if (tmp.Length != 0)
            {
                for (int i = 0; i < 5; i++)
                    audios.Add(sound_effects_lose[i], tmp[i]);
            }
            sound_effects = sound_effects_lose;
        } else
        {
            if (tmp.Length != 0)
            {
                for (int i = 5; i < 10; i++)
                    audios.Add(sound_effects_win[i-5], tmp[i]);
            }
            sound_effects = sound_effects_win;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetKey(keys[i]))
            {
                foreach (string s in sound_effects)
                    audios[s].Stop();
                audios[sound_effects[i]].Play();
            }
        }

        if (Input.GetKey("m"))
        {
            foreach (string s in sound_effects)
                audios[s].Stop();
            if (is_winned)
                audios["29"].Play();
            else
                audios["26"].Play();
        }

        for (int i = 0; i < 4; i++)
            if (Input.GetKey(keys[i]))
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotation[i], transform.eulerAngles.z);
    }
}
