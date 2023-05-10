using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //4/17 ���C�����j���[���ڂ��ł������߂قƂ�ǂO�P�Q�͎g��Ȃ�
    //0 = ���C�����j���[�֘A | 1 = �t�X�e�[�W | 2 = �ăX�e�[�W | 3 = �H�X�e�[�W | 4 = �~�X�e�[�W | 5 = �e�X�g�X�e�[�W
    public void MenuTempleteScene()
    {
        Pause.Instance.StartGame();
        FadeManager.Instance.LoadSceneIndex(0, 1);
    }

    public void SpringScene()
    {
        FadeManager.Instance.LoadSceneIndex(1, 1);

    }

    public void SummerScene()
    {
        FadeManager.Instance.LoadSceneIndex(2, 1);

    }

    public void AutumnScene()
    {
        FadeManager.Instance.LoadSceneIndex(3, 1);

    }
    public void WinterScene()
    {
        FadeManager.Instance.LoadSceneIndex(4, 1);
    }

    public void TestScene()
    {
        FadeManager.Instance.LoadSceneIndex(1, 1);
    }

}