using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 與操作、碰撞偵測有關的
abstract public class PatientBaseClass : MonoBehaviour
{
    public bool is_picked = false;
    public bool end_task = false; // 還沒用到
    public bool is_waiting4FirstMission = true, is_waiting = false;
    public GeneratePoint GP;
    public int point = 50;

    public string[] mission;
    public bool[] isComplete;
    public int lastPlayer=0;
    public int ID=0;
    public bool doingMission=false;
    public bool timerOK=false;
    public MissionManager MM;
    [SerializeField] GameObject Dialog;
    [SerializeField] GameObject timerPrefabs;

    abstract protected bool Waiting4FirstMission(); // 生兵後等待第一個任務，return true表示等不及了，進入Inpatience函式
    abstract protected bool ExecuteMission(); // 執行任務，return true表示成功執行
    abstract protected bool Waiting(); // 任務完成後等待下一個任務，return true表示等不及了，進入Inpatience函式
    abstract protected void Inpatience(); // 等不及開始搞事
    protected void Leaving() { Debug.Log("Leaving"); } // 最後一個任務完成


    //float timer=0;


    // Start is called before the first frame update
    public virtual void Start()
    {
        GP = GameObject.Find("生兵點").GetComponent<GeneratePoint>();
        MM = GameObject.Find("MissionManager").GetComponent<MissionManager>();
        updateDialogString();
        Dialog.SetActive(false);
        //startCall();
    }
    public void timerEnd(){
        timerOK=true;
    }
    
    void updateDialogString(){
        Dialog.transform.GetComponentInChildren<Text>().text=MM.getDialogString(ID);
    }
   
    void createTimer(){
        GameObject timer = Instantiate(timerPrefabs, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        timer.transform.SetParent (transform.GetChild(1), false);
        timer.GetComponent<Timer>().lookAt=transform;
    }
  
    void completeMission(string whatMission){
        for(int i =0; i<mission.Length; i++){
            if(mission[i]==whatMission){
                isComplete[i]=true;
            }
        }
        updateDialogString();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (!is_waiting && is_waiting4FirstMission && !is_picked && Waiting4FirstMission())
        {
            is_waiting4FirstMission = false;
            Inpatience();
        }
        else if (MM.checkAllMissionComplete(ID) && !is_picked)
            Leaving();
        else if (is_waiting && Waiting())
            Inpatience();
    }

    void OnCollisionEnter(Collision collision)
    {
        // 碰到任務點，把病人對齊中心
        /*if (collision.transform.tag == "task" && is_picked == false)
        {
            transform.position = collision.transform.position;
        }*/
    }

    private bool hasEntered1,hasEntered2,hasEntered3,hasEntered4,hasEntered5;

    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
           Dialog.SetActive(false);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        string name = collision.collider.gameObject.name;

        if (collision.transform.tag == "task" && is_picked == false)
            is_waiting4FirstMission = false;

        if (collision.transform.tag == "Player" && is_picked == false)
        {
           Dialog.SetActive(true);
        }

        if (name == "抽血" && is_picked == false && !end_task)
        {
            /*if(!hasEntered1)
            {
             createTimer();
                 if (ExecuteMission())
                 {
                     hasEntered1 = true;
                     string s = "blood";
                     completeMission(s);
                     MM.completeMission(ID, s, lastPlayer);
                     is_waiting = true;
                 }
             }*/
           if(!hasEntered1)
            {
                is_waiting = false;
                doingMission =true;
            createTimer();
           }
           hasEntered1=true;

           if (timerOK == true && ExecuteMission())
            {
                timerOK=false;
                doingMission=false;
                string s="blood";
                MM.completeMission(ID,s,lastPlayer);
                updateDialogString();
                is_waiting = true;
            }
        }

        if (name == "量身高" && is_picked == false && !end_task)
        {
            if (!hasEntered2)
            {
                is_waiting = false;
                doingMission =true;
                createTimer();
           }
           hasEntered2=true;
           if(timerOK == true && ExecuteMission())
            {
                timerOK=false;
                doingMission=false;
                string s="height";
                MM.completeMission(ID,s,lastPlayer);
                updateDialogString();
                is_waiting = true;
            }
        }

        if (name == "心電圖" && is_picked == false && !end_task)
        {
            if (!hasEntered3)
            {
                is_waiting = false;
                doingMission =true;
                createTimer();
           }
           hasEntered3=true;
           if (timerOK == true && ExecuteMission())
            {
                timerOK=false;
                doingMission=false;
                string s="ECG";
                MM.completeMission(ID,s,lastPlayer);
                updateDialogString();
                is_waiting = true;
            }
        }

        if (name == "驗尿" && is_picked == false && !end_task)
        {
            if (!hasEntered4)
            {
                is_waiting = false;
                doingMission =true;
                createTimer();
           }
           hasEntered4=true;
           if (timerOK == true && ExecuteMission())
            {
                timerOK=false;
                doingMission=false;
                string s="urine";
                MM.completeMission(ID,s,lastPlayer);
                updateDialogString();
                is_waiting = true;
            }
        }

        if (name == "量視力" && is_picked == false && !end_task)
        {
            if (!hasEntered5)
            {
                is_waiting = false;
                doingMission =true;
                createTimer();
           }
           hasEntered5=true;
           if (timerOK == true && ExecuteMission())
            {
                timerOK=false;
                doingMission=false;
                string s="visual";
                MM.completeMission(ID,s,lastPlayer);
                updateDialogString();
                is_waiting = true;
            }
        }

        if (collision.transform.tag == "exit" && is_picked == false && !end_task)
        {
            if(MM.checkAllMissionComplete(ID)){

                MM.score(ID,lastPlayer,point);
                MM.deleteMission(ID);
                Destroy(gameObject);
            }
            else{
                Debug.Log("任務未完成");
            }
        
        }
    }
}
