using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AngryGrandma : PatientBaseClass
{
    [SerializeField] GameObject Anger;
    public bool isAngry=false;

    override protected bool Waiting4FirstMission() // ?Ÿå…µå¾Œç?å¾…ç¬¬ä¸€?‹ä»»?™ï?return trueè¡¨ç¤ºç­‰ä??Šä?ï¼Œé€²å…¥Inpatience?½å?
    {
        //agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //agent.SetDestination(GameObject.Find("1P").transform.position);
        return false;
	}

	override protected bool ExecuteMission() // ?·è?ä»»å?ï¼Œreturn trueè¡¨ç¤º?å??·è?
	{
		return true;
	}

	override protected bool Waiting() // ä»»å?å®Œæ?å¾Œç?å¾…ä?ä¸€?‹ä»»?™ï?return trueè¡¨ç¤ºç­‰ä??Šä?ï¼Œé€²å…¥Inpatience?½å?
    {
        Invoke("createAnger", 4.0f); 
        if (isAngry == true && !allow_picked)
        {
            isAngry = false;
            deleteAnger();
        }
        return false;
    }

	override protected void Inpatience() // ç­‰ä??Šé?å§‹æ?äº?
    {
        Destroy(gameObject);
    }

   


    void deleteAnger(){
        Debug.Log("deleteAnger");
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
