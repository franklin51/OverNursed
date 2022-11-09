using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryGrandma : PatientBaseClass
{
    [SerializeField] GameObject Anger;
    public bool isAngry=false;

    public override void Start(){
        base.Start();

    }
    public override void Update(){
        base.Update();

        if(isAngry==false && is_picked==false &&doingMission==false){//doingMission會有race condition
            Invoke("createAnger",4.0f); //為了解race condition才這樣寫，剛好也蠻符合情況!?
        }
        if(isAngry==true && is_picked==true){
            isAngry=false;
            deleteAnger();
        }


    }
    public void timeOver(){
        MM.deleteMission(ID);
        Destroy(gameObject);
    }

    void deleteAnger(){
        Debug.Log("deleteAnger");
        Destroy(gameObject.transform.Find("Canvas").transform.Find("Anger(Clone)").gameObject);
    }
    void createAnger(){
        if(isAngry==false && is_picked==false &&doingMission==false){
            isAngry=true;
            GameObject anger = Instantiate(Anger, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            anger.transform.SetParent (transform.GetChild(1), false);
            anger.GetComponent<Anger>().lookAt=transform;
        }
       
    }
    
    
}
