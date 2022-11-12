using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AngryGrandma : PatientBaseClass
{
    [SerializeField] GameObject Anger;
    public bool isAngry=false;
    public NavMeshAgent agent;

    override protected bool Waiting4FirstMission() // 生兵後等待第一個任務，return true表示等不及了，進入Inpatience函式
    {
        //agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //agent.SetDestination(GameObject.Find("1P").transform.position);
        return false;
	}

	override protected bool ExecuteMission() // 執行任務，return true表示成功執行
	{
		return true;
	}

	override protected bool Waiting() // 任務完成後等待下一個任務，return true表示等不及了，進入Inpatience函式
    {
        Invoke("createAnger", 4.0f); 
        Debug.Log(is_picked == true);
        if (isAngry == true && is_picked == true)
        {
            isAngry = false;
            deleteAnger();
        }
        return false;
    }

	override protected void Inpatience() // 等不及開始搞事
    {
        Destroy(gameObject);
    }

   


    void deleteAnger(){
        Debug.Log("deleteAnger");
        Destroy(gameObject.transform.Find("Canvas").transform.Find("Anger(Clone)").gameObject);
    }
    void createAnger(){
        if(isAngry==false && is_picked==false &&doingMission==false){
            isAngry=true;
            GameObject anger = Instantiate(Anger, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            anger.transform.SetParent (transform.GetChild(1), false);
            anger.GetComponent<Anger>().lookAt=transform;
        }
    }
}
