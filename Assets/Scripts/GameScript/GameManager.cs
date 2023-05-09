using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class GameManager :Singleton <GameManager>
{
    //プレイヤーの変数（体力など
    [Header("PlayerのHP")]
    public float HPCurrent = 10;
    public float HPMax = 10;
    public float HPMin = 0;

    //ゲームのクリアやシーンなど必要なフラグ
    [Header("ゲームの進行状況を示すフラグ")]
    public bool gameStart = false;  //ゲーム開始前
    public bool mainGame = false;   //ゲーム中
    public bool clearble = false;   //クリア可能状態
    public bool gameClear = false;  //ゲームクリア
    public bool gameOver = false;   //ゲームオーバー

    //よくわからないけど必要なフラグかと思ったけど、重要
    [Header("ゲーム内のフラグ")]
    public bool state_damage = false;
    public bool hpFull;  //HPが最大かを判定するフラグ

    GameObject HPGauge;

    private GameObject[] enemyobject;

    [SerializeField] GameObject axe; //Timeline中に武器を隠すため

    [Header("TimelineCanvas")]
    [SerializeField] GameObject timelinecanvas;
    [Header("TimelineDirecter")]
    [SerializeField] PlayableDirector TL_GameStart;
    [SerializeField] PlayableDirector TL_GameClear;
    [SerializeField] PlayableDirector TL_GameOver;
    void Start()
    {
        axe.SetActive(false);
        timelinecanvas.SetActive(true);
        Pause.Instance.CanvasInit();
        TL_GameStart.Play();
        HPCurrent = HPMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (HPCurrent <= 0)
        {
            GameOver();
        }

        enemyobject = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemyobject.Length <= 0)
        {
            GameClear();
        }
        if(TL_GameStart.state == PlayState.Playing && Input.GetKeyDown(KeyCode.T))
        {
            DemoSkip();
        }
        HPCheck();
        HPCurrent = Mathf.Clamp(HPCurrent, HPMin, HPMax);
    }
    public void MainGame()
    {
        mainGame = true;
        Pause.Instance.canvas[0].SetActive(true);
        timelinecanvas.SetActive(false);
        HPGauge = GameObject.FindWithTag("HPGauge");
        HPGauge.GetComponent<Image>().fillAmount = 1;
        axe.SetActive(true);
    }
    public void DemoSkip()
    {
        mainGame = true;
        TL_GameStart.Stop();
        Pause.Instance.canvas[0].SetActive(true);
        timelinecanvas.SetActive(false);
        axe.SetActive(true);
    }
    public void Damage(float damage)
    {
        HPCurrent -= damage;

        float HPvalue = HPCurrent / HPMax;
        HPGauge.GetComponent<Image>().fillAmount = HPvalue;
        SoundManager.Instance.PlaySE_Sys(7);
        state_damage = true;//こいつはplayercontrollerにあって連続でダメージが受けないように、必要
    }
    public void Heal(float heal)
    {
        HPCurrent += heal;

        float HPvalue = HPCurrent / HPMax;
        HPGauge.GetComponent<Image>().fillAmount = HPvalue;
        SoundManager.Instance.PlaySE_Sys(6);
        Debug.Log("HP回復");
    }
    public void HPCheck()  //HPが最大か判定
    {
        if(HPCurrent == HPMax) { hpFull = true; }
        else { hpFull = false; }
    }

    public void GameOver()//ゲームオーバーのタイムラインを有効
    {
        mainGame = false;
        gameOver = true;
        Pause.Instance.canvas[0].SetActive(false);
        timelinecanvas.SetActive(true);
        TL_GameOver.Play();
        SoundManager.Instance.BGMSource.Stop();
    }

    public void GameClear()//ゲームクリアのタイムラインを有効
    {
        mainGame = false;
        gameClear = true;
        Pause.Instance.canvas[0].SetActive(false);
        timelinecanvas.SetActive(true);
        TL_GameClear.Play();
        SoundManager.Instance.BGMSource.Stop();
    }
}
