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
