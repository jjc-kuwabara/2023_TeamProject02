using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAreaScript : MonoBehaviour
{
    PlayerControl controll;//playerControlから呼び出して保存
    public GameObject checkpoint; 

    // Start is called before the first frame update
    void Start()
    {
        controll = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();//Playerタグの人を呼び出す
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("侵入したオブジェクト名＝" + other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            
            Debug.Log("Playerでした");
            controll.Dead(false);//Deadでキャラコンをオフにする
            other.transform.position = new Vector3(0, 2, 0); //リスの高さ
            Transform myTransform = other.transform;
            Vector3 rote = myTransform.localEulerAngles;
            rote.y = 90f;
            myTransform.eulerAngles = rote;  //右向き
            controll.Dead(true);//キャラコンをオンにする。
        }
       
    }
}