using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager :Singleton <GameManager>
{
    //プレイヤーの変数（体力など
    [Header("PlayerのHP")]
    public int HealthCounter = 100;
    public int HealthMAX = 100;
    public int DeadHealth = 0;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage()
    {
        HealthCounter -= 10;

        state_damage = true;//こいつはplayercontrollerにあって連続でダメージが受けないように、必要
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
