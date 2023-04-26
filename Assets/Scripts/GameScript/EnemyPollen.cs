using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPollen : MonoBehaviour
{
    public bool dead = false;   //切り倒されたときのフラグ
    public bool attack = false;

    EnemyMove move;
    EnemyAttackArea attackArea;
    PlayerControl control;
    
    void Start()
    {
        move = GetComponent<EnemyMove>();
        attackArea = transform.Find("EnemyAttackArea").GetComponent<EnemyAttackArea>();
        control = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }

    void Update()
    {
        if (attackArea.playerIn && !GameManager.Instance.state_damage)
        {
            attack = true;
        }
    }
    public void Hit()
    {
        GameObject deadEffect = Instantiate(EffectManager.Instance.StageFX[1], transform.position, Quaternion.identity);  //燃えるエフェクト
        Destroy(deadEffect, 3);
        Destroy(this.gameObject, 3);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && attack && !control.invicible)
        {
            GameManager.Instance.state_damage = true;
            GameManager.Instance.Damage(10);
            Debug.Log(other.gameObject.name + "に当たった");
            GameObject HitEffect;
            HitEffect = Instantiate(EffectManager.Instance.StageFX[0], transform.position, Quaternion.identity);
            HitEffect.transform.position = other.gameObject.transform.position;
            HitEffect.transform.parent = other.gameObject.transform;
            Destroy(HitEffect, 1.0f);
            attack = false;
        }
        if (other.gameObject.tag == "Weapon") { return; }
    }
}
