using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAreaScript : MonoBehaviour
{

    public GameObject checkpoint; 

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
        Debug.Log("�N�������I�u�W�F�N�g����" + other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            
            Debug.Log("Player�ł���");
            other.transform.position = new Vector3(0,2,0);
            other.transform.rotation = Quaternion.identity; //���X�̉�]
        }

    }
}
