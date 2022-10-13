using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public float speed = 0.1f;
    public GameObject patient;
    public GeneratePoint GP;
    public Rigidbody rb;

    private bool allow_attached = false; // 可以拿病人嗎

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
            if (Input.GetKey("w"))
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                pos.z += speed;
            }
            if (Input.GetKey("s"))
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                pos.z -= speed;
            }
            if (Input.GetKey("d"))
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
                pos.x += speed;
            }
            if (Input.GetKey("a"))
            {
                transform.eulerAngles = new Vector3(0, 270, 0);
                pos.x -= speed;
            }
            if (Input.GetKeyDown("t"))
            {
                allow_attached = !allow_attached;
                if (allow_attached == false)
                {
                    if (patient) {
						patient.GetComponent<Patient>().is_picked = false;
                        patient.transform.parent = null;
					}
                    patient = null;
                }
            }
        }
        else if (name == "2P")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                pos.z += speed;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                pos.z -= speed;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
                pos.x += speed;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.eulerAngles = new Vector3(0, 270, 0);
                pos.x -= speed;
            }
            if (Input.GetKeyDown("m"))
            {
                allow_attached = !allow_attached;
                if (allow_attached == false)
                {
                    patient.GetComponent<Patient>().is_picked = false;
                    if (patient)
                        patient.transform.parent = null;
                    patient = null;
                }
            }
        }

        // 暫時邊界
        // pos.x = (pos.x>30)? 30:((pos.x<0)? 0:pos.x);
        // pos.z = (pos.z > 20) ? 20 : ((pos.z < 0) ? 0 : pos.z);
        transform.position = pos;

        // 防止bouncing
        rb.velocity = new Vector3(0, 0, 0);
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
            if (allow_attached)
            {
                patient = collision.gameObject;

                // 拿起病人時對齊中線
                Vector3 temp = transform.eulerAngles;
                patient.transform.eulerAngles = new Vector3(0, 0, 0);
                transform.eulerAngles = new Vector3(0, 90, 0);
                patient.transform.position = transform.position + transform.forward * 3.0f;
                patient.transform.eulerAngles = temp;

                // 與病人合體
                patient.GetComponent<Patient>().is_picked = true;
                patient.transform.SetParent(transform);
            }
        }
    }
}
