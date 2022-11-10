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

    override protected void Inpatience()
    {
        Debug.Log("Angry-Grandma INPATIENT");

    }
}
