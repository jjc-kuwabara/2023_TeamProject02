using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    EnemyWood wood;
    PlayerControl control;
    void Start()
    {
        wood = GetComponent<EnemyWood>();
        control = GetComponent<PlayerControl>();
    }

    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            wood.Hit();
            Debug.Log("HIT");
            control.attack = false;
            control.weaponObj.SetActive(false);
        }
    }
}
