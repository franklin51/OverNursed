using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskBar : MonoBehaviour
{
    private Animator animator;
    public Image oldImage;
    public Sprite AngryGrandma;
    public Sprite Kids;
    public Sprite TerroristPatient;
    public Sprite EngineerPatient;
    public Sprite Patient;

    void Start(){
        oldImage=gameObject.GetComponent<Image>();
    }

    public void completeAnimation(){
        try
        {
            animator = GetComponent<Animator>();
            animator.Play ("missionComplete");
        }
        catch (System.Exception)
        {

        }  

    }

    public void failedAnimation(){
        try
        {
            animator = GetComponent<Animator>();
            animator.Play ("missionFailed");
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
        // animator = GetComponent<Animator>();
        //     animator.Play ("empty");
    }

    public void changeSprite(string name){
        

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
}
