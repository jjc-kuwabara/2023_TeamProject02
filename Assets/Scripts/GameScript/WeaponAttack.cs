using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{

    PlayerControl control;
    public float charge = 0.2f;

    void Start()
    {
        control = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();//Player�^�O�̐l���Ăяo��
        
    }
    void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if(control.attack)
        {
            if (other.tag == "Enemy")
            {
               Debug.Log("�U��");
               other.GetComponent<EnemyWood>().Hit();
                control.slider.value += charge;
            }
        }     
    }
}
