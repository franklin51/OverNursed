using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Lineup : MonoBehaviour
{
    public int length = 5;
    public int count = 0;
    public List<GameObject> line = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string NewPatientEnter(GameObject patient)
    {
        line.Add(patient);
        return (count++).ToString();
    }

    public void RemoveAPatient(int ID)
    {
        count--;
        int idx = 0;
        for (idx = 0; idx < line.Count; idx++)
            if (line[idx].GetComponent<PatientBaseClass>().ID == ID)
                break;
        line.RemoveAt(idx);

        for (int i = idx; i < line.Count; i++)
        {
            line[i].GetComponent<PatientBaseClass>().LineupPosition = "排隊點" + i.ToString();
            line[i].GetComponent<PatientBaseClass>().NavigateTo(GameObject.Find("排隊點"+i.ToString()));
        }
    }
}
