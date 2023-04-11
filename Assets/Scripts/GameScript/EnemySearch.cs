using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearch : MonoBehaviour
{
    //�v���C���[���N�����Ă���t���O
    public bool playerON;

    //collider�R���|�[�l���g
    SphereCollider col;

    void Start()
    {
        col = GetComponent<SphereCollider>();
    }

    void Update()
    {

    }

    //Collider�ɐڐG���Ă����
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerON = true;
            col.radius = 8;
        }
    }

    //Collider����o����
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerON = false;
            col.radius = 5;
        }
    }
}