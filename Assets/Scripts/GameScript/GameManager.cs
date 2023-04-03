using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager :Singleton <GameManager>
{
    //プレイヤーの変数（体力など
    [Header("PlayerのHP")]
    public float HPCurrent = 100;
    public float HPMax = 100;
    public float HPMin = 0;

    //ゲームのクリアやシーンなど必要なフラグ
    [Header("ゲームの進行状況を示すフラグ")]
    public bool gameStart = false;  //ゲーム開始前
    public bool mainGame = false;   //ゲーム中
    public bool clearble = false;   //クリア可能状態
    public bool gameClear = false;  //ゲームクリア
    public bool gameOver = false;   //ゲームオーバー

    //よくわからないけど必要なフラグかと思ったけど、重要
    [Header("ゲームないのフラグ")]
    public bool state_damage = false;
    public bool hpFull;  //HPが最大かを判定するフラグ

    // Start is called before the first frame update
    void Start()
    {
        mainGame = true;
        HPCurrent = HPMax;
    }

    // Update is called once per frame
    void Update()
    {
        if(HPCurrent <= 0)
        {
            GameOver();
        }
        HPCheck();
        HPCurrent = Mathf.Clamp(HPCurrent, HPMin, HPMax);
    }

    public void Damage(float damage)
    {
        HPCurrent -= damage;
        
        
        state_damage = true;//こいつはplayercontrollerにあって連続でダメージが受けないように、必要
    }
    public void Heal(float heal)
    {
        HPCurrent += heal;
    }
    public void HPCheck()  //HPが最大か判定
    {
        if(HPCurrent == HPMax) { hpFull = true; }
        else { hpFull = false; }
    }

    public void GameOver()
    {
        mainGame = false;
        gameOver = true;
        Debug.Log("ゲームオーバー");
    }

    public void GameClear()
    {
        mainGame = false;
        gameClear = true;
        Debug.Log("ゲームクリア！");
    }
}
