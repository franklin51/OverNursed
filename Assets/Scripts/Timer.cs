using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Text persentTxt;
    [SerializeField] Image persentShow;
    [SerializeField] public Transform lookAt;
    [SerializeField] public Vector3 offset;
    private Camera cam;
    int WaitingTime = 400;

    int i =0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(timer());
        cam = Camera.main;
    }

    public void reset(){
        i=0;
        StartCoroutine(timer());
    }

    void Update()
    {
        Vector3 pos = cam.WorldToScreenPoint(lookAt.position+offset);
        if(transform.position !=pos){
            transform.position=pos;
        }
    }

    IEnumerator timer(){
        while(i<WaitingTime){
            i+=1;
            int j=i/12;
            persentTxt.text = j.ToString();
            persentShow.fillAmount = i/(float)WaitingTime;
            yield return new WaitForFixedUpdate();
        }
        
        transform.parent.gameObject.transform.parent.gameObject.transform.GetComponent<PatientBaseClass>().timerEnd();
        Destroy(gameObject);
    }
}
