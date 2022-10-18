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
    //[SerializeField] GameObject DialogText;
    [SerializeField] GameObject Dialog;

    // Start is called before the first frame update
    void Start()
    {
        GP = GameObject.Find("生兵點").GetComponent<GeneratePoint>();
        MM = GameObject.Find("MissionManager").GetComponent<MissionManager>();
        updateDialogString();

    }
    void updateDialogString(){
        //DialogText.transform.GetComponent<Text>().text = getDialogString();
        Dialog.transform.GetComponentInChildren<Text>().text=getDialogString();
    }

    string getDialogString(){
        string s="";
        for (int i=0; i<mission.Length; i++){
            if(isComplete[i]==true){
                s = s + mission[i] + "√\n";
            }
            else{
                s = s + mission[i] + "\n";
            }
        }

        return s;
    }

    void showDialog(){

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

        if (collision.transform.tag == "task" && is_picked == false && !end_task)
        {
           
            string s="抽血";
            completeMission(s);
            MM.completeMission(ID,s);
        
        }

        if (collision.transform.tag == "task2" && is_picked == false && !end_task)
        {
            string s="量身高";
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
