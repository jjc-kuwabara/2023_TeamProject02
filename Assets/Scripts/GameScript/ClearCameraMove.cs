using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCameraMove : MonoBehaviour
{
    bool onece = false;
    void Start()
    {
        
    }

    void Update()
    {
        if(GameManager.Instance.gameClear && !onece)
        {
            onece = true;
            Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
            Transform myTransform = this.transform;
            Vector3 rote = myTransform.localEulerAngles;
            myTransform.position = new Vector3(playerPos.x + 3f, playerPos.y + 2f, playerPos.z);
            myTransform.eulerAngles = new(rote.x + 8f, rote.y - 90f, rote.z);
        }
    }
}
