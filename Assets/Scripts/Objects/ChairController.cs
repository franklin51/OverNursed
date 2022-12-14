using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairController : MonoBehaviour
{
    public Counter counter;

    // Start is called before the first frame update
    void Start()
    {
        counter = GameObject.Find("?d?i").GetComponent<Counter>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void enable(int chair_idx)
    {
        transform.GetComponent<Renderer>().enabled = true;
        transform.GetChild(0).GetComponent<Renderer>().enabled = true;
        counter.chair[chair_idx] = -1;
    }

    public void disable()
    {
        transform.GetComponent<Renderer>().enabled = false;
        transform.GetChild(0).GetComponent<Renderer>().enabled = false;
    }
}
