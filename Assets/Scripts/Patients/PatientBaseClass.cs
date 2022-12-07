using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

// 與操作、碰撞偵測有關的
abstract public class PatientBaseClass : MonoBehaviour
{
    public int state = 0; // 0: 排隊等候玩家帶位子; 1: 剛進入，準備入櫃檯; 2: 櫃檯結束，進入任務環節; 3: 結束所有任務，準備入櫃檯; 4: 櫃檯等候; 5: 櫃檯結束，準備回家;

    public bool allow_picked = true;
    public bool end_task = false; // 還沒用到
    public bool is_waiting4FirstMission = true, is_waiting = false, is_inpatience = false;
    public bool is_lineup = true; // 剛進場
    public bool is_attacking = false; // 8+9
    public bool isLeaving = false;//angry grandma
    public GeneratePoint GP;
    public int point = 50;
    public bool hasOwner=false;

    public string LineupPosition;
    public string[] mission;
    public bool[] isComplete;
    public int lastPlayer=0;
    public int ID=0;
    public bool doingMission=false;
    public bool timerOK=false;
    public MissionManager MM;
    public NavMeshAgent agent;
    public PatientController patientcontroller;
    public Counter counter;
    [SerializeField] GameObject Dialog;
    [SerializeField] GameObject timerPrefabs;
    public string missionPoint;
    public Lineup lineup;

    abstract protected bool Waiting4FirstMission(); // 生兵後等待第一個任務，return true表示等不及了，進入Inpatience函式
    abstract protected bool ExecuteMission(); // 執行任務，return true表示成功執行
    abstract protected bool Waiting(); // 任務完成後等待下一個任務，return true表示等不及了，進入Inpatience函式
    abstract protected void Inpatience(); // 等不及開始搞事
    protected void Leaving() {  } // 最後一個任務完成
    void Exiting() { agent.enabled = true; NavigateTo(GameObject.Find("離開點")); allow_picked = false; }
    public void NavigateTo(GameObject des) { agent.enabled = true; agent.SetDestination(des.transform.position); }
    public void NavigateTo(Vector3 pos) { agent.enabled = true; agent.SetDestination(pos); }

    //float timer=0;

    // Start is called before the first frame update
    public virtual void Start()
    {
        lineup = GameObject.Find("生兵點").GetComponent<Lineup>();
        GP = GameObject.Find("生兵點").GetComponent<GeneratePoint>();
        MM = GameObject.Find("MissionManager").GetComponent<MissionManager>();
        patientcontroller = GameObject.Find("Doors").GetComponent<PatientController>();
        counter = GameObject.Find("櫃檯").GetComponent<Counter>();

        updateDialogString();
        Dialog.SetActive(false);
        //startCall();
    }

    public virtual void picked()
    {
        
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
        is_inpatience = true;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (is_inpatience)
            Inpatience();
        else if (!is_waiting && is_waiting4FirstMission && allow_picked && Waiting4FirstMission())
        {
            is_waiting4FirstMission = false;
            Inpatience();
        }
        else if (is_waiting)
            Waiting();

        if (MM.checkAllMissionComplete(ID) && allow_picked && state == 2)
            state = 3;

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
            patientcontroller.finish_task(missionPoint);
        }

        if(allow_picked==false){
            missionPoint="";
        }

        if (agent.enabled && agent.velocity != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(-agent.velocity, Vector3.up); // navigation 看前方
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "chair")
        {
            int chair_idx = int.Parse(collision.transform.parent.transform.name.Replace("Chair_", ""));
            //把歸屬病人從PlayerBase改成這
            if (state == 0 && counter.chair[chair_idx] == -1 && !MM.isFull(lastPlayer))
            {
                MM.setOwner(ID,lastPlayer);
                hasOwner=true;

                state = 1; // 等櫃檯
                allow_picked = false;
                counter.enqueue(this, collision.transform.parent.transform.name);
            }
            if (state == 3 && counter.chair[chair_idx] == -1)
            {
                state = 4;
                allow_picked = false;
                counter.enqueue(this, collision.transform.parent.transform.name);
            }
        }

        if (state == 2 && collision.transform.tag == "task" && allow_picked){
            is_waiting4FirstMission = false;
            missionPoint = collision.GetComponent<Collider>().gameObject.name;
            //MM.updateTaskBar();

            if(MM.hasThisMission(ID, missionPoint) && !doingMission && MM.nextMissionName(ID)== missionPoint && patientcontroller.attempt_do_task(missionPoint))
            {
                is_waiting = false;
                doingMission = true;
                createTimer();
            }
        }
            

        if (collision.transform.tag == "exit")
        {
            if(MM.checkAllMissionComplete(ID)){
                //MM.score(ID,lastPlayer,point);
                Debug.Log(ID);

                MM.score(ID,point);
                MM.deleteMission(ID);
                Exiting();
            }
            else if(isLeaving)
            {
                //MM.deleteMission(ID);
                Destroy(gameObject);
            }
            else{
                Debug.Log("任務未完成");
            }
        }

        if (collision.transform.name == LineupPosition) // kids
        {
            agent.enabled = false;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (state == 4 && collision.transform.name == "離開點" && !is_waiting4FirstMission)
        {
            Destroy(gameObject);
        }
    }


    void OnTriggerExit(Collider collision)
    {
        if (collision.transform.root.transform.tag == "Player" || !allow_picked)
        {
           Dialog.SetActive(false);
        }
    }

    void OnTriggerStay(Collider collision)
    {
        //  if (collision.transform.root.transform.tag == "Player" && allow_picked)
        // {
        //    Dialog.SetActive(true);
        // }

        float dist2D = Vector3.Distance(new Vector3(collision.transform.position.x, 0, collision.transform.position.z), new Vector3(transform.position.x, 0, transform.position.z));
        if (collision.transform.tag == "chair" && dist2D < 0.6)
        {
            int chair_idx = int.Parse(collision.transform.parent.transform.name.Replace("Chair_", ""));
            if (state == 2 && counter.chair[chair_idx] == ID)
            {
                agent.enabled = false;
                allow_picked = true;
                counter.chair[chair_idx] = -1;
            }
            if (state == 5 && counter.chair[chair_idx] == ID)
            {
                agent.enabled = false;
                allow_picked = true;
                counter.chair[chair_idx] = -1;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (collision.transform.name == "1P") lastPlayer = 1;
            else if (collision.transform.name == "2P") lastPlayer = 2;
        }

        if (collision.transform.tag == "patient")
        {
            if (is_attacking)
            {
                MM.deleteMission(collision.gameObject.GetComponent<PatientBaseClass>().ID);
                //MM.deleteMission(ID);
                lineup.RemoveAPatient(collision.gameObject.GetComponent<PatientBaseClass>().ID);
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
