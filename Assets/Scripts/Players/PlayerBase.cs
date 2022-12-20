using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 跟操作有關的
public class PlayerBase : MonoBehaviour
{
    public bool is_movable = true;
    public float velocity = 40f;
    public float accelerate_times = 10f;
    public GameObject patient;
    public GeneratePoint GP;
    public Rigidbody rb;
    public MissionManager MM;
    GameObject player;
    Vector3 m_Input = new Vector3(0, 0, 0);

    public NavMeshAgent agent;

    private bool already_pick=false; // 已經撿起病人了嗎
    private bool to_pick = false, in_trigger = false;

    // Start is called before the first frame update
    void Start()
    {
        GP = GameObject.Find("生兵點").GetComponent<GeneratePoint>();
        rb = GetComponent<Rigidbody>();
        MM = GameObject.Find("MissionManager").GetComponent<MissionManager>();

        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        // 分不同角色的操作
        string name = gameObject.name;
        if (is_movable && name == "1P")
        {
            m_Input = new Vector3(0, 0, 0);
            if (Input.GetKey("w"))
                MoveUp();
            else if (Input.GetKey("s"))
                MoveDown();
            else if (Input.GetKey("a"))
                MoveLeft();
            else if (Input.GetKey("d"))
                MoveRight();
            else
                rb.velocity = new Vector3(0, 0, 0);
            if (Input.GetKeyDown("t"))
            {
                if (!already_pick && in_trigger)
                    to_pick = true;
                else
                    to_pick = false;

                if (already_pick && patient && !patient.GetComponent<PatientBaseClass>().allow_picked)
                    PutDownPatient();
            }
        }
        else if (is_movable && name == "2P")
        {
            m_Input = new Vector3(0, 0, 0);
            if (Input.GetKey(KeyCode.UpArrow))
                MoveUp();
            else if (Input.GetKey(KeyCode.DownArrow))
                MoveDown();
            else if (Input.GetKey(KeyCode.LeftArrow))
                MoveLeft();
            else if (Input.GetKey(KeyCode.RightArrow))
                MoveRight();
            else
                rb.velocity = new Vector3(0, 0, 0);
            if (Input.GetKeyDown("m"))
            {
                if (!already_pick && in_trigger)
                    to_pick = true;
                else
                    to_pick = false;

                if (already_pick && patient && !patient.GetComponent<PatientBaseClass>().allow_picked)
                    PutDownPatient();
            }
        }
        if (!is_movable)
            rb.velocity = new Vector3(0, 0, 0);
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.transform.root.transform.tag == "patient")
        {
            in_trigger = true;
            bool isDoing=collision.transform.root.transform.GetComponent<PatientBaseClass>().doingMission;
            /*if (name == "1P" && Input.GetKeyDown("t") || name == "2P" && Input.GetKeyDown("m"))
                to_pick = !already_pick;*/
            
            if (to_pick && !already_pick && !isDoing)
            {
                patient = collision.transform.root.gameObject;

                // 與病人合體
                if (patient.GetComponent<PatientBaseClass>().allow_picked)
                {
                    already_pick = true;
                    patient.GetComponent<PatientBaseClass>().is_picked = true;
                    patient.GetComponent<PatientBaseClass>().allow_picked = false;
                    patient.transform.SetParent(transform, false);
                    patient.transform.localRotation = Quaternion.Euler(0, 180, 0);
                    patient.transform.localScale = new Vector3(patient.transform.localScale.x / transform.localScale.x, patient.transform.localScale.y / transform.localScale.y, patient.transform.localScale.z / transform.localScale.z);
                    
                    patient.transform.localPosition = new Vector3(0, 1f, 0);

                    MM.pickPatient(patient.GetComponent<PatientBaseClass>().ID);//拿起病人時更新病人位子
                    patient.GetComponent<PatientBaseClass>().picked();
                    // if(!patient.GetComponent<PatientBaseClass>().hasOwner){
                    //     string name = gameObject.name;
                    //     if(name=="1P" && !MM.isFull(1)){
                    //         MM.setOwner(patient.GetComponent<PatientBaseClass>().ID,1);
                    //         patient.GetComponent<PatientBaseClass>().hasOwner=true;
                    //     }

                    //     else if(name=="2P" && !MM.isFull(2)){
                    //         MM.setOwner(patient.GetComponent<PatientBaseClass>().ID,2);
                    //         patient.GetComponent<PatientBaseClass>().hasOwner=true;
                    //     }

                    // }

                    Transform check_mark = patient.transform.Find("MissionCompleteCheckMark(Clone)");
                    if (check_mark)
                        Destroy(check_mark);                    

                    // 從隊伍中移除玩家
                    if (patient.GetComponent<PatientBaseClass>().is_waiting4FirstMission && patient.GetComponent<PatientBaseClass>().is_lineup)
                    {
                        patient.GetComponent<PatientBaseClass>().is_lineup = false;
                        patient.GetComponent<PatientBaseClass>().lineup.RemoveAPatient(patient.GetComponent<PatientBaseClass>().ID);
                    }
                }
            }
        }

