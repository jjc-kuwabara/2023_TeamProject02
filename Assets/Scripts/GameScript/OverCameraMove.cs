using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverCameraMove : MonoBehaviour
{
    bool onece = false;
    void Start()
    {
       
    }

    void Update()
    {
        if(GameManager.Instance.gameOver && !onece)
        {
            onece = true;
            Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
            Transform myTransform = this.transform;
            Vector3 rote = myTransform.localEulerAngles;
            myTransform.position = new Vector3(playerPos.x + 1.2f, playerPos.y + 2f, playerPos.z);
            myTransform.eulerAngles = new(rote.x + 90f, rote.y , rote.z +90f);
        }
    }
}
