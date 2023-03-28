using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //�ǉ�

public class Sample_PauseManager : MonoBehaviour
{
    //Time.timeScale�̒l�Ŏ��Ԃ̗����ς���
    //0�Ȃ玞�Ԓ�~�A1�Ȃ瓙��;
    //Time.unscaledDeltaTime �� Time.unscaledTime
    //�� timeScale�ɉe������Ȃ�


    //�|�[�Y���t���O
    public bool pauseFLG;
    public bool hitStopFLG;

    [Header("�L�����o�X")]
    [SerializeField] GameObject[] canvas;

    [Header("�|�[�Y���j���[�̃J�[�\�������ʒu")]
    [SerializeField] GameObject focusPauseMenu;

    [Header("�q�b�g�X�g�b�v")]
    [SerializeField] float timeScale = 0.02f;   //Time.timeScale�ɐݒ肷��l 
    [SerializeField] float slowTime = 0.15f;    //���Ԃ�x�����Ă��鎞��
    float elapsedTime = 0f;                     //�q�b�g�X�g�b�v���̌o�ߎ���

    //�t�H�[�J�X���O��Ă��܂�Ȃ��悤�ɂ��鏈��
    GameObject currentFocus;    //����
    GameObject previousFocus;   //�O�t���[��

    void Start()
    {
        //������
        CanvasInit();

        //���C���Q�[�����̃L�����o�X�����A�N�e�B�u
        canvas[0].SetActive(true);

    }

    void Update()
    {
        Debug.Log("Update�c�������Ă�");

        //�|�[�Y������Ȃ����̂݃|�[�Y�{�^�����󂯕t����
        if (!pauseFLG)
        {
            //P���������玞�Ԓ�~�ؑ�
            if (Input.GetKeyDown(KeyCode.P))
            {
                ChangePause(true);
                return;
            }

            if (!hitStopFLG)
            {
                //O����������q�b�g�X�g�b�v
                if (Input.GetKeyDown(KeyCode.O))
                {
                    HitStopStart();
                    return;
                }
            }

            //�q�b�g�X�g�b�v���̎��Ԍv������
            HitStopTime();
        }

        //�|�[�Y���̂�
        //�t�H�[�J�X���O��Ă��Ȃ����`�F�b�N
        FocusCheck();


    }

    void FixedUpdate()
    {
        Debug.Log("FixedUpdate�c�������Ă�");
    }


    //�|�[�Y����
    public void ChangePause(bool flg)
    {
        CanvasInit();               //�L�����o�X����U�S�ď���
        pauseFLG = flg;             //�|�[�Y�t���O�̐؂�ւ�

        //�����J�[�\���ʒu��ݒ�
        EventSystem.current.SetSelectedGameObject(focusPauseMenu);

        //�|�[�Y���������玞�Ԃ��~
        if (flg)
        {
            Time.timeScale = 0;         //timeScale = 0�͎��Ԃ��i�܂Ȃ�
            canvas[1].SetActive(flg);   //�|�[�Y�p�L�����o�X�\��
        }
        else
        {
            Time.timeScale = 1;         //timeScale = 1�͓��{
            canvas[0].SetActive(flg);   //���C���Q�[���̃L�����o�X�\��
        }
    }

    //�q�b�g�X�g�b�v������
    void HitStopStart()
    {
        elapsedTime = 0f;
        Time.timeScale = timeScale;
        hitStopFLG = true;
    }

    //�q�b�g�X�g�b�v�����㎞�Ԍv��
    void HitStopTime()
    {
        if (hitStopFLG)
        {
            //���Ԍv��
            elapsedTime += Time.unscaledDeltaTime;

            //���Ԍo�߂Ō��̑����ɖ߂�
            if (elapsedTime >= slowTime)
            {
                Time.timeScale = 1f;
                hitStopFLG = false;
            }
        }
    }


    //�S�ẴL�����o�X���\����
    void CanvasInit()
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].SetActive(false);
        }
    }
    
    //�t�H�[�J�X���O��Ă��Ȃ����`�F�b�N
    void FocusCheck()
    {
        //���݂̃t�H�[�J�X���i�[
        currentFocus = EventSystem.current.currentSelectedGameObject;

        //�����O��܂ł̃t�H�[�J�X�Ɠ����Ȃ瑦�I��
        if (currentFocus == previousFocus) return;

        //�����t�H�[�J�X���O��Ă�����
        //�O�t���[���̃t�H�[�J�X�ɖ߂�
        if (currentFocus == null)
        {
            EventSystem.current.SetSelectedGameObject(previousFocus);
            return;
        }

        //�c���ꂽ��������A�t�H�[�J�X�����݂���̂͊m��
        //�O�t���[���̃t�H�[�J�X���X�V
        previousFocus = EventSystem.current.currentSelectedGameObject;
    }
}
