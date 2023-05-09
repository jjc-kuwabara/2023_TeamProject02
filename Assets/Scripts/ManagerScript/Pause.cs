using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pause : Singleton<Pause>
{
    [Header("キャンバス")]
    public GameObject[] canvas;

    [Header("ポーズメニューのカーソル初期位置")]
    [SerializeField] GameObject focusPauseMenu;

    GameObject currentFocus;    //現在
    GameObject previousFocus;   //前フレーム

    bool pause;

    void Start()
    {
        CanvasInit();
        canvas[0].SetActive(true);
    }

    void Update()
    {
        if (!pause)
        {
            PauseGame();
        }
        FocusCheck();
    }

    public void PauseGame()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            //一時停止
            Time.timeScale = 0;
            pause = true;
            Debug.Log("一時停止");
            canvas[1].SetActive(true);
            canvas[0].SetActive(false);
            EventSystem.current.SetSelectedGameObject(focusPauseMenu);
        }
    }
    public void StartGame()
    {
       //再開
       Time.timeScale = 1;
       pause = false;
       Debug.Log("再開");
       canvas[0].SetActive(true);
       canvas[1].SetActive(false);
    }

    public void CanvasInit()
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].SetActive(false);
        }
    }

    void FocusCheck()
    {
        //現在のフォーカスを格納
        currentFocus = EventSystem.current.currentSelectedGameObject;

        //もし前回までのフォーカスと同じなら即終了
        if (currentFocus == previousFocus) return;

        //もしフォーカスが外れていたら
        //前フレームのフォーカスに戻す
        if (currentFocus == null)
        {
            EventSystem.current.SetSelectedGameObject(previousFocus);
            return;
        }

        //残された条件から、フォーカスが存在するのは確定
        //前フレームのフォーカスを更新
        previousFocus = EventSystem.current.currentSelectedGameObject;
    }

}
