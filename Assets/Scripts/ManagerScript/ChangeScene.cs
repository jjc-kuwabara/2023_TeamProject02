using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //4/17 メインメニュー項目ができたためほとんど０１２は使わない
    //0 = メインメニュー関連 | 1 = 春ステージ | 2 = 夏ステージ | 3 = 秋ステージ | 4 = 冬ステージ | 5 = テストステージ
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
        FadeManager.Instance.LoadSceneIndex(5, 1);
    }

    public void EndGame()
    {
        Application.Quit();//ゲーム終了
    }

}