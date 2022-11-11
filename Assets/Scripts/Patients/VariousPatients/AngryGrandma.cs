<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryGrandma : PatientBaseClass
{
    [SerializeField] GameObject Anger;

	// protected override void startCall()
	// {

	//     Debug.Log("hihihi");
	// }

	override protected bool Waiting4FirstMission() // 生兵後等待第一個任務，return true表示等不及了，進入Inpatience函式
	{
		return false;
	}

	override protected bool ExecuteMission() // 執行任務，return true表示成功執行
	{
		return true;
	}

	override protected bool Waiting() // 任務完成後等待下一個任務，return true表示等不及了，進入Inpatience函式
	{
		return false;
	}

	override protected void Inpatience() // 等不及開始搞事
	{

	}
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryGrandma : PatientBaseClass
{
    [SerializeField] GameObject Anger;
    public bool isAngry=false;

    public override void Start(){
        base.Start();

    }
    public override void Update(){
        base.Update();

        if(isAngry==false && is_picked==false &&doingMission==false){//doingMission������race condition
            Invoke("createAnger",4.0f); //��箔��閫ψace condition������璅�撖恬�����憟賭����餌泵������瘜�!?
        }
        if(isAngry==true && is_picked==true){
            isAngry=false;
            deleteAnger();
        }


    }
    public void timeOver(){
        MM.deleteMission(ID);
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
>>>>>>> refs/remotes/origin/main
