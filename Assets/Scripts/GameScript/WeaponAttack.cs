using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{

    PlayerControl control;

    void Start()
    {
        control = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();//Playerタグの人を呼び出す
        
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
               Debug.Log("攻撃");
               other.GetComponent<EnemyWood>().Hit();
            }
        }     
    }
}
