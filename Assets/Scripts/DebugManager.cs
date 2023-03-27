using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugManager : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            SceneReset();
        }
    }
    public void SceneReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
