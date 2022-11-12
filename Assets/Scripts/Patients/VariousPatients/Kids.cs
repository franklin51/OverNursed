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

    override protected bool Waiting4FirstMission() //生兵後等待第一個任務，return true表示等不及了，進入Inpatience函式
    {
        return false;
    }

    override protected bool ExecuteMission() // 執行任務，return true表示成功執行
    {
        return Pass_Mission();
    }

    override protected bool Waiting() // 任務完成後等待下一個任務，return true表示等不及了，進入Inpatience函式
    {
        return false;
    }

    override protected void Inpatience() // 等不及開始搞事
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
