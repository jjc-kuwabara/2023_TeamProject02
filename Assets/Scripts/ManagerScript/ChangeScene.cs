using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //4/17 ���C�����j���[���ڂ��ł������߂قƂ�ǂO�P�Q�͎g��Ȃ�
    //0 = �^�C�g���@| 1�@���@�X�e�[�W�I���@| 2�@���@������ʁ@| 3�@���@�t�X�e�[�W�@| 4�@���@�ăX�e�[�W�@| 5�@���@�H�X�e�[�W�@| 6�@���@�~�X�e�[�W | 7 = �e�X�g�X�e�[�W
    //8�@���@���C�����j���[�e���v���[�g
    public void InstructionsScene()
    {
        FadeManager.Instance.LoadSceneIndex(2, 1);
    }

    public void TitleScene()
    {
        FadeManager.Instance.LoadSceneIndex(0, 1);

    }

    public void StageSelectScene()
    {
        FadeManager.Instance.LoadSceneIndex(1, 1);

    }

    public void SpringStageScene()
    {
        FadeManager.Instance.LoadSceneIndex(3, 1);

    }
    public void SummerStageScene()
    {
        FadeManager.Instance.LoadSceneIndex(4, 1);
    }

    public void AutumnStageScene()
    {
        FadeManager.Instance.LoadSceneIndex(5, 1);
    }

    public void WinterStageScene()
    {
        FadeManager.Instance.LoadSceneIndex(6, 1);
    }

    public void TestStageScene()
    {
        FadeManager.Instance.LoadSceneIndex(7, 1);
    }

    public void MenuScene()
    {
        FadeManager.Instance.LoadSceneIndex(8, 1);
    }
}
