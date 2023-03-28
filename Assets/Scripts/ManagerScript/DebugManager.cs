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
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SceneReset();
        }
    }
    public void SceneReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Clear()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            GameManager.Instance.GameClear();
        }
    }
 
    public void DaM()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            GameManager.Instance.Damage(10);
            GameManager.Instance.state_damage = false;
        }
    }

}
