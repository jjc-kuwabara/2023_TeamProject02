using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAreaScript : MonoBehaviour
{
    PlayerControl controll;//playerControl����Ăяo���ĕۑ�

    void Start()
    {
        controll = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();//Player�^�O�̐l���Ăяo��
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�N�������I�u�W�F�N�g����" + other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player�ł���");
            controll.Dead(false);//Dead�ŃL�����R�����I�t�ɂ���
            other.transform.position = new Vector3(0, 2, 0); //���X�̍���
            other.transform.rotation = Quaternion.identity; //���X�̉�]
            controll.Dead(true);//�L�����R�����I���ɂ���B
        }

    }
}