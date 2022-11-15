using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TerroristPatient : PatientBaseClass
{
    [SerializeField] GameObject Anger;

	public bool isAngry = false;
	private GameObject target;

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
		Invoke("createAnger", 4.0f); //?箔?閫ψace condition?見撖恬??末銋蝚血???!?
		if (isAngry == true && !allow_picked)
		{
			isAngry = false;
			deleteAnger();
		}
		return false;
	}

	override protected void Inpatience() // 等不及開始搞事
	{
		deleteAnger();
		agent.enabled = true;
		agent.speed = 40f;
		agent.acceleration = 1000f;
		if (!is_attacking)
			do
			{
				target = GameObject.FindGameObjectsWithTag("patient")[Random.Range(0, GameObject.FindGameObjectsWithTag("patient").Length)];
			} while (target.transform.name == "8+9(Clone)");

		NavigateTo(target);
		is_attacking = true;
	}

	void deleteAnger()
	{
		Destroy(gameObject.transform.Find("Canvas").transform.Find("Anger(Clone)").gameObject);
	}

	void createAnger()
	{
		if (isAngry == false && allow_picked && doingMission == false)
		{
			isAngry = true;
			GameObject anger = Instantiate(Anger, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
			anger.transform.SetParent(transform.GetChild(1), false);
			anger.GetComponent<Anger>().lookAt = transform;
		}
	}
}
