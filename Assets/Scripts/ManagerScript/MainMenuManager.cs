using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("�K�v�ȃL�����o�X")]
    [SerializeField] GameObject[] canvas;

    [Header("�e���j���[�̏����J�[�\���Ώ�")]
    [SerializeField] GameObject[] focusobject;

    GameObject currentFocus; //���ݑI�����Ă���Ώ�
    GameObject previousFocus; //�O�t���[���ɑI�����Ă����Ώ�

    // Start is called before the first frame update
    void Start()
    {
        CanvasInIt();

        canvas[0].SetActive(true);

        EventSystem.current.SetSelectedGameObject(focusobject[0]);
    }

    // Update is called once per frame
    void Update()
    {
        FocusCheck();
    }

    void CanvasInIt()
    {
        //canvasGroup�̗v�f����Canvas�̐��ƈꏏ�ɂ���B�@canvasGroup = new CanvasGroup[canvas.length];

        for(int i = 0; i < canvas.Length; i++)
        {
            canvas[i].SetActive(false);//���ׂẴ��j���[���\��
        }
    }

    //���j���[�̈ړ��i�e���j���[���ڂ̃C�x���g�g���K�[�ŃL�����o�X�̔ԍ����w��j
    public void Transition_menu(int nextMenu)
    {
        CanvasInIt();

        canvas[nextMenu].SetActive(true);//���̃��j���[��\��

        EventSystem.current.SetSelectedGameObject(focusobject[nextMenu]);//���̃��j���[�̏����J�[�\���ʒu

        SoundManager.Instance.PlaySE_Sys(1);
         
    }
    //�t�H�[�J�X�Ώۂ̃`�F�b�N
    void FocusCheck()
    {
        currentFocus = EventSystem.current.currentSelectedGameObject;//���݂̃t�H�[�J�X�̍��ڂ̊i�[

        if (currentFocus == previousFocus) return;//�O��ƕς��Ȃ��ꍇ�͉��������I��

        if (currentFocus != previousFocus)
        {
            SoundManager.Instance.PlaySE_Sys(0);
            return;
        }
        if(currentFocus == null)//�t�H�[�J�X�Ώۂ��Ȃ������ꍇ�A�O�t���[���܂őI�����Ă������ڂ�I����Ԃ�
        {
            EventSystem.current.SetSelectedGameObject(previousFocus);
            return;
        }

        //�c���ꂽ��������t�H�[�J�X���Ă��鍀�ڂ����݂���̂��m��
        //�O�t���[���̑Ώۂ��X�V����
        previousFocus = EventSystem.current.currentSelectedGameObject;
    }

    //�Q�[���I���̏���
    public void Quit()
    {
        //unity��
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
//�r���h�������s�f�[�^�Ńv���C���I������ꍇ
          Application.Quit();
#endif



    }
}
