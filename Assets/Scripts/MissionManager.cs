using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class MissionManager : MonoBehaviour
{
    [SerializeField] GameObject[] patientPrefabs;
    GameObject taskbar1P;
    GameObject taskbar2P;
    GameObject ScoreBoard;
    int[] scoreArray = new int[] { 0, 0 };
    int missionCount = 0; //存在的任務數
    int ID = 0;    //任務編號
    string[] missionType = new string[] { "blood", "height", "ECG", "urine", "visual" };
    string[] AngryGrandmaMission = new string[] { "ECG", "urine", "visual" };
    string[] EngineerMission = new string[] { "blood","ECG", "urine", "visual" };
    string[] KidsMission = new string[] {"height", "visual" };
    string[] NormalMission = new string[] { "blood", "height","visual" };
    string[] TerroristMission = new string[] {"ECG", "urine", "blood" };
    string[] OldmanMission = new string[] {"visual", "urine", "ECG", "blood" };

    SoundEffects SE;
    public float Timer = 2f;

    public class Mission
    {
        public int ID;
        public string[] type;
        public bool[] isComplete;
        public int[] whoComplete;
        public int owner;
        public GameObject patient;
        public Mission(int ID, string[] type, GameObject patient)
        {
            this.ID = ID;
            this.type = type;
            this.patient = patient;
            this.owner = 0;
            this.isComplete = new bool[this.type.Length];
            for (int i = 0; i < this.isComplete.Length; i++)
            {
                this.isComplete[i] = false;
            }
            this.whoComplete = new int[this.type.Length];
            for (int i = 0; i < this.whoComplete.Length; i++)
            {
                this.whoComplete[i] = 0;
            }
        }
    }
    List<Mission> missionList = new List<Mission>();


    // 隨機創任務
    public void createMission(){


        Random random = new Random();

        //隨機取兩個任務
        List<int> indices = new List<int>();
        int TasksCount=4;
        while (indices.Count < TasksCount)
        {
            int index = random.Next(0, missionType.Length);
            if (indices.Count == 0 || !indices.Contains(index))
            {
                indices.Add(index);
            }
        }

        string[] missionsRandom = new string[TasksCount];
        for (int i = 0; i < indices.Count; i++)
        {
            int randomIndex = indices[i];
            missionsRandom[i] = missionType[randomIndex];
        }



        //創patient,隨機取一種人
        int patientRandom = random.Next(0, patientPrefabs.Length);
        // GameObject patient = createPatient(patientRandom, ID, missionsRandom);
        // Mission newMission = new Mission(ID, missionsRandom, patient);
        //暫時先改這樣
        GameObject patient;
        Mission newMission;
        if(patientRandom==0){
            patient = createPatient(patientRandom, ID, AngryGrandmaMission);
            newMission = new Mission(ID, AngryGrandmaMission, patient);
        }
        else if(patientRandom==1){
            patient = createPatient(patientRandom, ID, EngineerMission);
            newMission = new Mission(ID, EngineerMission, patient);
            
        }
        else if(patientRandom==2){
            patient = createPatient(patientRandom, ID, KidsMission);
            newMission = new Mission(ID, KidsMission, patient);
            
        }
        else if(patientRandom==3){
            patient = createPatient(patientRandom, ID, NormalMission);
            newMission = new Mission(ID, NormalMission, patient);
        }
        else if(patientRandom==4){
            patient = createPatient(patientRandom, ID, TerroristMission);
            newMission = new Mission(ID, TerroristMission, patient);
            
        }
        else {
            patient = createPatient(patientRandom, ID, OldmanMission);
            newMission = new Mission(ID, OldmanMission, patient);
            
        }

        missionList.Add(newMission);
        missionCount += 1;
        ID += 1;
        //updateTaskBar();

    }

    // 創病人，input病人種類
    public Lineup lineup;
    GameObject createPatient(int patientType, int ID, string[] mission){
        GameObject generatePoint = GameObject.Find("生兵點");
        Vector3 pos;
        if (patientPrefabs[patientType].transform.name == "Kids")
            pos = new Vector3(generatePoint.transform.position.x, 2f, generatePoint.transform.position.z);
        else
            pos = generatePoint.transform.position;

        GameObject patient = Instantiate(patientPrefabs[patientType], pos, generatePoint.transform.rotation);
        patient.GetComponent<PatientBaseClass>().ID = ID;
        patient.GetComponent<PatientBaseClass>().mission = mission;
        patient.GetComponent<PatientBaseClass>().isComplete = new bool[] { false, false };
        //Debug.Log(patient.name.Replace("(Clone)", ""));//8+9(clone)

        string lineupPoint = "排隊點" + lineup.NewPatientEnter(patient);
        patient.GetComponent<PatientBaseClass>().LineupPosition = lineupPoint;
        patient.GetComponent<PatientBaseClass>().agent = patient.GetComponent<UnityEngine.AI.NavMeshAgent>();
        
        patient.GetComponent<PatientBaseClass>().agent.enabled = true;
        patient.GetComponent<PatientBaseClass>().NavigateTo(GameObject.Find(lineupPoint));
        

        return patient;
    }

    /*Mission
       1. setOwner(int ID,int player)
       2.changeOwner(int ID,int player)
       3. completeMission(int ID, string whatMission, int whoComplete)
       4. deleteMission(int ID)
       5. score(int ID, int point)
    */

    public void setOwner(int ID,int player){
        int index=findMissionIndex(ID);
        missionList[index].owner=player;
        if(player==1) taskbar1P.GetComponent<TaskBar>().createRecord(missionList[index].ID, missionList[index].patient.name.Replace("(Clone)", ""), missionList[index].type);
        if(player==2) taskbar2P.GetComponent<TaskBar>().createRecord(missionList[index].ID, missionList[index].patient.name.Replace("(Clone)", ""), missionList[index].type);
    }

    public void changeOwner(int ID,int player){
        int index=findMissionIndex(ID);
        int owner=findMissionOwner(ID);
        
        if(player==1 && player!=owner  &&owner!=0){
            taskbar2P.GetComponent<TaskBar>().deleteRecord(missionList[index].ID);
            taskbar1P.GetComponent<TaskBar>().createRecord(missionList[index].ID, missionList[index].patient.name.Replace("(Clone)", ""), missionList[index].type);
            int completeNum=0;
            for(int i=0; i<missionList[index].isComplete.Length; i++){
                if(missionList[index].isComplete[i]){
                    completeNum++;
                }
            }
            taskbar1P.GetComponent<TaskBar>().alreadyCompleted(ID,completeNum);
            missionList[index].owner=player;
        } 
        if(player==2 && player!=owner &&owner!=0){ 
            taskbar1P.GetComponent<TaskBar>().deleteRecord(missionList[index].ID);
            taskbar2P.GetComponent<TaskBar>().createRecord(missionList[index].ID, missionList[index].patient.name.Replace("(Clone)", ""), missionList[index].type);
            int completeNum=0;
            for(int i=0; i<missionList[index].isComplete.Length; i++){
                if(missionList[index].isComplete[i]){
                    completeNum++;
                }
            }
            taskbar2P.GetComponent<TaskBar>().alreadyCompleted(ID,completeNum);
            missionList[index].owner=player;
        }

        
    }

    public void completeMission(int ID, string whatMission, int whoComplete){
        if(existID(ID)){
            int index = findMissionIndex(ID);
            int i;
            for (i=0 ; i < missionList[index].type.Length; i++)
            {
                if (missionList[index].type[i] == whatMission)
                {
                    missionList[index].isComplete[i] = true;
                    missionList[index].whoComplete[i] = whoComplete;
                    break;
                }
            }
            Debug.Log("whoComplete:"+whoComplete.ToString());

            if(whoComplete==1) taskbar1P.GetComponent<TaskBar>().completeTask(ID,i);
            else if(whoComplete==2) taskbar2P.GetComponent<TaskBar>().completeTask(ID,i);

            SE.PlaySoundEffect(whatMission);
        }
    }

    public void deleteMission(int ID){
        if(existID(ID)){
            int index = findMissionIndex(ID);
            int player = findMissionOwner(ID);
            missionList.RemoveAt(index);
            missionCount -= 1;

            

            if(player==1){ 
                Debug.Log("deleteMission1");
                taskbar1P.GetComponent<TaskBar>().deleteRecord(ID);
            }
            if(player==2) {
                Debug.Log("deleteMission2");
                taskbar2P.GetComponent<TaskBar>().deleteRecord(ID);
            }
        }
    }

    public void score(int ID, int point){
       if(existID(ID)){
            int index = findMissionIndex(ID);
            int player=missionList[index].owner - 1;
            scoreArray[player] +=point;
            Debug.Log("player"+player.ToString()+"+"+point.ToString());

            updateScoreBoard();
       }
    }

    /*string
       1. getDialogString(int ID)
       2. nextMissionName(int ID)
    */

    public string getDialogString(int ID){
        int index = findMissionIndex(ID);


        string s = "";
        for (int i = 0; i < missionList[index].type.Length; i++)
        {


            if (missionList[index].isComplete[i] == true)
            {
                s = s + missionList[index].type[i] + " (ok)\n";
            }
            else
            {
                s = s + missionList[index].type[i] + "\n";
            }
        }

        return s;
    }

    public string nextMissionName(int ID){
        int index = findMissionIndex(ID);
        string name="";
        for(int i=0; i<missionList[index].isComplete.Length; i++ ){
            if(!missionList[index].isComplete[i]){
                name=missionList[index].type[i];
                break;
            }
        }

        return name;
    }

    /*bool
        1. hasThisMission(int ID, string whatMission)
        2. checkAllMissionComplete(int ID)
        3. isFull(int player)
    */

    public bool hasThisMission(int ID, string whatMission){
        int index = findMissionIndex(ID);

        for (int i = 0; i < missionList[index].type.Length; i++)
        {
            if (missionList[index].type[i] == whatMission)
            {
                if (missionList[index].isComplete[i] == false)
                    return true;
            }
        }
        return false;
    }

    

    public bool checkAllMissionComplete(int ID){
        bool check = true;
        int index = findMissionIndex(ID);
        for (int i = 0; i < missionList[index].isComplete.Length; i++)
        {
            if (missionList[index].isComplete[i] == false)
            {
                check = false;
                break;
            }
        }

        return check;

    }

    public bool isFull(int player){
        int count=0;
        for(int i = 0 ; i < missionList.Count ; i++){
            if(missionList[i].owner==player) count++;
        }

        if(count<5){
            Debug.Log("ok");
            return false;
        }
        else{
            Debug.Log("full");
            return true;
        }
    }

    

    /*UI
        1. updateScoreBoard() 
    */

    void updateScoreBoard(){
        for (int i = 0; i < ScoreBoard.transform.childCount; i++)
        {
            string scoreBoardString = "";
            scoreBoardString = scoreArray[i].ToString();

            //ScoreBoard.transform.GetChild(i).GetComponent<Text>().text=scoreBoardString;
            ScoreBoard.transform.GetChild(i).gameObject.transform.GetComponentInChildren<Text>().text = scoreBoardString;
        }
    }


    /*some funciton
        1. findMissionIndex(int ID)
        2. findMissionIndexInOwner(int ID,int player)
        3. findMissionOwner(int ID)
    
    */

    int findMissionIndex(int ID){
        int index = 0;
        for (int i = 0; i < missionList.Count; i++)
        {
            if (missionList[i].ID == ID)
            {
                index = i;
                break;
            }
        }
        return index;
    }

    int findMissionIndexInOwner(int ID,int player){
        int index=0;
        for(int i = 0 ; i < missionList.Count ; i++){
            if(missionList[i].owner==player && missionList[i].ID==ID){
                break;
            }
            else if (missionList[i].owner==player && missionList[i].ID!=ID)
            {
                index++;
            }
        }
        return index;
    }

    public int findMissionOwner(int ID){
        int index=findMissionIndex(ID);
        return missionList[index].owner;
           
    }

    bool existID(int ID){
        bool b=false;
        for(int i = 0 ; i < missionList.Count ; i++){
            if(missionList[i].ID==ID){
                b=true;
                break;
            }
            
        }
        return b;
    }

    
    
    /*animation
        1. pickPatient(int ID)
        2. putDownPatient(int ID)
        3. missionFailed(int ID, string whatMission)
    */
    public void pickPatient(int ID){
        int owner = findMissionOwner(ID);
        int index = findMissionIndexInOwner(ID,owner);
        
        if(owner==1){
            taskbar1P.GetComponent<TaskBar>().pickTask(ID);
        }
        else if(owner==2){
            taskbar2P.GetComponent<TaskBar>().pickTask(ID);
        }

    }
    public void putDownPatient(int ID){
        int owner = findMissionOwner(ID);
        int index = findMissionIndexInOwner(ID,owner);
        if(owner==1){
            taskbar1P.GetComponent<TaskBar>().putDownTask(ID);
        }
        else if(owner==2){
            taskbar2P.GetComponent<TaskBar>().putDownTask(ID);
        }
    }
    
    

    public void missionFailed(int ID, string whatMission){
        int index = findMissionIndex(ID);
        int i;
        int owner=missionList[index].owner;
        for (i=0 ; i < missionList[index].type.Length; i++)
        {
            if (missionList[index].type[i] == whatMission)
            {
                
                break;
            }
        }

        if(owner==1){
            taskbar1P.GetComponent<TaskBar>().failedTask(ID,i);
        }
        else if(owner==2){
            taskbar2P.GetComponent<TaskBar>().failedTask(ID,i);
        }
        
    }


    // Start is called before the first frame update
    void Start()
    {
        SE = GameObject.Find("Main Camera").GetComponent<SoundEffects>();
        lineup = GameObject.Find("生兵點").GetComponent<Lineup>();
        taskbar1P = GameObject.Find("taskbar(1P)");
        taskbar2P = GameObject.Find("taskbar(2P)");
        ScoreBoard = GameObject.Find("ScoreBoard");
    }

    // Update is called once per frame
    void Update()
    {
        GeneratePatients();

        for (int i = 0; i < scoreArray.Length; i++)
        {
            if (scoreArray[i] >= 1000)
            {
                SceneManager.LoadScene("ED", LoadSceneMode.Single);
            }
        }
    }

    void GeneratePatients()
    {
        Timer -= Time.deltaTime;
        if (lineup.count < lineup.length && Timer <= 0)
        {
            createMission();
            Timer = 2;
        }
    }

    public void rob(string player_name, int num)
    {
        if (player_name == "1P")
            scoreArray[0] = Math.Max(0, scoreArray[0] - num);
        else if (player_name == "2P")
            scoreArray[1] = Math.Max(0, scoreArray[1] - num);
        updateScoreBoard();
    }
}