using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSensor : MonoBehaviour
{
    private int collisionCount = 0;
    Vector3 close_position;

    // Start is called before the first frame update
    void Start()
    {
        close_position = transform.parent.transform.Find("Door").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (collisionCount > 0)
        {
            transform.parent.transform.Find("Door").transform.position = new Vector3(close_position.x, 5, close_position.z);
        }
        else
            transform.parent.transform.Find("Door").transform.position = close_position;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.root.transform.tag == "patient")
        {
            collisionCount++;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.transform.root.transform.tag == "patient")
        {
            collisionCount--;
        }
    }
}
