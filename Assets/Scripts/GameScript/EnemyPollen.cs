using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPollen : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") 
        { 
            Destroy(this.gameObject); 
            return;
        }
        else
        {
            Destroy(this.gameObject, 2);
        }
       

    }
}
