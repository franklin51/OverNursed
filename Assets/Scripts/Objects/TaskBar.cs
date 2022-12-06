using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskBar : MonoBehaviour
{   
    [SerializeField] GameObject[] RecordPrefabs;
    List<GameObject> RecordList = new List<GameObject>();
    int recordCount = 0;
    float firstPosition=294.0f;
    float distance=147.0f;

    public void createRecord(int ID, string patientType, string[] tasksType){
        Vector3 pos = transform.position;
        Vector3 offset = new Vector3(recordCount*distance-firstPosition, 0, 0);
        GameObject record = Instantiate(RecordPrefabs[0], pos+offset , Quaternion.identity, transform);
        record.GetComponent<Record>().setInitialValue(ID, patientType, tasksType);
        // record.GetComponent<Record>().ID = ID;
        // record.GetComponent<Record>().changePatientSprite(patientType);
        // if(tasksType.Length<=4){
        //     for(int i=0; i<tasksType.Length; i++){
        //         record.GetComponent<Record>().changeTaskSprite(i, tasksType[i]);
        //     }
        // }
        // else
        //     Debug.Log("tasksType Length error");
        
        recordCount++;
        RecordList.Add(record);
    }

    public void deleteRecord(int ID){
        int index = findIndex(ID);
        Debug.Log("deleteRecord");
        Destroy(RecordList[index]);
        RecordList.RemoveAt(index);
        recordCount--;
        
        reArrange();
    }
    

    public void completeTask(int ID, int taskNum){
        int index = findIndex(ID);
        RecordList[index].GetComponent<Record>().completeAnimation(taskNum);

    }

    public void failedTask(int ID, int taskNum){
        int index = findIndex(ID);
        RecordList[index].GetComponent<Record>().failedAnimation(taskNum);
    }

    public void pickTask(int ID){
        int index = findIndex(ID);
        RecordList[index].GetComponent<Record>().pickAnimation();
    }

    public void putDownTask(int ID){
        int index = findIndex(ID);
        RecordList[index].GetComponent<Record>().emptyAnimation();
    }

    public void reArrange(){
        Vector3 pos = transform.position;
        
        for(int i=0; i<RecordList.Count; i++){
            Vector3 offset = new Vector3(i*distance-firstPosition, 0, 0);
            RecordList[i].transform.position= pos+offset;
        }

    }
    // public void setPatientImage(int index, string patientType){

    // }
    // public void setTaskImage(int index, int taskNum, string taskType){

    // }

    int findIndex(int ID){
        int index=0;
        for(int i=0; i<RecordList.Count; i++){
            if(RecordList[i].GetComponent<Record>().ID==ID){
                index=i;
                break;
            }
        }
        return index;
    }

    
}
