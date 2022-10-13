using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class MissionManager : MonoBehaviour
{
    [SerializeField] GameObject[] peoplePrefabs;
    [SerializeField] GameObject taskbar;
    int missionCount=0; //存在的任務數
    int missionNum=0;    //任務編號
    string[] missionType = new string[]{"抽血","量身高","心電圖","驗尿","量視力","X光"};
    
    public class Mission
    {
        public int num;
        public string[] type;
        public bool[] isComplete;
        public  Mission(int num, string[] type)
        {
            this.num = num;
            this.type = type;
            isComplete= new bool[]{false,false};
        }
    }
    List<Mission> missionList = new List<Mission>();
    

    // 隨機創任務
    public void CreateMission(){
        
        //隨機取一種人
        Random random = new Random();
        int peopleRandom = random.Next(0, peoplePrefabs.Length);
        CreatePeople(peopleRandom, missionNum);


        //每個人隨機取兩個任務
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

        Mission newMission = new Mission(missionNum,missionsRandom);
        missionList.Add(newMission);


        missionCount+=1;
        missionNum+=1;
        UpdateTaskBar();
        
    }

    // 任務完成
    public void DeleteMission(int num){
        for(int i=0 ; i<missionList.Count ; i++ ){

        }
        DeletePeople(num);
        missionCount-=1;
        UpdateTaskBar();
    }

    // 創病人，input病人種類
    public void CreatePeople(int peopleType, int num){
        GameObject people =Instantiate(peoplePrefabs[peopleType], transform);
        people.transform.position = new Vector3(-7.5f+missionCount*2.8f, 1.1f, 0f);
        people.GetComponent<people>().num=num;

    }

    public void DeletePeople(int num){

    }

    void UpdateTaskBar(){
        
        for (int i=0; i<taskbar.transform.childCount; i++){
            if(missionCount>i){
                taskbar.transform.GetChild(i).gameObject.SetActive(true);
                taskbar.transform.GetChild(i).gameObject.transform.GetComponentInChildren<Text>().text=missionList[i].type[0]+"\n"+missionList[i].type[1];
                
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
                CreateMission();
            }
            else{
                Debug.Log("full");
            }
           
        }
    }
}
