using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    int chair_num = 6;
    public int[] chair; // -1: 椅子沒人; else: ID;
    Queue<PatientBaseClass> queue = new Queue<PatientBaseClass>(); // 排隊嘍
    Queue<int> lastplayer_queue = new Queue<int>(); // 排隊嘍
    Queue<Vector3> chair_queue = new Queue<Vector3>(); // 排隊嘍

    PatientBaseClass cur_patient;
    SoundEffects SE;
    MissionManager MM;

    Vector3 cur_patient_chair_pos;
    bool has_taken_files;
    int ID_turn = -1;
    int prev_ID_turn = -2;
    int lastplayer_turn = 100;
    float timer = 3;
    bool is_occupied = false;
    bool is_leaving = false;

    // Start is called before the first frame update
    void Start()
    {
        SE = GameObject.Find("Main Camera").GetComponent<SoundEffects>();
        MM = GameObject.Find("MissionManager").GetComponent<MissionManager>();

        chair = new int[chair_num];
        for (int i = 0; i < chair_num; i++)
            chair[i] = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (is_occupied)
        {
            //Debug.Log(timer);
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                if (cur_patient)
                {
                    cur_patient.NavigateTo(cur_patient_chair_pos);
                    if (cur_patient.GetComponent<PatientBaseClass>().state == 1)
                        cur_patient.GetComponent<PatientBaseClass>().state = 2;
                    else if (cur_patient.GetComponent<PatientBaseClass>().state == 4)
                        cur_patient.GetComponent<PatientBaseClass>().state = 5;
                }
                is_occupied = false;
                is_leaving = true;
            }
        }
        else if (ID_turn == -1 || timer < 0)
            next_turn();
    }

    public void enqueue(PatientBaseClass patient, string chair_name, int lastplayer)
    {
        int chair_idx = int.Parse(chair_name.Replace("Chair_", ""));
        if (chair[chair_idx] != -1)
            return;

        chair[chair_idx] = patient.GetComponent<PatientBaseClass>().ID;
        queue.Enqueue(patient);
        lastplayer_queue.Enqueue(lastplayer);
        chair_queue.Enqueue(GameObject.Find(chair_name).transform.position);
    }

    private void next_turn ()
    {
        if (queue.Count == 0)
            return;

        timer = 3;
        PatientBaseClass patient = queue.Dequeue();
        lastplayer_turn = lastplayer_queue.Dequeue();
        Vector3 chair_pos = chair_queue.Dequeue();
        has_taken_files = false;
        prev_ID_turn = ID_turn;
        ID_turn = patient.ID;

        patient.NavigateTo(gameObject);
        cur_patient = patient;
        cur_patient_chair_pos = chair_pos;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.root.transform.tag == "patient" && collision.transform.root.gameObject.GetComponent<PatientBaseClass>().ID == ID_turn && !has_taken_files)
        {
            SE.PlaySoundEffect("counter");
            has_taken_files = true;

            if (lastplayer_turn != -1)
                MM.setOwner(ID_turn, lastplayer_turn);
            //else
            //    MM.deleteMission_counter(ID_turn);
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.transform.root.transform.tag == "patient" && collision.transform.root.gameObject.GetComponent<PatientBaseClass>().ID == ID_turn)
        {
            float dist2D = Vector3.Distance(new Vector3(collision.transform.position.x, 0, collision.transform.position.z), new Vector3(transform.position.x, 0, transform.position.z));
            if (!is_leaving && dist2D < 0.8)
            {
                cur_patient = collision.transform.root.gameObject.GetComponent<PatientBaseClass>();
                cur_patient.agent.enabled = false;
                is_occupied = true;
            }
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.transform.root.transform.tag == "patient" && (collision.transform.root.gameObject.GetComponent<PatientBaseClass>().ID == ID_turn || collision.transform.root.gameObject.GetComponent<PatientBaseClass>().ID == prev_ID_turn))
        {
            float dist2D = Vector3.Distance(new Vector3(collision.transform.position.x, 0, collision.transform.position.z), new Vector3(transform.position.x, 0, transform.position.z));
            if (is_leaving)
            {
                is_leaving = false;
            }
        }
    }
}
