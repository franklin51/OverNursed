using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TerroristPatient : PatientBaseClass
{
    [SerializeField] GameObject Anger;
	
	private GameObject target;
	private NavMeshAgent agent;

	void Start()
	{
		GP = GameObject.Find("生兵點").GetComponent<GeneratePoint>();
		target = GameObject.Find("1P");
		MM = GameObject.Find("MissionManager").GetComponent<MissionManager>();
		//Dialog.SetActive(false);

		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

		// 記得關掉才能舉起來
		agent.enabled = false;
		//agent.updatePosition = false;
		//agent.updateRotation = false;
	}

	override protected bool Waiting4FirstMission() // 生兵後等待第一個任務，return true表示等不及了，進入Inpatience函式
    {
		//Debug.Log("Wait 4 First");
		return false;
    }

	override protected bool ExecuteMission() // 執行任務，return true表示成功執行
	{
		//Debug.Log("Executing");
		return true;
    }

	override protected bool Waiting() // 任務完成後等待下一個任務，return true表示等不及了，進入Inpatience函式
	{
		//Debug.Log("Waiting");
		return false;
	}

	override protected void Inpatience() // 等不及開始搞事
	{
		//Debug.Log("Inpatience");
	}
}
