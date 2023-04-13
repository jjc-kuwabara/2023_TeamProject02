using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //0 = タイトル　| 1　＝　ステージ選択　| 2　＝　説明画面　| 3　＝　春ステージ　| 4　＝　夏ステージ　| 5　＝　秋ステージ　| 6　＝　冬ステージ | 7 = テストステージ
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
}
