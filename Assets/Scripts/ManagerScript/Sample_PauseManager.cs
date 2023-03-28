using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //追加

public class Sample_PauseManager : MonoBehaviour
{
    //Time.timeScaleの値で時間の流れを変える
    //0なら時間停止、1なら等速;
    //Time.unscaledDeltaTime と Time.unscaledTime
    //は timeScaleに影響されない


    //ポーズ中フラグ
    public bool pauseFLG;
    public bool hitStopFLG;

    [Header("キャンバス")]
    [SerializeField] GameObject[] canvas;

    [Header("ポーズメニューのカーソル初期位置")]
    [SerializeField] GameObject focusPauseMenu;

    [Header("ヒットストップ")]
    [SerializeField] float timeScale = 0.02f;   //Time.timeScaleに設定する値 
    [SerializeField] float slowTime = 0.15f;    //時間を遅くしている時間
    float elapsedTime = 0f;                     //ヒットストップ時の経過時間

    //フォーカスが外れてしまわないようにする処理
    GameObject currentFocus;    //現在
    GameObject previousFocus;   //前フレーム

    void Start()
    {
        //初期化
        CanvasInit();

        //メインゲーム中のキャンバスだけアクティブ
        canvas[0].SetActive(true);

    }

    void Update()
    {
        Debug.Log("Update…処理してる");

        //ポーズ中じゃない時のみポーズボタンを受け付ける
        if (!pauseFLG)
        {
            //Pを押したら時間停止切替
            if (Input.GetKeyDown(KeyCode.P))
            {
                ChangePause(true);
                return;
            }

            if (!hitStopFLG)
            {
                //Oを押したらヒットストップ
                if (Input.GetKeyDown(KeyCode.O))
                {
                    HitStopStart();
                    return;
                }
            }

            //ヒットストップ中の時間計測処理
            HitStopTime();
        }

        //ポーズ中のみ
        //フォーカスが外れていないかチェック
        FocusCheck();


    }

    void FixedUpdate()
    {
        Debug.Log("FixedUpdate…処理してる");
    }


    //ポーズ処理
    public void ChangePause(bool flg)
    {
        CanvasInit();               //キャンバスを一旦全て消す
        pauseFLG = flg;             //ポーズフラグの切り替え

        //初期カーソル位置を設定
        EventSystem.current.SetSelectedGameObject(focusPauseMenu);

        //ポーズ中だったら時間を停止
        if (flg)
        {
            Time.timeScale = 0;         //timeScale = 0は時間が進まない
            canvas[1].SetActive(flg);   //ポーズ用キャンバス表示
        }
        else
        {
            Time.timeScale = 1;         //timeScale = 1は等倍
            canvas[0].SetActive(flg);   //メインゲームのキャンバス表示
        }
    }

    //ヒットストップ発生時
    void HitStopStart()
    {
        elapsedTime = 0f;
        Time.timeScale = timeScale;
        hitStopFLG = true;
    }

    //ヒットストップ発生後時間計測
    void HitStopTime()
    {
        if (hitStopFLG)
        {
            //時間計測
            elapsedTime += Time.unscaledDeltaTime;

            //時間経過で元の早さに戻る
            if (elapsedTime >= slowTime)
            {
                Time.timeScale = 1f;
                hitStopFLG = false;
            }
        }
    }


    //全てのキャンバスを非表示に
    void CanvasInit()
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].SetActive(false);
        }
    }
    
    //フォーカスが外れていないかチェック
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
