using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearch : MonoBehaviour
{
    //プレイヤーが侵入しているフラグ
    public bool playerON;

    //colliderコンポーネント
    SphereCollider col;

    void Start()
    {
        col = GetComponent<SphereCollider>();
    }

    void Update()
    {

    }

    //Colliderに接触している間
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerON = true;
            col.radius = 6;
        }
    }

    //Colliderから出たら
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerON = false;
            col.radius = 3;
        }
    }
}