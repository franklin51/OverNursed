using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{

    // [SerializeField] float moveSpeed=0.1f;
    // Start is called before the first frame update
    public int num;
    public void setNum(int x){
        // this.num=x;
        Debug.Log("hi");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKey(KeyCode.RightArrow)){
        //     transform.Translate(moveSpeed, 0, 0);
        //     Debug.Log("right");
        // }
        // if(Input.GetKey(KeyCode.LeftArrow)){
        //     transform.Translate(-1*moveSpeed, 0, 0);
        //     Debug.Log("Left");
        // }
    }
}
