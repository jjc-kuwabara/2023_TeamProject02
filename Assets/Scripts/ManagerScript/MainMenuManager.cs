using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("必要なキャンバス")]
    [SerializeField] GameObject[] canvas;

    [Header("各メニューの初期カーソル対象")]
    [SerializeField] GameObject[] focusobject;

    GameObject currentFocus; //現在選択している対象
    GameObject previousFocus; //前フレームに選択していた対象

    // Start is called before the first frame update
    void Start()
    {
        CanvasInIt();
        SoundManager.Instance.PlayBGM(2);
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
        //canvasGroupの要素数をCanvasの数と一緒にする。　canvasGroup = new CanvasGroup[canvas.length];

        for(int i = 0; i < canvas.Length; i++)
        {
            canvas[i].SetActive(false);//すべてのメニューを非表示
        }
    }

    //メニューの移動（各メニュー項目のイベントトリガーでキャンバスの番号を指定）
    public void Transition_menu(int nextMenu)
    {
        CanvasInIt();

        canvas[nextMenu].SetActive(true);//次のメニューを表示

        EventSystem.current.SetSelectedGameObject(focusobject[nextMenu]);//次のメニューの初期カーソル位置

        SoundManager.Instance.PlaySE_Sys(1);
         
    }
    //フォーカス対象のチェック
    void FocusCheck()
    {
        currentFocus = EventSystem.current.currentSelectedGameObject;//現在のフォーカスの項目の格納

        if (currentFocus == previousFocus) return;//前回と変わらない場合は何もせず終了

        if (currentFocus != previousFocus)
        {
            SoundManager.Instance.PlaySE_Sys(0);
            
        }
        if(currentFocus == null)//フォーカス対象がなかった場合、前フレームまで選択していた項目を選択状態に
        {
            EventSystem.current.SetSelectedGameObject(previousFocus);
            return;
        }

        //残された条件からフォーカスしている項目が存在するのが確定
        //前フレームの対象を更新する
        previousFocus = EventSystem.current.currentSelectedGameObject;
    }

    
    //ゲーム終了の処理
    public void Quit()
    {
        //unity上
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
//ビルドした実行データでプレイを終了する場合
          Application.Quit();
#endif



    }
}