        if (collision.transform.name == "離開點" && !is_movable)
        {
            MM.ED_scene();
        }
        if (collision.transform.root.transform.tag == "Player" && collision.transform.root.transform.GetComponent<PlayerBase>().is_movable == false)
        {
            in_trigger = true;

            if (to_pick && !already_pick)
            {
                player = collision.transform.root.gameObject;
                player.transform.Find("BaseCharacter").transform.Find("Body").GetComponent<CapsuleCollider>().enabled = false;
                player.GetComponent<PlayerBase>().agent.enabled = true;
                player.GetComponent<PlayerBase>().agent.speed = 20f;
                player.GetComponent<PlayerBase>().agent.SetDestination(GameObject.Find("離開點").transform.position);
            }
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.transform.root.transform.tag == "patient")
            in_trigger = false;
    }


    private void MoveUp()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        /*if (prev_action == 1)
        {
            rb.velocity += new Vector3(0, 0, velocity / accelerate_times);
            rb.velocity = new Vector3(0, 0, Math.Min(velocity, rb.velocity[2]));
        }
        else
            rb.velocity = new Vector3(0, 0, velocity / accelerate_times);*/
        rb.velocity = new Vector3(0, 0, velocity);
    }
    private void MoveDown()
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
        /*if (prev_action == 2)
        {
            rb.velocity += new Vector3(0, 0, -velocity / accelerate_times);
            rb.velocity = new Vector3(0, 0, Math.Max(-velocity, rb.velocity[2]));
        }
        else
            rb.velocity = new Vector3(0, 0, -velocity / accelerate_times);*/
        rb.velocity = new Vector3(0, 0, -velocity);
    }
    private void MoveLeft()
    {
        transform.eulerAngles = new Vector3(0, 270, 0);
        /*if (prev_action == 3)
        {
            rb.velocity += new Vector3(- velocity / accelerate_times, 0, 0);
            rb.velocity = new Vector3(Math.Max(-velocity, rb.velocity[0]), 0, 0);
        }
        else
            rb.velocity = new Vector3(-velocity / accelerate_times, 0, 0);*/
        rb.velocity = new Vector3(-velocity, 0, 0);
    }
    private void MoveRight()
    {
        transform.eulerAngles = new Vector3(0, 90, 0);
        /*if (prev_action == 4)
        {
            rb.velocity += new Vector3(velocity / accelerate_times, 0, 0);
            rb.velocity = new Vector3(Math.Min(velocity, rb.velocity[0]), 0, 0);
        }
        else
            rb.velocity = new Vector3(velocity / accelerate_times, 0, 0);*/
        rb.velocity = new Vector3(velocity, 0, 0);
    }
    private void PutDownPatient()
    {
        already_pick = false;
        patient.GetComponent<PatientBaseClass>().allow_picked = true;
        //patient.GetComponent<PatientBaseClass>().lastPlayer = 1;
        patient.transform.parent = null;

        MM.putDownPatient(patient.GetComponent<PatientBaseClass>().ID);//放下病人時更新病人位子
        patient.GetComponent<PatientBaseClass>().is_picked = false;

        Vector3 temp = new Vector3(patient.transform.position.x, 0.03f, patient.transform.position.z) + transform.forward * 2f;
        for (float f = 2f; f>=0; f-=0.1f)
        {
            temp = new Vector3(patient.transform.position.x, 0.03f, patient.transform.position.z) + transform.forward * f;
            var hitColliders = Physics.OverlapSphere(temp, 0.7f);//2 is purely chosen arbitrarly

            bool flag = true;
            for (int i=0; i<hitColliders.Length; i++)
            {
                if (hitColliders[i].transform.tag == "Wall")
                {
                    flag = false;
                }
            }
            if (flag)
                break;
        }
        patient.transform.position = temp;
        patient = null;
    }
}
