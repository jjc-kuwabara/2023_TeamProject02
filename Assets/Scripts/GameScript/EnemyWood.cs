using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWood : MonoBehaviour
{

    public int hitCount;   //–Ø‚Ì‘Ì—Í
    public GameObject[] kafun;   //‚Ü‚«U‚ç‚·‰Ô•²

    int range;    //¶¬‚·‚é‰Ô•²‚ğƒ‰ƒ“ƒ_ƒ€‚É‚·‚é

    public bool hit = false;   //‰£‚ç‚ê‚½‚Ìƒtƒ‰ƒO
    public bool dead = false;   //Ø‚è“|‚³‚ê‚½‚Æ‚«‚Ìƒtƒ‰ƒO

    void Start()
    {  
    }

    void Update()
    {
        if (hitCount <= 0) 
        { 
            dead = true;
            Down();
        }
        Sprinkle();
    }
    public void Hit()
    {
        hitCount--;
        hit = true;
    }

    public void Down()    //Ø‚è“|‚³‚ê‚½‚çÁ‚¦‚é
    {
        if(dead)
        {
            Destroy(this.gameObject,3);
        }
    }

    public void Sprinkle()    //‰£‚ç‚ê‚½‚ç‰Ô•²‚ğT‚­
    {
        if(hit)
        {
            range = Random.Range(0, 5);
            Vector3 sprpos = this.transform.position;
            Instantiate(kafun[range], new Vector3(sprpos.x,sprpos.y,sprpos.z),Quaternion.identity);
            hit = false;
        }
    }
}
