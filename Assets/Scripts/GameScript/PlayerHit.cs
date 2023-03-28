using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    PlayerControl control;
    // Start is called before the first frame update
    void Start()
    {
        control = GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            GameManager.Instance.Damage(10);
            Debug.Log("ìGÇ∆ê⁄êG");
        }
    }
}
