using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCameraMove : MonoBehaviour
{
    bool onece = false;
    [SerializeField] float xpos;
    [SerializeField] float ypos;
    [SerializeField] float yrote;
    void Start()
    { 
        if (!onece)
        {
            onece = true;
            Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
            Transform myTransform = this.transform;
            Vector3 rote = myTransform.localEulerAngles;
            myTransform.position = new Vector3(playerPos.x + xpos, playerPos.y + ypos, playerPos.z);
            myTransform.eulerAngles = new(rote.x , rote.y + yrote, rote.z);
        }
    }
    void Update()
    {
       
    }
}
