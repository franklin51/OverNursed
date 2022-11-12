using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AngryGrandma : PatientBaseClass
{
    [SerializeField] GameObject Anger;
    public bool isAngry=false;
    public NavMeshAgent agent;

    override protected bool Waiting4FirstMission() // �ͧL�ᵥ�ݲĤ@�ӥ��ȡAreturn true��ܵ����ΤF�A�i�JInpatience�禡
    {
        //agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //agent.SetDestination(GameObject.Find("1P").transform.position);
        return false;
	}

	override protected bool ExecuteMission() // ������ȡAreturn true��ܦ��\����
	{
		return true;
	}

	override protected bool Waiting() // ���ȧ����ᵥ�ݤU�@�ӥ��ȡAreturn true��ܵ����ΤF�A�i�JInpatience�禡
    {
        Invoke("createAnger", 4.0f); //?��?解race condition?�這樣寫�??�好也蠻符�??��?!?
        Debug.Log(is_picked == true);
        if (isAngry == true && is_picked == true)
        {
            isAngry = false;
            deleteAnger();
        }
        return false;
    }

	override protected void Inpatience() // �����ζ}�l�d��
    {
        Destroy(gameObject);
    }

    /*public override void Update(){
        base.Update();

        if(isAngry==false && is_picked==false &&doingMission==false){//doingMission?��?race condition
            Invoke("createAnger",4.0f); //?��?解race condition?�這樣寫�??�好也蠻符�??��?!?
        }
        if(isAngry==true && is_picked==true){
            isAngry=false;
            deleteAnger();
        }
    }*/


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
