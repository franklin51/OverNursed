using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndoorSensor : MonoBehaviour
{
    Vector3 idle_rotation, close_rotation;
    public bool patient_in = false;
    PatientController patientcontroller;

    // Start is called before the first frame update
    void Start()
    {
        idle_rotation = transform.parent.Find("01_low").transform.localEulerAngles;
        close_rotation = idle_rotation + new Vector3(0, 90, 0);
        patientcontroller = GameObject.Find("Doors").GetComponent<PatientController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.root.transform.tag == "patient" || collision.transform.root.transform.tag == "Player")
        {
            open();
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if ((collision.transform.root.transform.tag == "patient" || collision.transform.root.transform.tag == "Player") && !patientcontroller.can_door_open(transform.parent.name))
        {
            close();
        }
    }

    public void open()
    {
        transform.parent.Find("01_low").transform.localEulerAngles = idle_rotation;
    }

    public void close()
    {
        transform.parent.Find("01_low").transform.localEulerAngles = close_rotation;
    }

    void close_lock()
    {

    }
}
