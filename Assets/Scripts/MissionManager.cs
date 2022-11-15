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
    int[] scoreArray = new int[] { 0, 0 };
    int missionCount = 0; //存在的任務數
    int ID = 0;    //任務編號
    //string[] missionType = new string[]{"抽血","量身高","心電圖","驗尿","量視力","X光"};
    string[] missionType = new string[] { "blood", "height", "ECG", "urine", "visual" };
    public float Timer = 2f;

    public class Mission
    {
        public int ID;
        public string[] type;
        public bool[] isComplete;
        public int[] whoComplete;
        public GameObject patient;
        public Mission(int ID, string[] type, GameObject patient)
        {
            this.ID = ID;
            this.type = type;
            this.patient = patient;
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
    public void createMission()
    {


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



        //創patient,隨機取一種人
        int patientRandom = random.Next(0, patientPrefabs.Length);
        GameObject patient = createPatient(patientRandom, ID, missionsRandom);


        Mission newMission = new Mission(ID, missionsRandom, patient);
        missionList.Add(newMission);


        missionCount += 1;
        ID += 1;
        updateTaskBar();

    }

    public void completeMission(int ID, string whatMission, int whoComplete)
    {
        int index = 0;
        for (int i = 0; i < missionList.Count; i++)
        {
            if (missionList[i].ID == ID)
            {
                index = i;
                break;
            }
        }

        for (int i = 0; i < missionList[index].type.Length; i++)
        {
            if (missionList[index].type[i] == whatMission)
            {
                missionList[index].isComplete[i] = true;
                missionList[index].whoComplete[i] = whoComplete;
            }
        }
        taskbar.transform.GetChild(index).gameObject.transform.GetComponent<TaskBar>().completeAnimation();
        updateTaskBar();

    }
    // 任務刪除
    public void deleteMission(int ID)
    {
        int index = 0;
        for (int i = 0; i < missionList.Count; i++)
        {
            if (missionList[i].ID == ID)
            {
                index = i;
                break;
            }
        }
        missionList.RemoveAt(index);
        missionCount -= 1;
        updateTaskBar();
    }

    public bool checkAllMissionComplete(int ID)
    {
        bool check = true;
        int index = 0;
        for (int i = 0; i < missionList.Count; i++)
        {
            if (missionList[i].ID == ID)
            {
                index = i;
                break;
            }
        }
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
    public void score(int ID, int player, int point)
    {
        //scoreArray[0]紀錄1p,scoreArray[1]紀錄2p
        scoreArray[player - 1] += 100; //送出院+100分

        int index = 0;
        for (int i = 0; i < missionList.Count; i++)
        {
            if (missionList[i].ID == ID)
            {
                index = i;
                break;
            }
        }


        for (int i = 0; i < missionList[index].whoComplete.Length; i++)
        {
            scoreArray[missionList[index].whoComplete[i] - 1] += point;
        }

        updateScoreBoard();
    }
    public string getDialogString(int ID)
    {
        int index = 0;
        for (int i = 0; i < missionList.Count; i++)
        {
            if (missionList[i].ID == ID)
            {
                index = i;
                break;
            }
        }


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

    public void missionFailed(int ID)
    {
        int index = findMissionIndex(ID);

        taskbar.transform.GetChild(index).gameObject.transform.GetComponent<TaskBar>().failedAnimation();
    }

    //病人有沒有該任務，有回傳True，沒有或已完成回傳false
    public bool hasThisMission(int ID, string whatMission)
    {
        int index = 0;
        for (int i = 0; i < missionList.Count; i++)
        {
            if (missionList[i].ID == ID)
            {
                index = i;
                break;
            }
        }

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

    // 創病人，input病人種類
    public Lineup lineup;
    GameObject createPatient(int patientType, int ID, string[] mission)
    {
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
        Debug.Log(patient.name.Replace("(Clone)", ""));//8+9(clone)

        string lineupPoint = "排隊點" + lineup.NewPatientEnter(patient);
        patient.GetComponent<PatientBaseClass>().LineupPosition = lineupPoint;
        patient.GetComponent<PatientBaseClass>().agent = patient.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (patient.name.Replace("(Clone)", "") != "Kids")
        {
            patient.GetComponent<PatientBaseClass>().agent.enabled = true;
            patient.GetComponent<PatientBaseClass>().NavigateTo(GameObject.Find(lineupPoint));
        }

        return patient;
    }


    int findMissionIndex(int ID)
    {
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

    void updateScoreBoard()
    {
        for (int i = 0; i < ScoreBoard.transform.childCount; i++)
        {
            string scoreBoardString = "";
            scoreBoardString = scoreArray[i].ToString();

            //ScoreBoard.transform.GetChild(i).GetComponent<Text>().text=scoreBoardString;
            ScoreBoard.transform.GetChild(i).gameObject.transform.GetComponentInChildren<Text>().text = scoreBoardString;
        }
    }

    public void updateTaskBar()
    {

        for (int i = 0; i < taskbar.transform.childCount; i++)
        {
            if (missionCount > i)
            {
                taskbar.transform.GetChild(i).gameObject.SetActive(true);
                string taskBarString = "";
                for (int j = 0; j < missionList[i].type.Length; j++)
                {
                    if (missionList[i].isComplete[j] == true)
                    {
                        taskBarString += missionList[i].type[j] + " (p" + missionList[i].whoComplete[j].ToString() + ")\n";
                    }
                    else
                    {
                        taskBarString += missionList[i].type[j] + "\n";
                    }
                }
                taskbar.transform.GetChild(i).gameObject.transform.GetComponentInChildren<Text>().text = taskBarString;
                string spriteName = missionList[i].patient.name.Replace("(Clone)", "");
                taskbar.transform.GetChild(i).gameObject.transform.GetComponent<TaskBar>().changeSprite(spriteName);



                string positionString="";
                if(missionList[i].patient.GetComponent<PatientBaseClass>().is_lineup){
                    positionString=missionList[i].patient.GetComponent<PatientBaseClass>().LineupPosition.Replace("排隊點","");
                    int n = int.Parse(positionString)+1;
                    positionString=n.ToString();
                }
                else{
                    if(!missionList[i].patient.GetComponent<PatientBaseClass>().allow_picked){
                        //pick的人
                        int lastPlayer=missionList[i].patient.GetComponent<PatientBaseClass>().lastPlayer;
                        if(lastPlayer==1){
                            positionString="1P";
                        }
                        else if(lastPlayer==2){
                            positionString="2P";
                        }
                        else{

                        }
                    }else{
                        //任務點
                        positionString=missionList[i].patient.GetComponent<PatientBaseClass>().missionPoint;
                    }

                }
                taskbar.transform.GetChild(i).gameObject.transform.Find("positionText").GetComponent<Text>().text=positionString;

            }
            else
            {
                taskbar.transform.GetChild(i).gameObject.SetActive(false);

            }


        }

    }
    


    // Start is called before the first frame update
    void Start()
    {
        lineup = GameObject.Find("生兵點").GetComponent<Lineup>();
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
}