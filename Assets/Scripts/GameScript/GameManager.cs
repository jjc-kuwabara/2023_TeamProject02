using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.EventSystems;

public class GameManager :Singleton <GameManager>
{
    //�Q�[���̃t�H�[�J�X����Ƃ��̃I�u�W�F�N�g
    [Header("�e���j���[�̏����J�[�\���ʒu")]
    [SerializeField] GameObject[] FocusObject;

    //�v���C���[�̕ϐ��i�̗͂Ȃ�
    [Header("Player��HP")]
    public float HPCurrent = 10;
    public float HPMax = 10;
    public float HPMin = 0;

    //�Q�[���̃N���A��V�[���ȂǕK�v�ȃt���O
    [Header("�Q�[���̐i�s�󋵂������t���O")]
    public bool gameStart = false;  //�Q�[���J�n�O
    public bool mainGame = false;   //�Q�[����
    public bool clearble = false;   //�N���A�\���
    public bool gameClear = false;  //�Q�[���N���A
    public bool gameOver = false;   //�Q�[���I�[�o�[

    //�悭�킩��Ȃ����ǕK�v�ȃt���O���Ǝv�������ǁA�d�v
    [Header("�Q�[�����̃t���O")]
    public bool state_damage = false;
    public bool hpFull;  //HP���ő傩�𔻒肷��t���O
    public bool hpCheck;  //HP�����������p

    [Header("MainGame����UI")]
    public GameObject[] mainCanvas;
    GameObject HPGauge;
    [Header("MainGame�Ŏg��")]
    [SerializeField] GameObject mainCamera;
    
    private GameObject[] enemyobject;
    PlayerControl control;
    [SerializeField] GameObject axe; //Timeline���ɕ�����B������

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
        int vo = Random.Range(8, 11);
        SoundManager.Instance.PlaySE_Game(vo);
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
        state_damage = true;//�A���Ń_���[�W���󂯂Ȃ��悤��
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
        Debug.Log("HP��");
    }
    public void HPCheck()  //HP���ő傩����
    {
        if(HPCurrent == HPMax) { hpFull = true; }
        else { hpFull = false; }
    }

    public void GameOver()//�Q�[���I�[�o�[�̃^�C�����C����L��
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
        int vo = Random.Range(22, 27);
        SoundManager.Instance.PlaySE_Game(vo);
    }

    public void GameClear()//�Q�[���N���A�̃^�C�����C����L��
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
            if (HPCurrent == HPMax)
            {
                int vo = Random.Range(20, 22);
                SoundManager.Instance.PlaySE_Game(vo);
            }
            if (HPCurrent < HPMax && HPCurrent >= 7)
            {
                int vo = Random.Range(17, 20);
                SoundManager.Instance.PlaySE_Game(vo);
            }
            if (HPCurrent <= 6 && HPCurrent >= 4)
            {
                int vo = Random.Range(14, 17);
                SoundManager.Instance.PlaySE_Game(vo);
            }
            if (HPCurrent <= 3 && HPCurrent >= 1)
            {
                int vo = Random.Range(11, 14);
                SoundManager.Instance.PlaySE_Game(vo);
            }
    }
}
