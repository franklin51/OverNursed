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
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                //pos.z += speed;
                //m_Input += new Vector3(0, 0, 1);
                //m_Input.Normalize();
                //rb.MovePosition(transform.position + m_Input * Time.deltaTime * speed);
				rb.velocity = new Vector3(0, 0, velocity);
            }
            else if (Input.GetKey("s"))
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                //pos.z -= speed;
                //m_Input += new Vector3(0, 0, -1);
                //m_Input.Normalize();
                //rb.MovePosition(transform.position + m_Input * Time.deltaTime * speed);
				rb.velocity = new Vector3(0, 0, -velocity);
            }
            else if (Input.GetKey("d"))
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
                //pos.x += speed;
                //m_Input += new Vector3(1, 0, 0);
                //m_Input.Normalize();
                //rb.MovePosition(transform.position + m_Input * Time.deltaTime * speed);
				rb.velocity = new Vector3(velocity, 0, 0);
            }
            else if (Input.GetKey("a"))
            {
                transform.eulerAngles = new Vector3(0, 270, 0);
                //pos.x -= speed;
                //m_Input += new Vector3(-1, 0, 0);
                //m_Input.Normalize();
                //rb.MovePosition(transform.position + m_Input * Time.deltaTime * speed);
				rb.velocity = new Vector3(-velocity, 0, 0);
            }
			else
				rb.velocity = new Vector3(0, 0, 0);
            //if(Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s")|| Input.GetKey("d")){
            //    rb.MovePosition(transform.position + m_Input * Time.deltaTime * speed);
            //}
            if (Input.GetKeyDown("t"))
            {
                allow_attached = !allow_attached;
                if (allow_attached == false)
                {
                    if (patient) {
                        already_pick=false;
						patient.GetComponent<PatientBaseClass>().is_picked = false;
                        patient.GetComponent<PatientBaseClass>().lastPlayer = 1;
                        patient.transform.parent = null;
					}
                    patient = null;
                }
            }
        }
        else if (name == "2P")
        {
            m_Input = new Vector3(0, 0, 0);
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                //pos.z += speed;
                //m_Input += new Vector3(0, 0, 1);
                //m_Input.Normalize();
                //rb.MovePosition(transform.position + m_Input * Time.deltaTime * speed);
				rb.velocity = new Vector3(0, 0, velocity);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                //pos.z -= speed;
                //m_Input += new Vector3(0, 0, -1);
                //m_Input.Normalize();
                //rb.MovePosition(transform.position + m_Input * Time.deltaTime * speed);
				rb.velocity = new Vector3(0, 0, -velocity);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
                //pos.x += speed;
                //m_Input += new Vector3(1, 0, 0);
                //m_Input.Normalize();
                //rb.MovePosition(transform.position + m_Input * Time.deltaTime * speed);
				rb.velocity = new Vector3(velocity, 0, 0);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.eulerAngles = new Vector3(0, 270, 0);
                //pos.x -= speed;
                //m_Input += new Vector3(-1, 0, 0);
                //m_Input.Normalize();
                //rb.MovePosition(transform.position + m_Input * Time.deltaTime * speed);
				rb.velocity = new Vector3(-velocity, 0, 0);
            }
			else
				rb.velocity = new Vector3(0, 0, 0);
            //if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow)){
            //    rb.MovePosition(transform.position + m_Input * Time.deltaTime * speed);
            //}
            if (Input.GetKeyDown("m"))
            {
                allow_attached = !allow_attached;
                if (allow_attached == false)
                {
                    
                    if (patient)
                    {
                        already_pick=false;
                        patient.GetComponent<PatientBaseClass>().is_picked = false;
                        patient.GetComponent<PatientBaseClass>().lastPlayer = 2;
                        patient.transform.parent = null;
                    }
                        
                    patient = null;
                }
            }
        }

        // 暫時邊界
        // pos.x = (pos.x>30)? 30:((pos.x<0)? 0:pos.x);
        // pos.z = (pos.z > 20) ? 20 : ((pos.z < 0) ? 0 : pos.z);
        //transform.position = pos;

        // 防止bouncing
        // rb.velocity = new Vector3(0, 0, 0);
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
            if (allow_attached&& !already_pick)
            {
                already_pick=true;
                patient = collision.gameObject;
				Debug.Log(patient);

                // 拿起病人時對齊中線
                Vector3 temp = transform.eulerAngles;
                patient.transform.eulerAngles = new Vector3(0, 0, 0);
                transform.eulerAngles = new Vector3(0, 90, 0);
                patient.transform.position = transform.position + transform.forward * 3.0f;
                patient.transform.eulerAngles = temp;

                // 與病人合體
                patient.GetComponent<PatientBaseClass>().is_picked = true;
                patient.transform.SetParent(transform);
            }
            speed = 50f;
        }
        if (collision.transform.tag == "Wall")
        {
            speed = 50f;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Wall" || collision.transform.tag == "patient")
        {
            speed = 100f;
        }
    }
}
