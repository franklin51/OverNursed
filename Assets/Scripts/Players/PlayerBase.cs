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

    private bool allow_attached = false; // 可以拿病人嗎
    private bool already_pick=false; // 已經撿起病人了嗎

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
                allow_attached = !allow_attached;
                if (allow_attached == false)
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
                allow_attached = !allow_attached;
                if (allow_attached == false)
                    PutDownPatient();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "patient")
        {
            allow_attached = false;
        }
        
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "patient")
        {
            bool isDoing=collision.transform.GetComponent<PatientBaseClass>().doingMission;
            if (allow_attached&& !already_pick && !isDoing)
            {
                already_pick=true;
                patient = collision.gameObject;

                // 拿起病人時對齊中線
                /*Vector3 temp = transform.eulerAngles;
                patient.transform.eulerAngles = new Vector3(0, 0, 0);
                transform.eulerAngles = new Vector3(0, 90, 0);
                //patient.transform.position = transform.position + transform.forward * 2.5f;
                patient.transform.eulerAngles = temp;*/

                // 與病人合體
                patient.GetComponent<PatientBaseClass>().is_picked = true;
                patient.transform.SetParent(transform, false);
                //patient.transform.localEulerAngles = new Vector3(0, 0, 0);
                patient.transform.localScale = new Vector3(patient.transform.localScale.x/transform.localScale.x, 2, patient.transform.localScale.z / transform.localScale.z);
                patient.transform.localPosition = new Vector3(0, 3.5f, 0);
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
        if (patient)
        {
            already_pick = false;
            patient.GetComponent<PatientBaseClass>().is_picked = false;
            patient.GetComponent<PatientBaseClass>().lastPlayer = 1;
            patient.transform.parent = null;
            patient.transform.position = new Vector3(patient.transform.position.x, 0.03f, patient.transform.position.z) + transform.forward * 2f;
        }
        patient = null;
    }
}
