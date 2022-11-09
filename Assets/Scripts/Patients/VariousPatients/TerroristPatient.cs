using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TerroristPatient : PatientBaseClass
{
    [SerializeField] GameObject Anger;
	
	private GameObject target;
	private NavMeshAgent agent;

    int i = 0;
	
	void Start()
	{
		GP = GameObject.Find("生兵點").GetComponent<GeneratePoint>();
		target = GameObject.Find("1P");
		MM = GameObject.Find("MissionManager").GetComponent<MissionManager>();
		updateDialogString();
		//Dialog.SetActive(false);
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	
	void Update()
	{
		agent.SetDestination(target.transform.position);
	}
	
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "task" && is_picked == false)
        {
			agent.SetDestination(GP.transform.position);
        }
	}
}
