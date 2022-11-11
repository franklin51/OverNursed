<<<<<<< HEAD
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

	override protected bool Waiting4FirstMission() // ¥Í§L«áµ¥«Ý²Ä¤@­Ó¥ô°È¡Areturn trueªí¥Üµ¥¤£¤Î¤F¡A¶i¤JInpatience¨ç¦¡
	{
		return false;
	}

	override protected bool ExecuteMission() // °õ¦æ¥ô°È¡Areturn trueªí¥Ü¦¨¥\°õ¦æ
	{
		return true;
	}

	override protected bool Waiting() // ¥ô°È§¹¦¨«áµ¥«Ý¤U¤@­Ó¥ô°È¡Areturn trueªí¥Üµ¥¤£¤Î¤F¡A¶i¤JInpatience¨ç¦¡
	{
		return false;
	}

	override protected void Inpatience() // µ¥¤£¤Î¶}©l·d¨Æ
	{

	}
}
=======
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

        if(isAngry==false && is_picked==false &&doingMission==false){//doingMissionæœƒæœ‰race condition
            Invoke("createAnger",4.0f); //ç‚ºäº†è§£race conditionæ‰é€™æ¨£å¯«ï¼Œå‰›å¥½ä¹Ÿè »ç¬¦åˆæƒ…æ³!?
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
>>>>>>> refs/remotes/origin/main
