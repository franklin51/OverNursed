using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AngryGrandma : PatientBaseClass
{
    [SerializeField] GameObject Anger;
    public bool isAngry=false;
    
	private GameObject target;
    float Timer=4.0f;


    override protected bool Waiting4FirstMission() // ?�兵後�?待第一?�任?��?return true表示等�??��?，進入Inpatience?��?
    {
        //agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //agent.SetDestination(GameObject.Find("1P").transform.position);
        return false;
	}

	override protected bool ExecuteMission() // ?��?任�?，return true表示?��??��?
	{
		return true;
	}

	override protected bool Waiting() // 任�?完�?後�?待�?一?�任?��?return true表示等�??��?，進入Inpatience?��?
    {
        //Invoke("createAnger", 4.0f); 
        if(allow_picked){
            Timer-=Time.deltaTime;
        }
        if(Timer<0){
            createAnger();
        }
        if (isAngry == true && !allow_picked )
        {
            Timer=4.0f;
            isAngry = false;
            deleteAnger();
        }
        return false;
    }

	override protected void Inpatience() // 等�??��?始�?�?
    {
        agent.enabled = true;
		agent.speed = 40f;
		agent.acceleration = 1000f;
		if(!isLeaving){
            deleteAnger();
		    target = GameObject.Find("離開點");
        }

		NavigateTo(target);
		isLeaving = true;

        // Destroy(gameObject);
    }

   

    void deleteAnger(){
       
        Destroy(gameObject.transform.Find("Canvas").transform.Find("Anger(Clone)").gameObject);
    }
    void createAnger(){
        if(isAngry==false && allow_picked &&doingMission==false){
            isAngry=true;
            GameObject anger = Instantiate(Anger, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            anger.transform.SetParent (transform.GetChild(1), false);
            anger.GetComponent<Anger>().lookAt=transform;
        }
    }
}
