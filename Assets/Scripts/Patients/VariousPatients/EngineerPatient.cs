using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EngineerPatient : PatientBaseClass
{
	public bool isAngry = false;
	public bool has_sleep = false;
	public float SleepTimer = 20;

	private GameObject target;

	override protected bool Waiting4FirstMission() // 生兵後等待第一個任務，return true表示等不及了，進入Inpatience函式
	{
		return false;
	}

	override protected bool ExecuteMission() // 執行任務，return true表示成功執行
	{
		has_sleep = false;
		return true;
	}

	override protected bool Waiting() // 任務完成後等待下一個任務，return true表示等不及了，進入Inpatience函式
	{
		if (!has_sleep)
		{
			transform.eulerAngles = new Vector3(90, 0, 0);
			allow_picked = false;
			Invoke("Sleeping", 10f);
			has_sleep = true;
		}

		return false;
	}

	override protected void Inpatience() // 等不及開始搞事
	{

	}

	private void Sleeping()
	{
		transform.eulerAngles = new Vector3(0, 0, 0);
		allow_picked = true;
	}
}
