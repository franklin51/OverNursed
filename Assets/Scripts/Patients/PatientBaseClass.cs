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
    public string missionPoint;

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
  
    
    public void timeOver()
    {
        MM.deleteMission(ID);
        Inpatience();
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
        else if (is_waiting)
            Waiting();


        //任務的計時完觸發
        if(timerOK==true){
            if(ExecuteMission()){
                timerOK=false;
                doingMission=false;
                MM.completeMission(ID,missionPoint,lastPlayer);
                updateDialogString();
                is_waiting = true;

            }
            else{
                timerOK=false;
                doingMission=false;
                is_waiting = true;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.tag == "task" && is_picked == false){
            is_waiting4FirstMission = false;
            missionPoint = collision.collider.gameObject.name;
        }
            

        if(MM.hasThisMission(ID, missionPoint)){
            is_waiting = false;
            doingMission =true;
            createTimer();
        }
        

        if (collision.transform.tag == "exit"  && !end_task)
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
         if (collision.transform.tag == "Player" && is_picked == false)
        {
           Dialog.SetActive(true);
        }
    }
}
