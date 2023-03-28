using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    PlayerControl control;
    Rigidbody rig;

    void Start()
    {
        control = GetComponent<PlayerControl>();
        rig = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag == "Enemy")
        {
            GameManager.Instance.Damage(10);
            Debug.Log("ìGÇ∆ê⁄êG");
        }
    }
}
