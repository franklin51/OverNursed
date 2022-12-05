using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExaminationRoomDoorCloseSensor : MonoBehaviour
{
    public PatientController patientcontroller;
    public int people_count = 0;
    public bool is_closed = true;
    Vector3 idle_rotation;

    // Start is called before the first frame update
    void Start()
    {
        patientcontroller = GameObject.Find("Doors").GetComponent<PatientController>();
        idle_rotation = transform.parent.Find("01_low").transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (people_count == 0 || !patientcontroller.can_door_open(transform.parent.name))
        {
            transform.parent.Find("01_low").transform.localEulerAngles = idle_rotation;
            is_closed = true;
        }
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.root.transform.tag == "patient" || collision.transform.root.transform.tag == "Player")
        {
            people_count++;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.transform.root.transform.tag == "patient" || collision.transform.root.transform.tag == "Player")
        {
            people_count--;
        }
    }
}
