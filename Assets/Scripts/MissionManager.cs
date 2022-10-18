using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class MissionManager : MonoBehaviour
{
    [SerializeField] GameObject[] patientPrefabs;
    [SerializeField] GameObject taskbar;
    [SerializeField] GameObject ScoreBoard;
    int[] scoreArray = new int[]{0,0};
    int missionCount=0; //存在的任務數
    int ID=0;    //任務編號
    //string[] missionType = new string[]{"抽血","量身高","心電圖","驗尿","量視力","X光"};
    string[] missionType = new string[]{"抽血","量身高","心電圖","驗尿","量視力"};
    
    public class Mission
    {
        public int ID;
        public string[] type;
        public bool[] isComplete;
        public  Mission(int ID, string[] type)
        {
            this.ID = ID;
            this.type = type;
            isComplete= new bool[]{false,false};
        }
    }
    List<Mission> missionList = new List<Mission>();
    
    

    // 隨機創任務
    public void createMission(){
        
        
        Random random = new Random();

        //隨機取兩個任務
        List<int> indices = new List<int>();
        while (indices.Count < 2)
        {
            int index = random.Next(0, missionType.Length);
            if (indices.Count == 0 || !indices.Contains(index))
            {
                indices.Add(index);
            }
        }

        string[] missionsRandom = new string[2];
        for (int i = 0; i < indices.Count; i++)
        {
            int randomIndex = indices[i];
            missionsRandom[i] = missionType[randomIndex];
        }
        
        for(int i=0; i<missionsRandom.Length;i++){
            Debug.Log(missionsRandom[i]);
        }

        Mission newMission = new Mission(ID,missionsRandom);
        missionList.Add(newMission);

        //創patient,隨機取一種人
        int patientRandom = random.Next(0, patientPrefabs.Length);
        createPatient(patientRandom, ID, missionsRandom);


        missionCount+=1;
        ID+=1;
        updateTaskBar();
        
    }

    public void completeMission(int ID, string whatMission){
        int index=0;
        for(int i=0 ; i<missionList.Count ; i++ ){
            if(missionList[i].ID==ID){
                index=i;
                break;
            }
        }

        for(int i =0; i<missionList[index].type.Length; i++){
            if(missionList[index].type[i]==whatMission){
                missionList[index].isComplete[i]=true;
            }
        }

        updateTaskBar();

    }
    // 任務完成
    public void deleteMission(int ID){
        int index=0;
        for(int i=0 ; i<missionList.Count ; i++ ){
            if(missionList[i].ID==ID){
                index=i;
                break;
            }
        }
        missionList.RemoveAt(index);
        missionCount-=1;
        updateTaskBar();
    }

    public bool checkAllMissionComplete(int ID){
        bool check=true;
        int index=0;
        for(int i=0 ; i<missionList.Count ; i++ ){
            if(missionList[i].ID==ID){
                index=i;
                break;
            }
        }
        for(int i=0; i<missionList[index].isComplete.Length; i++){
            if(missionList[index].isComplete[i]==false){
                check=false;
                break;
            }
        }

        return check;

    }

    // 創病人，input病人種類
    void createPatient(int patientType, int ID, string[] mission){

        GameObject generatePoint = GameObject.Find("生兵點");
        var distance = new Vector3(0f, 0f, missionCount*3f);

        GameObject patient =Instantiate(patientPrefabs[patientType], generatePoint.transform.position+distance, generatePoint.transform.rotation);
        patient.GetComponent<Patient>().ID=ID;
        patient.GetComponent<Patient>().mission=mission;
        patient.GetComponent<Patient>().isComplete = new bool[]{false,false};

    }

    public void score(int player,int point){
        scoreArray[player-1]+=point;
        updateScoreBoard();
    }

    void updateScoreBoard(){
        for(int i=0; i<ScoreBoard.transform.childCount; i++){
            string scoreBoardString = "";
            scoreBoardString =scoreArray[i].ToString();

            //ScoreBoard.transform.GetChild(i).GetComponent<Text>().text=scoreBoardString;
            ScoreBoard.transform.GetChild(i).gameObject.transform.GetComponentInChildren<Text>().text=scoreBoardString;
        }
    }

    void updateTaskBar(){
        
        for (int i=0; i<taskbar.transform.childCount; i++){
            if(missionCount>i){
                taskbar.transform.GetChild(i).gameObject.SetActive(true);
                string taskBarString="";
                for(int j=0;j<missionList[i].type.Length;j++){
                    if(missionList[i].isComplete[j]==true){
                        taskBarString+=missionList[i].type[j]+"√\n";
                    }
                    else{
                        taskBarString+=missionList[i].type[j]+"\n";
                    }
                }
                taskbar.transform.GetChild(i).gameObject.transform.GetComponentInChildren<Text>().text=taskBarString;
                
            }
            else{
                taskbar.transform.GetChild(i).gameObject.SetActive(false);
                
            }


        }

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Mouse1)){
            if(missionCount<5){
                createMission();
            }
            else{
                Debug.Log("full");
            }
           
        }

        for(int i=0; i<scoreArray.Length; i++){
            if(scoreArray[i]>=500){
                SceneManager.LoadScene("ED", LoadSceneMode.Single);
            }
        }
    }
}
