using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAreaScript : MonoBehaviour
{
    PlayerControl controll;//playerControl����Ăяo���ĕۑ�
    public GameObject checkpoint; 

    // Start is called before the first frame update
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
            Transform myTransform = other.transform;
            Vector3 rote = myTransform.localEulerAngles;
            rote.y = 90f;
            myTransform.eulerAngles = rote;  //�E����
            controll.Dead(true);//�L�����R�����I���ɂ���B
        }
       
    }
}