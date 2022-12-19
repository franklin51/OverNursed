using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngerEmote : MonoBehaviour
{
    private Animator anime;
    [SerializeField] public Transform lookAt;

    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        anime.Play("AngerEmote_0");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
