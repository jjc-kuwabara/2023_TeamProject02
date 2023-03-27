using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAreaScript : MonoBehaviour
{
    PlayerControl controll;//playerControlから呼び出して保存

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
            other.transform.rotation = Quaternion.identity; //リスの回転
            controll.Dead(true);//キャラコンをオンにする。
        }

    }
}