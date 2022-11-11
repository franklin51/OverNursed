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
    public MissionManager MM;
    [SerializeField] GameObject Dialog;
    [SerializeField] GameObject timerPrefabs;

    abstract protected bool Waiting4FirstMission(); // 生兵後等待第一個任務，return true表示等不及了，進入Inpatience函式
    abstract protected bool ExecuteMission(); // 執行任務，return true表示成功執行
    abstract protected bool Waiting(); // 任務完成後等待下一個任務，return true表示等不及了，進入Inpatience函式
    abstract protected void Inpatience(); // 等不及開始搞事
    protected void Leaving() { } // 最後一個任務完成


    //float timer=0;


    // Start is called before the first frame update
    void Start()
    {
        GP = GameObject.Find("生兵點").GetComponent<GeneratePoint>();
        MM = GameObject.Find("MissionManager").GetComponent<MissionManager>();
        updateDialogString();
        Dialog.SetActive(false);
        //startCall();
    }

    //protected abstract void startCall();
    
    protected void updateDialogString(){
        Dialog.transform.GetComponentInChildren<Text>().text=getDialogString();
    }

    string getDialogString(){
        string s="";
        for (int i=0; i<mission.Length; i++){
            if(isComplete[i]==true){
                s = s + mission[i] + " (ok)\n";
            }
            else{
                s = s + mission[i] + "\n";
            }
        }

        return s;
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

    bool checkAllMissionComplete(){
        bool check=true;
        
        for(int i=0; i<isComplete.Length; i++){
            if(isComplete[i]==false){
                check=false;
                break;
            }
        }

        return check;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (is_picked == false)
        {
            if (!is_waiting && is_waiting4FirstMission && Waiting4FirstMission())
            {
                is_waiting4FirstMission = false;
                Inpatience();
            }
            else if (MM.checkAllMissionComplete(ID))
                Leaving();
            else if (is_waiting && Waiting())
                    Inpatience();
        }
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
            Debug.Log("task");

        if (collision.transform.tag == "Player" && is_picked == false)
        {
           Dialog.SetActive(true);
        }

        if (name == "抽血" && is_picked == false && !end_task)
        {
           if(!hasEntered1)
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
            }
        }

        if (name == "量身高" && is_picked == false && !end_task)
        {
            if(!hasEntered2){
            createTimer();
                if (ExecuteMission())
                {
                    hasEntered2 = true;
                    string s = "height";
                    completeMission(s);
                    MM.completeMission(ID, s, lastPlayer);
                    is_waiting = true;
                }
            }
        }

        if (name == "心電圖" && is_picked == false && !end_task)
        {
            if(!hasEntered3){
            createTimer();
                if (ExecuteMission())
                {
                    hasEntered3 = true;
                    string s = "ECG";
                    completeMission(s);
                    MM.completeMission(ID, s, lastPlayer);
                    is_waiting = true;
                }
            }
        }

        if (name == "驗尿" && is_picked == false && !end_task)
        {
            if(!hasEntered4){
            createTimer();
                if (ExecuteMission())
                {
                    hasEntered4 = true;
                    string s = "urine";
                    completeMission(s);
                    MM.completeMission(ID, s, lastPlayer);
                    is_waiting = true;
                }
            }
        }

        if (name == "量視力" && is_picked == false && !end_task)
        {
            if(!hasEntered5){
            createTimer();
                if (ExecuteMission())
                {
                    hasEntered5 = true;
                    string s = "visual";
                    completeMission(s);
                    MM.completeMission(ID, s, lastPlayer);
                    is_waiting = true;
                }
            }
        }

        if (collision.transform.tag == "exit" && is_picked == false && !end_task)
        {
            if(MM.checkAllMissionComplete(ID)){

                Destroy(gameObject);
                MM.score(ID,lastPlayer,point);
            }
            else{
                Debug.Log("任務未完成");
            }
        
        }
    }
}
