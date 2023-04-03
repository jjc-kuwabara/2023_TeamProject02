using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWood : MonoBehaviour
{

    public int hitCount;   //木の体力
    public GameObject[] kafun;   //まき散らす花粉

    int range;    //生成する花粉をランダムにする

    [SerializeField] bool hit = false;   //殴られた時のフラグ
    [SerializeField] bool dead = false;   //切り倒されたときのフラグ

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

    public void Down()    //切り倒されたら消える
    {
        if(dead)
        {
            Destroy(this.gameObject,3);
        }
    }

    public void Sprinkle()    //殴られたら花粉を撒く
    {
        if(hit)
        {
            range = Random.Range(0, 5);
            Vector3 sprpos = this.transform.position;
            Instantiate(kafun[range], new Vector3(sprpos.x,sprpos.y,sprpos.z),Quaternion.identity);
        }
    }
}
