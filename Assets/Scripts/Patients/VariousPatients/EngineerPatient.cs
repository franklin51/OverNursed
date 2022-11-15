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

	override protected bool Waiting4FirstMission() // �ͧL�ᵥ�ݲĤ@�ӥ��ȡAreturn true��ܵ����ΤF�A�i�JInpatience�禡
	{
		return false;
	}

	override protected bool ExecuteMission() // ������ȡAreturn true��ܦ��\����
	{
		has_sleep = false;
		return true;
	}

	override protected bool Waiting() // ���ȧ����ᵥ�ݤU�@�ӥ��ȡAreturn true��ܵ����ΤF�A�i�JInpatience�禡
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

	override protected void Inpatience() // �����ζ}�l�d��
	{

	}

	private void Sleeping()
	{
		transform.eulerAngles = new Vector3(0, 0, 0);
		allow_picked = true;
	}
}
