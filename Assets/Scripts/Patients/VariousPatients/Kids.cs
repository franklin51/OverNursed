using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kids : PatientBaseClass
{
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
    public int Max_repeat_time = 5;
    private int Already_repeat = 0;
    private float Probability_Complete = 0.5f;//the probabilty of passing the mission

    override protected bool Waiting4FirstMission() // �ͧL�ᵥ�ݲĤ@�ӥ��ȡAreturn true��ܵ����ΤF�A�i�JInpatience�禡
    {
        return false;
    }

    override protected bool ExecuteMission() // ������ȡAreturn true��ܦ��\����
    {
        return true;
    }

    override protected bool Waiting() // ���ȧ����ᵥ�ݤU�@�ӥ��ȡAreturn true��ܵ����ΤF�A�i�JInpatience�禡
    {
        return false;
    }

    override protected void Inpatience() // �����ζ}�l�d��
    {

    }

    public override void Start()
    {
        base.Start();

    }

    bool Pass_Mission(){ // determine if the mission need to be done again
        int temp_num = Random.Range(1, 11);
        if(temp_num <= 10*Probability_Complete){
            return true;
        }
        // less than 
        Already_repeat+=1;
        if(Already_repeat > Max_repeat_time)
            return true;
        return false;
    } 
    
    // if(mission finish){
    //     if(Pass_Mission()){ // true than complete this mission
    //         // complete mission
    //     }
    //     else{
    //         //do nothing
    //     }
    // }

    

}
