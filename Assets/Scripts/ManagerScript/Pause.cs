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
            //�ꎞ��~
            Time.timeScale = 0;
            pause = true;
            Debug.Log("�ꎞ��~");
        }
    }
    public void StartGame()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //�ĊJ
            Time.timeScale = 1;
            pause = false;
            Debug.Log("�ĊJ");
        }
    }
    
}
