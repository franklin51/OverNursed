using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePoint : MonoBehaviour
{
    public GameObject patient;
    public PatientBaseClass myPatients;

    public List<PatientBaseClass> patient_list;

    // Start is called before the first frame update
    void Start()
    {
        // template
        /*patient = GameObject.Find("病人");
        myPatients = patient.GetComponent<PatientBaseClass>();*/

        //GeneratePatient();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 生兵
    public void GeneratePatient()
    {
        GameObject temp = Instantiate(patient, transform.position, transform.rotation);
        patient_list.Add(temp.GetComponent<PatientBaseClass>());
    }
}
