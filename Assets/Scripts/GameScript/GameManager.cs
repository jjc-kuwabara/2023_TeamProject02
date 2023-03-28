using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager :Singleton <GameManager>
{
    //�v���C���[�̕ϐ��i�̗͂Ȃ�
    [Header("Player��HP")]
    public int HealthCounter = 100;
    public int HealthMAX = 100;
    public int DeadHealth = 0;

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

        state_damage = true;//������playercontroller�ɂ����ĘA���Ń_���[�W���󂯂Ȃ��悤�ɁA�K�v
    }

    public void GameOver()
    {
        mainGame = false;
        gameOver = true;
        Debug.Log("�Q�[���I�[�o�[");
    }

    public void GameClear()
    {
        mainGame = false;
        gameClear = true;
        Debug.Log("�Q�[���N���A�I");
    }
}
