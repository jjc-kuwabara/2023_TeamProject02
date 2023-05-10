using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{

    PlayerControl control;

    void Start()
    {
        control = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();//Player^Oฬl๐ฤัoท
        
    }
    void Update()
    {
    }
    private void OnTriggerExit(Collider other)
    {
        if(control.attack)
        {
            if (other.tag == "Enemy")
            {
               Debug.Log("U");
               other.GetComponent<EnemyWood>().Hit();
                control.FlameCharge();
                SoundManager.Instance.PlaySE_Game(1);
            }
            if (other.tag == "PollenEnemy")
            {
                Debug.Log("U");
                other.GetComponent<EnemyPollen>().Hit();
                control.FlameCharge();
            }
        }     
    }
}
