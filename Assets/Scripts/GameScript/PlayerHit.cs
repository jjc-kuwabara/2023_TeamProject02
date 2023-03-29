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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log(other.gameObject.name + "“G‚É“–‚½‚Á‚½‚æ");
        }
       
    }
}
