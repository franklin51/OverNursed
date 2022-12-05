using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExaminationRoomDoorOpenSensor : MonoBehaviour
{
    public PatientController patientcontroller;
    public ExaminationRoomDoorCloseSensor examinationroomdoorclosesensor;
    Vector3 idle_rotation;

    // Start is called before the first frame update
    void Start()
    {
        patientcontroller = GameObject.Find("Doors").GetComponent<PatientController>();
        examinationroomdoorclosesensor = transform.parent.Find("Open2").GetComponent<ExaminationRoomDoorCloseSensor>();
        idle_rotation = transform.parent.Find("01_low").transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.root.transform.tag == "patient" || collision.transform.root.transform.tag == "Player")
        {
            if (name == "Open0" && patientcontroller.can_door_open(transform.parent.name))
            {
                open0();
            }
            if (name == "Open1" && patientcontroller.can_door_open(transform.parent.name))
            {
                open1();
            }
        }
    }

    void open0()
    {
        if (examinationroomdoorclosesensor.is_closed == false)
            return;

        examinationroomdoorclosesensor.is_closed = false;
        GameObject door_component = transform.parent.Find("01_low").gameObject;
        //door_component.transform.localRotation = idle_rotation * Quaternion.Euler(0, -90, 0);
        door_component.transform.localEulerAngles = idle_rotation + new Vector3(0, -90, 0);
    }

    void open1()
    {
        if (examinationroomdoorclosesensor.is_closed == false)
            return;

        examinationroomdoorclosesensor.is_closed = false;
        GameObject door_component = transform.parent.Find("01_low").gameObject;
        //door_component.transform.localRotation = idle_rotation * Quaternion.Euler(0, 90, 0);
        door_component.transform.localEulerAngles = idle_rotation + new Vector3(0, 90, 0);
    }
}
