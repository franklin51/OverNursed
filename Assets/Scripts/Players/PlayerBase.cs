using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 跟操作有關的
public class PlayerBase : MonoBehaviour
{
    public float speed = 40f;
    public float velocity = 50f;
    public GameObject patient;
    public GeneratePoint GP;
    public Rigidbody rb;
    Vector3 m_Input = new Vector3(0, 0, 0);

    private bool already_pick=false; // 已經撿起病人了嗎
    private bool to_pick = false;

    // Start is called before the first frame update
    void Start()
    {
        GP = GameObject.Find("生兵點").GetComponent<GeneratePoint>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        // 分不同角色的操作
        string name = gameObject.name;
        if (name == "1P")
        {
            m_Input = new Vector3(0, 0, 0);
            if (Input.GetKey("w"))
                MoveUp();
            else if (Input.GetKey("s"))
                MoveDown();
            else if (Input.GetKey("a"))
                MoveLeft();
            else if (Input.GetKey("d"))
                MoveRight();
            else
                rb.velocity = new Vector3(0, 0, 0);
            if (Input.GetKeyDown("t"))
            {
                to_pick = !to_pick;
                if (already_pick && patient && !patient.GetComponent<PatientBaseClass>().allow_picked)
                    PutDownPatient();
            }
        }
        else if (name == "2P")
        {
            m_Input = new Vector3(0, 0, 0);
            if (Input.GetKey(KeyCode.UpArrow))
                MoveUp();
            else if (Input.GetKey(KeyCode.DownArrow))
                MoveDown();
            else if (Input.GetKey(KeyCode.LeftArrow))
                MoveLeft();
            else if (Input.GetKey(KeyCode.RightArrow))
                MoveRight();
            else
				rb.velocity = new Vector3(0, 0, 0);
            if (Input.GetKeyDown("m"))
            {
                to_pick = !to_pick;
                if (already_pick && patient && !patient.GetComponent<PatientBaseClass>().allow_picked)
                    PutDownPatient();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {

    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "patient")
        {
            bool isDoing=collision.transform.GetComponent<PatientBaseClass>().doingMission;
            if (to_pick && !already_pick && !isDoing)
            {
                patient = collision.gameObject;
                to_pick = true;

                // 與病人合體
                if (patient.GetComponent<PatientBaseClass>().allow_picked)
                {
                    already_pick = true;
                    patient.GetComponent<PatientBaseClass>().allow_picked = false;
                    patient.transform.SetParent(transform, false);
                    patient.transform.localScale = new Vector3(patient.transform.localScale.x / transform.localScale.x, 2, patient.transform.localScale.z / transform.localScale.z);
                    patient.transform.localPosition = new Vector3(0, 3.5f, 0);
                }
            }
        }
        if (collision.transform.tag == "Wall")
        {
            
        }
    }
    void OnCollisionExit(Collision collision)
    {

    }


    private void MoveUp()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        rb.velocity = new Vector3(0, 0, velocity);
    }
    private void MoveDown()
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
        rb.velocity = new Vector3(0, 0, -velocity);
    }
    private void MoveLeft()
    {
        transform.eulerAngles = new Vector3(0, 270, 0);
        rb.velocity = new Vector3(-velocity, 0, 0);
    }
    private void MoveRight()
    {
        transform.eulerAngles = new Vector3(0, 90, 0);
        rb.velocity = new Vector3(velocity, 0, 0);
    }
    private void PutDownPatient()
    {
        already_pick = false;
        patient.GetComponent<PatientBaseClass>().allow_picked = true;
        patient.GetComponent<PatientBaseClass>().lastPlayer = 1;
        patient.transform.parent = null;
        patient.transform.position = new Vector3(patient.transform.position.x, 0.03f, patient.transform.position.z) + transform.forward * 2f;
        patient = null;
    }
}
