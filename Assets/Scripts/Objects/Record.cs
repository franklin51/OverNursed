using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Record : MonoBehaviour
{
    private Animator animator;
    public Image oldImage;
    public Image recordImage;
    public Image task0Image;
    public Image task1Image;
    public Image task2Image;
    public Image task3Image;

    public Sprite AngryGrandma;
    public Sprite Kids;
    public Sprite TerroristPatient;
    public Sprite EngineerPatient;
    public Sprite Patient;
    public Sprite OldMan;

    public Sprite blood;
    public Sprite urine;
    public Sprite ECG;
    public Sprite height;
    public Sprite visual;

    public Sprite twoTask;
    public Sprite threeTask;
    public Sprite fourTask;
    public int ID;

    void Start(){
        oldImage=gameObject.GetComponentInChildren<Image>();
        recordImage = gameObject.GetComponent<Image>();
    }

    public void setInitialValue(int ID, string patientType, string[] tasksType){
        this.ID=ID;
        changePatientSprite(patientType);
        int tasksCount=tasksType.Length;
        changeRecordSprite(tasksCount);
        for(int i=0; i<tasksCount; i++){
            changeTaskSprite(i, tasksType[i]);
        }


    }

    public void changeRecordSprite(int tasksCount){
        Vector3 pos = transform.position;
        task0Image.gameObject.SetActive(false);
        task1Image.gameObject.SetActive(false);
        task2Image.gameObject.SetActive(false);
        task3Image.gameObject.SetActive(false);
        if(tasksCount==2){
            recordImage.sprite = twoTask;
            task0Image.gameObject.SetActive(true);
            task1Image.gameObject.SetActive(true);

            task0Image.transform.position = pos + new Vector3(-33.7f, -53.6f, 0);
            task1Image.transform.position = pos + new Vector3(33.4f, -53.6f, 0);
        }
        if(tasksCount==3){
            recordImage.sprite = threeTask;
            task0Image.gameObject.SetActive(true);
            task1Image.gameObject.SetActive(true);
            task2Image.gameObject.SetActive(true);

            task0Image.transform.position = pos + new Vector3(-30f, 33.6f, 0);
            task1Image.transform.position = pos + new Vector3(-30f, -53.6f, 0);
            task2Image.transform.position = pos + new Vector3(33.4f, -53.6f, 0);
        }
        if(tasksCount==4){
            recordImage.sprite = fourTask;
            task0Image.gameObject.SetActive(true);
            task1Image.gameObject.SetActive(true);
            task2Image.gameObject.SetActive(true);
            task3Image.gameObject.SetActive(true);

            task0Image.transform.position = pos + new Vector3(-29f, 65.8f, 0);
            task1Image.transform.position = pos + new Vector3(-29f, 0.3f, 0);
            task2Image.transform.position = pos + new Vector3(-29f, -65.7f, 0);
            task3Image.transform.position = pos + new Vector3(33.4f, -65.7f, 0);
        }
    }
    
    public void changePatientSprite(string name){
        

        if(name=="AngryGrandma"){
            oldImage.sprite = AngryGrandma;
        }
        else if(name=="Kids"){
            oldImage.sprite = Kids;
        }
        else if(name=="TerroristPatient"){
            oldImage.sprite = TerroristPatient;
        }
        else if(name=="EngineerPatient"){
            oldImage.sprite = EngineerPatient;
        }
        else if(name=="Patient"){
            oldImage.sprite = Patient;
        }
        else if(name=="OldMan"){
            oldImage.sprite = OldMan;
        }
        else{
            Debug.Log("changeSprite name error");
        }
    }

    public void changeTaskSprite(int index, string taskName){
        Image img;
        if(index==0) img=task0Image;
        else if(index==1) img=task1Image;
        else if(index==2) img=task2Image;
        else  img=task3Image;
        //else Debug.Log("changeTaskSprite index wrong");

        if(taskName=="blood"){
            img.sprite = blood;
        }
        else if(taskName=="urine"){
            img.sprite = urine;
        }
        else if(taskName=="ECG"){
            img.sprite = ECG;
        }
        else if(taskName=="height"){
            img.sprite = height;
        }
        else if(taskName=="visual"){
            img.sprite = visual;
        }
        else{
            Debug.Log("Task Sprite name wrong");
        }

    }

    public void setComplete(int num){
        if(num==0) task0Image.transform.GetChild(0).gameObject.SetActive(true);
            else if(num==1) task1Image.transform.GetChild(0).gameObject.SetActive(true);
            else if(num==2) task2Image.transform.GetChild(0).gameObject.SetActive(true);
            else if(num==3) task3Image.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void completeAnimation(int num){
        try
        {
            
            string s = "check"+num.ToString();
            Debug.Log(s);
            animator = GetComponent<Animator>();
            animator.Play (s);
            if(num==0) task0Image.transform.GetChild(0).gameObject.SetActive(true);
            else if(num==1) task1Image.transform.GetChild(0).gameObject.SetActive(true);
            else if(num==2) task2Image.transform.GetChild(0).gameObject.SetActive(true);
            else if(num==3) task3Image.transform.GetChild(0).gameObject.SetActive(true);
        }
        catch (System.Exception)
        {

        }  

    }

    public void failedAnimation(int num){
        try
        {
            string s ="failed"+num.ToString();
            animator = GetComponent<Animator>();
            animator.Play (s);
        }
        catch (System.Exception)
        {

        } 
        
    }

    public void pickAnimation(){
        // Vector3 objectScale = transform.localScale;
        // transform.localScale = new Vector3(2,  2, objectScale.z);
        try
        {

            animator = GetComponent<Animator>();
            animator.Play ("pick");
            
        }
        catch (System.Exception)
        {

        }
        
    }
    public void emptyAnimation(){
        try{
            animator = GetComponent<Animator>();
            animator.Play ("empty");
        }
        catch(System.Exception)
        {

        }
    }
}
