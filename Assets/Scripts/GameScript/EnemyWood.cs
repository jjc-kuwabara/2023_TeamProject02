using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWood : MonoBehaviour
{

    public int hitCount;   //木の体力
    public GameObject[] kafun;   //まき散らす花粉

    int range;    //生成する花粉をランダムにする

    public bool hit = false;   //殴られた時のフラグ
    public bool dead = false;   //切り倒されたときのフラグ
    public bool death = false;

    Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (hitCount <= 0 && !death)   //一度だけ処理する
        {
            EnemyMove move = gameObject.GetComponent<EnemyMove>();
            move.enabled = false;    //コンポーネントの非有効化
            dead = true;
            death = true;
            Down();
        }
        //Sprinkle();
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
            animator.SetTrigger("Die");
            GameObject deadEffect = Instantiate(EffectManager.Instance.StageFX[1],transform.position,Quaternion.identity);  //燃えるエフェクト
            Destroy(deadEffect, 3);
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
            hit = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Weapon") { return; }
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.Damage(10);
            Debug.Log(other.gameObject.name + "に当たったよ");
            GameObject HitEffect;
            HitEffect = Instantiate(EffectManager.Instance.StageFX[0], transform.position, Quaternion.identity);
            HitEffect.transform.position = other.gameObject.transform.position;
            HitEffect.transform.parent = other.gameObject.transform;
            Destroy(HitEffect, 1.0f);
        }
    }
}
