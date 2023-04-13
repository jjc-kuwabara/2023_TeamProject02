using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager :Singleton <GameManager>
{
    //�v���C���[�̕ϐ��i�̗͂Ȃ�
    [Header("Player��HP")]
    public float HPCurrent = 100;
    public float HPMax = 100;
    public float HPMin = 0;

    //�Q�[���̃N���A��V�[���ȂǕK�v�ȃt���O
    [Header("�Q�[���̐i�s�󋵂������t���O")]
    public bool gameStart = false;  //�Q�[���J�n�O
    public bool mainGame = false;   //�Q�[����
    public bool clearble = false;   //�N���A�\���
    public bool gameClear = false;  //�Q�[���N���A
    public bool gameOver = false;   //�Q�[���I�[�o�[

    //�悭�킩��Ȃ����ǕK�v�ȃt���O���Ǝv�������ǁA�d�v
    [Header("�Q�[���Ȃ��̃t���O")]
    public bool state_damage = false;
    public bool hpFull;  //HP���ő傩�𔻒肷��t���O

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
        if(state_damage)
        {
            StartCoroutine(DelayTime());
        }
    }

    public void Damage(float damage)
    {
        HPCurrent -= damage;
        
        state_damage = true;//������playercontroller�ɂ����ĘA���Ń_���[�W���󂯂Ȃ��悤�ɁA�K�v
    }
    public void Heal(float heal)
    {
        HPCurrent += heal;
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
        Debug.Log("�Q�[���I�[�o�[");
    }

    public void GameClear()//�Q�[���N���A�̃^�C�����C����L��
    {
        mainGame = false;
        gameClear = true;
        Debug.Log("�Q�[���N���A�I");
    }
    IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(1);
        state_damage = false;
    }
}
