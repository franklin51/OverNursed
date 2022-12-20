using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TerroristPatient : PatientBaseClass
{
    [SerializeField] GameObject Anger;

	public bool isAngry = false;
	private GameObject target;
	float Timer=4.0f;

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
		//Invoke("createAnger", 4.0f); //?箔?閫ψace condition?見撖恬??末銋蝚血???!?
		if(allow_picked){
            Timer-=Time.deltaTime;
        }
        if(Timer<0){
            createAnger();
        }
		if (isAngry == true && !allow_picked)
		{
			Timer=4.0f;
			isAngry = false;
			deleteAnger();
		}
		return false;
	}

	override protected void Inpatience() // 等不及開始搞事
	{
		state = 10;
		agent.enabled = true;
		agent.speed = 40f;
		agent.acceleration = 1000f;
		if (!is_attacking)
		{
			deleteAnger();
			target = GameObject.FindGameObjectsWithTag("Player")[Random.Range(0, GameObject.FindGameObjectsWithTag("Player").Length)];
			/*do
			{
				target = GameObject.FindGameObjectsWithTag("patient")[Random.Range(0, GameObject.FindGameObjectsWithTag("patient").Length)];
			} while (target.transform.name == "8+9(Clone)");*/
		}

		NavigateTo(target);
		is_attacking = true;
	}

	void deleteAnger()
	{
		if (gameObject.transform.Find("Canvas").transform.Find("Anger(Clone)"))
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

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.tag == "Player")
		{
			if (collision.transform.name == "1P") lastPlayer = 1;
			else if (collision.transform.name == "2P") lastPlayer = 2;

			if (is_attacking && target.transform.name == collision.transform.name)
            {
				SE.PlaySoundEffect("terrorist attack");
				MM.rob(target.transform.name, 150);
				Destroy(gameObject);
            }
		}

		/*if (collision.transform.tag == "patient")
        {
            if (is_attacking)
            {
                MM.deleteMission(collision.gameObject.GetComponent<PatientBaseClass>().ID);
                //MM.deleteMission(ID);
                lineup.RemoveAPatient(collision.gameObject.GetComponent<PatientBaseClass>().ID);
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }*/
	}
}
