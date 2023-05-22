using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.EventSystems;

public class GameManager :Singleton <GameManager>
{
    //ゲームのフォーカスするときのオブジェクト
    [Header("各メニューの初期カーソル位置")]
    [SerializeField] GameObject[] FocusObject;

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
    public bool hpCheck;  //HPが減った時用

    [Header("MainGame中のUI")]
    public GameObject[] mainCanvas;
    GameObject HPGauge;
    [Header("MainGameで使う")]
    [SerializeField] GameObject mainCamera;
    
    private GameObject[] enemyobject;
    PlayerControl control;
    [SerializeField] GameObject axe; //Timeline中に武器を隠すため

    [Header("TimelineCanvas")]
    [SerializeField] GameObject startCanvas;
    [SerializeField] GameObject clearCanvas;
    [SerializeField] GameObject overCanvas;
    [Header("TimelineDirecter")]
    [SerializeField] PlayableDirector TL_GameStart;
    [SerializeField] PlayableDirector TL_GameClear;
    [SerializeField] PlayableDirector TL_GameOver;
    [Header("TimelineCamera")]
    [SerializeField] GameObject startCamera;
    [SerializeField] GameObject clearCamera;
    [SerializeField] GameObject overCamera;
    void Start()
    {
        control = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        control.FlameGauge.GetComponent<Image>().fillAmount = 0;
        mainCamera.SetActive(false);
        axe.SetActive(false);
        startCanvas.SetActive(true);
        clearCanvas.SetActive(false);
        overCanvas.SetActive(false);
        Pause.Instance.CanvasInit();
        TL_GameStart.Play();
        HPCurrent = HPMax;
    }
    // Update is called once per frame
    void Update()
    {
        if (HPCurrent <= 0 && !gameOver)
        {
            GameOver();
        }

        enemyobject = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemyobject.Length <= 0 && !gameClear)
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
        SoundManager.Instance.PlayBGM(0);
        mainCanvas[0].SetActive(true);
        startCanvas.SetActive(false);
        HPGauge = GameObject.FindWithTag("HPGauge");
        HPGauge.GetComponent<Image>().fillAmount = 1;
        axe.SetActive(true);
        startCamera.SetActive(false);
        mainCamera.SetActive(true);
        control.FlameGauge.GetComponent<Image>().fillAmount = 0;
    }
    public void DemoSkip()
    {
        mainGame = true;
        SoundManager.Instance.PlayBGM(0);
        TL_GameStart.Stop();
        mainCanvas[0].SetActive(true);
        startCanvas.SetActive(false);
        HPGauge = GameObject.FindWithTag("HPGauge");
        HPGauge.GetComponent<Image>().fillAmount = 1;
        axe.SetActive(true);
        startCamera.SetActive(false);
        mainCamera.SetActive(true);
        control.FlameGauge.GetComponent<Image>().fillAmount = 0;
    }
    public void Damage(float damage)
    {
        HPCurrent -= damage;
        state_damage = true;//連続でダメージが受けないように
        float HPvalue = HPCurrent / HPMax;
        HPGauge.GetComponent<Image>().fillAmount = HPvalue;
        SoundManager.Instance.PlaySE_Sys(7);
        hpCheck = true;
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
        EventSystem.current.SetSelectedGameObject(FocusObject[0]);
        axe.SetActive(false);
        mainCamera.SetActive(false);
        overCamera.SetActive(true);
        mainCanvas[0].SetActive(false);
        overCanvas.SetActive(true);
        TL_GameOver.Play();
        SoundManager.Instance.BGMSource.Stop();
    }

    public void GameClear()//ゲームクリアのタイムラインを有効
    {
        mainGame = false;
        gameClear = true;
        EventSystem.current.SetSelectedGameObject(FocusObject[1]);
        axe.SetActive(false);
        mainCamera.SetActive(false);
        clearCamera.SetActive(true);
        mainCanvas[0].SetActive(false);
        clearCanvas.SetActive(true);
        TL_GameClear.Play();
        SoundManager.Instance.BGMSource.Stop();
    }
}
