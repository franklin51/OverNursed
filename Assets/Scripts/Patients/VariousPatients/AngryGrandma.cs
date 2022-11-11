using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryGrandma : PatientBaseClass
{
    [SerializeField] GameObject Anger;
    public bool isAngry=false;

	override protected bool Waiting4FirstMission() // ¥Í§L«áµ¥«İ²Ä¤@­Ó¥ô°È¡Areturn trueªí¥Üµ¥¤£¤Î¤F¡A¶i¤JInpatience¨ç¦¡
	{
		return false;
	}

	override protected bool ExecuteMission() // °õ¦æ¥ô°È¡Areturn trueªí¥Ü¦¨¥\°õ¦æ
	{
		return true;
	}

	override protected bool Waiting() // ¥ô°È§¹¦¨«áµ¥«İ¤U¤@­Ó¥ô°È¡Areturn trueªí¥Üµ¥¤£¤Î¤F¡A¶i¤JInpatience¨ç¦¡
    {
        Invoke("createAnger", 4.0f); //?ºä?è§£race condition?é€™æ¨£å¯«ï??›å¥½ä¹Ÿè »ç¬¦å??…æ?!?
        Debug.Log(is_picked == true);
        if (isAngry == true && is_picked == true)
        {
            isAngry = false;
            deleteAnger();
        }
        return false;
    }

	override protected void Inpatience() // µ¥¤£¤Î¶}©l·d¨Æ
    {

    }

    /*public override void Update(){
        base.Update();

        if(isAngry==false && is_picked==false &&doingMission==false){//doingMission?ƒæ?race condition
            Invoke("createAnger",4.0f); //?ºä?è§£race condition?é€™æ¨£å¯«ï??›å¥½ä¹Ÿè »ç¬¦å??…æ?!?
        }
        if(isAngry==true && is_picked==true){
            isAngry=false;
            deleteAnger();
        }
    }*/

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
