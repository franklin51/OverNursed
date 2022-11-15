using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour
{
    private Scene scene;
    char instruction_num;
    int next_num;
    private char total_instru = '3';
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        Debug.Log("Scene Name: " + scene.name);

        instruction_num = scene.name[scene.name.Length-1];
        next_num = ((int)Char.GetNumericValue(instruction_num))+1;
        // Debug.Log(next_num);
    }
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        // char b = a.ToString()[0];
        if(Input.GetKeyDown(KeyCode.M)){
			if(instruction_num < total_instru )
                SceneManager.LoadScene("Instructions"+((next_num).ToString()[0]), LoadSceneMode.Single);
            else
			    SceneManager.LoadScene("OP", LoadSceneMode.Single);

		}
    }
}
