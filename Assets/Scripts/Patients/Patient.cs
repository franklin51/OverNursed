using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : MonoBehaviour
{
    public bool is_picked = false;
    public bool end_task = false; // 還沒用到
    public GeneratePoint GP;

    // Start is called before the first frame update
    void Start()
    {
        GP = GameObject.Find("生兵點").GetComponent<GeneratePoint>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        // 碰到任務點，把病人對齊中心
        if (collision.transform.tag == "task" && is_picked == false)
        {
            transform.position = collision.transform.position;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        // 生兵，到時候用結束點代替；消滅
        if (collision.transform.tag == "task" && is_picked == false && !end_task)
        {
            GP.GeneratePatient();
            end_task = true;
            Destroy(gameObject);
        }
    }
}
