using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameStartScene : MonoBehaviour
{
    string current_state;
    string menu, start;
    public GameObject MenuText;
    public GameObject StartText;
    // Start is called before the first frame update
    void Start()
    {
        current_state = "start";
        start = "start";
        menu = "menu";
        MenuText.SetActive(false);
        StartText.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(current_state == menu){
            if(Input.GetKeyDown(KeyCode.T)){
                SceneManager.LoadScene("Map_Scene", LoadSceneMode.Single);
            }
            if(Input.GetKeyDown(KeyCode.M)){
                SceneManager.LoadScene("Instructions1", LoadSceneMode.Single);
            }
        }
        else if(current_state == start)
        {
            if (Input.anyKey)
            {
                Debug.Log("A key or mouse click has been detected");
                Debug.Log("Start Game");
                current_state = menu;
                MenuText.SetActive(true);
                StartText.SetActive(false);
            }
        }
        
    }
}
