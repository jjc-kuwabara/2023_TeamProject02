using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pause : Singleton<Pause>
{
    [Header("�|�[�Y���j���[�̃J�[�\�������ʒu")]
    [SerializeField] GameObject focusPauseMenu;

    GameObject currentFocus;    //����
    GameObject previousFocus;   //�O�t���[��

    bool pause;

    void Start()
    {
        CanvasInit();
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
            //�ꎞ��~
            Time.timeScale = 0;
            pause = true;
            Debug.Log("�ꎞ��~");
            GameManager.Instance.mainCanvas[1].SetActive(true);
            GameManager.Instance.mainCanvas[0].SetActive(false);
            EventSystem.current.SetSelectedGameObject(focusPauseMenu);
        }
    }
    public void StartGame()
    {
       //�ĊJ
       Time.timeScale = 1;
       pause = false;
       Debug.Log("�ĊJ");
       GameManager.Instance.mainCanvas[0].SetActive(true);
       GameManager.Instance.mainCanvas[1].SetActive(false);
    }

    public void CanvasInit()
    {
        for (int i = 0; i < GameManager.Instance.mainCanvas.Length; i++)
        {
            GameManager.Instance.mainCanvas[i].SetActive(false);
        }
    }

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
