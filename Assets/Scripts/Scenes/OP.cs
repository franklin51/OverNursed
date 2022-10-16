using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class OP : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)){
			SceneManager.LoadScene("Map_Scene", LoadSceneMode.Single);
		}
        if(Input.GetKeyDown(KeyCode.M)){
			SceneManager.LoadScene("Instructions1", LoadSceneMode.Single);
		}
    }
}
