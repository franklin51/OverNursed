using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Patient : MonoBehaviour
{
    public bool is_picked = false;
    public bool end_task = false; // 還沒用到
    public GeneratePoint GP;
    public int point = 100;

    public string[] mission;
    public bool[] isComplete;
    public int lastPlayer=0;
    public int ID=0;
    public MissionManager MM;
    [SerializeField] GameObject Dialog;
    [SerializeField] GameObject timerPrefabs;

    //float timer=0;
	GeneralPatient a;


    // Start is called before the first frame update
    void Start()
    {
        GP = GameObject.Find("生兵點").GetComponent<GeneratePoint>();
        MM = GameObject.Find("MissionManager").GetComponent<MissionManager>();
        updateDialogString();
        Dialog.SetActive(true);


		

    }
    void updateDialogString(){
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
        // timer +=Time.deltaTime;
        // if(timer>3){
        //     Dialog.SetActive(false);
        // }
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
            createTimer();
           }
           hasEntered1=true;
            string s="blood";
            completeMission(s);
            MM.completeMission(ID,s);
        
        }

        if (collision.transform.tag == "task2" && is_picked == false && !end_task)
        {

            if(!hasEntered2){
            createTimer();
           }
           hasEntered2=true;
            string s="height";
            completeMission(s);
            MM.completeMission(ID,s);
        
        }

        if (collision.transform.tag == "task3" && is_picked == false && !end_task)
        {

            if(!hasEntered3){
            createTimer();
           }
           hasEntered3=true;
            string s="ECG";
            completeMission(s);
            MM.completeMission(ID,s);
        
        }

        if (collision.transform.tag == "task4" && is_picked == false && !end_task)
        {

            if(!hasEntered4){
            createTimer();
           }
           hasEntered4=true;
            string s="urine";
            completeMission(s);
            MM.completeMission(ID,s);
        
        }

        if (collision.transform.tag == "task5" && is_picked == false && !end_task)
        {

            if(!hasEntered5){
            createTimer();
           }
           hasEntered5=true;
            string s="visual";
            completeMission(s);
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
