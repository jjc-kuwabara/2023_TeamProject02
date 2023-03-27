using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    bool pause;

    void Start()
    {
        
    }

    void Update()
    {
        if (pause)
        {
            StartGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            //ˆê’â~
            Time.timeScale = 0;
            pause = true;
            Debug.Log("ˆê’â~");
        }
    }
    public void StartGame()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //ÄŠJ
            Time.timeScale = 1;
            pause = false;
            Debug.Log("ÄŠJ");
        }
    }
    
}
