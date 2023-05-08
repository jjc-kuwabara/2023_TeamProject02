using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DebugManager : MonoBehaviour
{
    PlayerControl control;
    void Start()
    {
        control = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }

    void Update()
    {
        Clear();
        DaM();
        FullF();
    }
    public void SceneReset()
    {
        Pause.Instance.StartGame();
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
    public void FullF()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            control.FullFlame();
        }
    }
}
