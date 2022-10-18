using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingInMenu : MonoBehaviour
{
	public float velocity = 10f;
    public Rigidbody rb;
		
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // 分不同角色的操作
        string name = gameObject.name;
        if (name == "1P")
        {
            // m_Input = new Vector3(0, 0, 0);
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
            /*if (Input.GetKeyDown("t"))
            {
                allow_attached = !allow_attached;
                if (allow_attached == false)
                {
                    if (patient) {
						patient.GetComponent<Patient>().is_picked = false;
                        patient.GetComponent<Patient>().lastPlayer = 1;
                        patient.transform.parent = null;
					}
                    patient = null;
                }
            }*/
        }
        else if (name == "2P")
        {
            // m_Input = new Vector3(0, 0, 0);
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
            /*if (Input.GetKeyDown("m"))
            {
                allow_attached = !allow_attached;
                if (allow_attached == false)
                {
                    patient.GetComponent<Patient>().is_picked = false;
                    patient.GetComponent<Patient>().lastPlayer = 2;
                    if (patient)
                        patient.transform.parent = null;
                    patient = null;
                }
            }*/
        }
    }
}
