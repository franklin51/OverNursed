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

	override protected bool Waiting4FirstMission() // �ͧL�ᵥ�ݲĤ@�ӥ��ȡAreturn true��ܵ����ΤF�A�i�JInpatience�禡
	{
		return false;
	}

	override protected bool ExecuteMission() // ������ȡAreturn true��ܦ��\����
	{
		return true;
	}

	override protected bool Waiting() // ���ȧ����ᵥ�ݤU�@�ӥ��ȡAreturn true��ܵ����ΤF�A�i�JInpatience�禡
	{
		return false;
	}

	override protected void Inpatience() // �����ζ}�l�d��
	{

	}
}
