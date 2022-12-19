using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMoneyAnimation : MonoBehaviour
{
    private Animator anime;
    float destroyTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fadein()
    {
        if (anime)
            anime.Play("Money_fadein");
        Destroy(gameObject, destroyTime);
    }

    public void fadeout()
    {
        if (anime)
            anime.Play("Money_fadeout");
        Debug.Log("fadeout");
        Destroy(gameObject, destroyTime);
    }
}
