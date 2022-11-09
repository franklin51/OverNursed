using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 與操作、碰撞偵測有關的
public class PatientBaseClass : MonoBehaviour
{
    public bool is_picked = false;
    public bool end_task = false; // 還沒用到
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


    

    // Update is called once per frame
    public virtual void Update()
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


        if (collision.transform.tag == "task" && is_picked == false && !end_task)
        {
           if(!hasEntered1){
            doingMission=true;
            createTimer();
           }
           hasEntered1=true;

           if(timerOK==true){
                timerOK=false;
                doingMission=false;
                string s="blood";
                MM.completeMission(ID,s,lastPlayer);
                updateDialogString();
                
           }
            
        
        }

        if (collision.transform.tag == "task2" && is_picked == false && !end_task)
        {

            if(!hasEntered2){
                doingMission=true;
                createTimer();
           }
           hasEntered2=true;
           if(timerOK==true){
                timerOK=false;
                doingMission=false;
                string s="height";
                MM.completeMission(ID,s,lastPlayer);
                updateDialogString();
                
           }
        }

        if (collision.transform.tag == "task3" && is_picked == false && !end_task)
        {

            if(!hasEntered3){
                doingMission=true;
                createTimer();
           }
           hasEntered3=true;
           if(timerOK==true){
                timerOK=false;
                doingMission=false;
                string s="ECG";
                MM.completeMission(ID,s,lastPlayer);
                updateDialogString();
                
           }
        }

        if (collision.transform.tag == "task4" && is_picked == false && !end_task)
        {

            if(!hasEntered4){
                doingMission=true;
                createTimer();
           }
           hasEntered4=true;
           if(timerOK==true){
                timerOK=false;
                doingMission=false;
                string s="urine";
                MM.completeMission(ID,s,lastPlayer);
                updateDialogString();
                
           }
        }

        if (collision.transform.tag == "task5" && is_picked == false && !end_task)
        {

            if(!hasEntered5){
                doingMission=true;
                createTimer();
           }
           hasEntered5=true;
           if(timerOK==true){
                timerOK=false;
                doingMission=false;
                string s="visual";
                MM.completeMission(ID,s,lastPlayer);
                updateDialogString();
                
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
