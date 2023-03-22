using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAreaScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("侵入したオブジェクト名＝" + other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Playerでした");
            other.transform.position = new Vector3(0, 2, 0); //リスの高さ
            other.transform.rotation = Quaternion.identity; //リスの回転
        }

    }
}
