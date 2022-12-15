using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExaminationRoomDoorAnimation : MonoBehaviour
{
    Vector3 idle_rotation, close_rotation;
    private Animator mydoor;

    // Start is called before the first frame update
    void Start()
    {
        idle_rotation = transform.Find("01_low").transform.localEulerAngles;
        close_rotation = idle_rotation + new Vector3(0, 90, 0);
        mydoor = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void open()
    {
        /*if (is_open)
            return;
        is_open = true;
        mydoor.Play("open", 0);*/
        transform.Find("01_low").transform.localEulerAngles = idle_rotation;
    }

    public void close()
    {
        /*if (!is_open)
            return;
        is_open = false;
        mydoor.Play("close", 0);*/
        transform.Find("01_low").transform.localEulerAngles = close_rotation;
    }
}
