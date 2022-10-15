using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : MonoBehaviour
{
    public bool is_picked = false;
    public bool end_task = false; // 還沒用到
    public GeneratePoint GP;
    public int point = 100;

    public int lastPlayer=0;
    public int ID=0;
    public MissionManager MM;

    // Start is called before the first frame update
    void Start()
    {
        GP = GameObject.Find("生兵點").GetComponent<GeneratePoint>();
        MM = GameObject.Find("MissionManager").GetComponent<MissionManager>();

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
            // GP.GeneratePatient();
            // end_task = true;
            // Destroy(gameObject);
            string s="抽血";
            MM.completeMission(ID,s);
        
        }

        if (collision.transform.tag == "task2" && is_picked == false && !end_task)
        {
            string s="量身高";
            MM.completeMission(ID,s);
        
        }

        if (collision.transform.tag == "exit" && is_picked == false && !end_task)
        {
            if(MM.checkAllMissionComplete(ID)){
                
                MM.deleteMission(ID);
                Destroy(gameObject);
                MM.score(lastPlayer,point);
            }
            else{
                Debug.Log("任務未完成");
            }
        
        }
    }
}
