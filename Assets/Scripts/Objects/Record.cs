using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Record : MonoBehaviour
{
    private Animator animator;
    public Image oldImage;
    public Image task0Image;
    public Image task1Image;
    public Sprite AngryGrandma;
    public Sprite Kids;
    public Sprite TerroristPatient;
    public Sprite EngineerPatient;
    public Sprite Patient;
    public Sprite blood;
    public Sprite urine;
    public Sprite ECG;
    public Sprite height;
    public Sprite visual;
    public int ID;

    void Start(){
        oldImage=gameObject.GetComponentInChildren<Image>();
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
        else{
            Debug.Log("changeSprite name error");
        }
    }

    public void changeTaskSprite(int index, string taskName){
        Image img;
        if(index==0) img=task0Image;
        else  img=task1Image;
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

    public void completeAnimation(int num){
        try
        {
            
            string s = "check"+num.ToString();
            Debug.Log(s);
            animator = GetComponent<Animator>();
            animator.Play (s);
            if(num==0) task0Image.transform.GetChild(0).gameObject.SetActive(true);
            else if(num==1) task1Image.transform.GetChild(0).gameObject.SetActive(true);
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
