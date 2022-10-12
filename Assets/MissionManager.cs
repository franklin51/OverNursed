using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeField] GameObject[] missionPrefabs;
    [SerializeField] GameObject[] peoplePrefabs;
    [SerializeField] GameObject taskbar;
    int missionCount=0; //還存在的任務
    int missionNum=0;    
    string[] missionType = new string[]{"抽血","量身高","心電圖","驗尿","量視力","X光"};
    

    // 隨機創任務
   public void CreateMission(){
        // int r =1;
        int r = Random.Range(0, missionPrefabs.Length);
        GameObject mission =Instantiate(missionPrefabs[r], transform);
        mission.transform.position = new Vector3(-7.5f+missionCount*2.8f, 4f, 0f);
        mission.GetComponent<Mission>().num=missionCount;

        // Random random = new Random();
        // List<int> indices = new List<int>();
        // while (indices.Count < 2)
        // {
        //     int index = random.Next(0, missionType.Length);
        //     if (indices.Count == 0 || !indices.Contains(index))
        //     {
        //         indices.Add(index);
        //     }
        // }

        // string[] missions = new string[4];
        // for (int i = 0; i < indices.Count; i++)
        // {
        //     int randomIndex = indices[i];
        //     missions[i] = missionType[randomIndex];
        // }
        
        // for(int i=0; i<missions.Length;i++){
        //     Debug.Log(missions[i]);
        // }



        CreatePeople(r, missionNum);

        missionCount+=1;
        missionNum+=1;
        
        UpdateTaskBar();
        
   }

    // 任務完成
   public void DeleteMission(){
        DeletePeople();
        missionCount-=1;
   }

    // 創病人，input病人種類
   public void CreatePeople(int peopleType, int num){
        GameObject people =Instantiate(peoplePrefabs[peopleType], transform);
        people.transform.position = new Vector3(-7.5f+missionCount*2.8f, 1.1f, 0f);
        people.GetComponent<people>().num=num;

   }

   public void DeletePeople(){

   }

    void UpdateTaskBar(){
        
        for (int i=0; i<taskbar.transform.childCount; i++){
            if(missionCount>i){
                taskbar.transform.GetChild(i).gameObject.SetActive(true);
                
            }
            else{
                taskbar.transform.GetChild(i).gameObject.SetActive(false);
                
            }
        }
        taskbar.transform.GetChild(missionCount).gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Mouse1)){
            if(missionCount<6){
                CreateMission();
            }
            else{
                Debug.Log("full");
            }
           
        }
    }
}
