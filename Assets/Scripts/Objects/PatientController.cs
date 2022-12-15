using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientController : MonoBehaviour
{
    string[] doors = new string[] { "Door urine", "Door height", "Door visual", "Door ECG", "Door blood" };
    Dictionary<string, int> doors_dict = new Dictionary<string, int>();
    Dictionary<string, int> doors_limit_dict = new Dictionary<string, int>(); // 任務同時進行人數上線
    int[] occupied_num;

    // Start is called before the first frame update
    void Start()
    {
        occupied_num = new int[doors.Length];
        for (int i = 0; i < doors.Length; i++)
            doors_dict.Add(doors[i], i);

        for (int i = 0; i < doors.Length; i++)
            doors_limit_dict.Add(doors[i], 1);
        doors_limit_dict["Door blood"] = 2;
        doors_limit_dict["Door visual"] = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool attempt_do_task(string task_name)
    {
        string door_name = "Door " + task_name;
        if (occupied_num[doors_dict[door_name]] == doors_limit_dict[door_name])
            return false;

        occupied_num[doors_dict[door_name]]++;
        if (!can_door_open(door_name))
            transform.Find(door_name).Find("Open1").GetComponent<IndoorSensor>().close();
        return true;
    }

    public void finish_task(string task_name)
    {
        string door_name = "Door " + task_name;
        occupied_num[doors_dict[door_name]]--;
        transform.Find(door_name).Find("Open1").GetComponent<IndoorSensor>().open();
    }

    public bool can_door_open (string door_name)
    {
        /*if (door_name == "Door ECG" && (occupied_num[doors_dict[door_name]] == 1 || occupied_num[doors_dict["Door blood"]] == 1))
            return false;
        else if (door_name != "Door ECG" && occupied_num[doors_dict[door_name]] == 1)
            return false;*/
        if (occupied_num[doors_dict[door_name]] == doors_limit_dict[door_name])
            return false;
        return true;
    }
}
