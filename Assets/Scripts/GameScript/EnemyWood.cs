using UnityEngine;

public class EnemyWood : MonoBehaviour
{

    public int hitCount;   //木の体力
    float knockPower = 100f;

    int range;    //生成する花粉をランダムにする

    float attackReflesh = 1f;   //攻撃判定が残る時間
    float attackRefSave;   //↑のデータ保存用

    public bool attack;   //攻撃
    public bool hit = false;   //殴られた時のフラグ
    public bool dead = false;   //切り倒されたときのフラグ
    public bool death = false;

    Animator animator;
    Rigidbody rigid;
    EnemyAttackArea attackArea;
    PlayerControl control;
    BoxCollider coll;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        attackArea =  transform.Find("EnemyAttackArea").GetComponent<EnemyAttackArea>();
        control = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        coll = GetComponent<BoxCollider>();
        attackRefSave = attackReflesh;
    }

    void Update()
    {
        if(!dead)
        {
            MoveAni();
        }
        if(attack)
        {
            attackReflesh -= Time.deltaTime;
            if(attackReflesh <= 0)
            {
                attack = false;
                attackReflesh = attackRefSave;
            }
        }
        if (hitCount <= 0 && !death)   //一度だけ処理する
        {
            dead = true;
            death = true;
            animator.SetTrigger("Die");
            EnemyMove move = gameObject.GetComponent<EnemyMove>();
            move.enabled = false;    //コンポーネントの非有効化
            coll.isTrigger = true;
            Down();
            return;
        }
    }
    public void MoveAni()
    {
        if (rigid.IsSleeping())
        {
            animator.SetBool("Move", false);
        }
        else
        {
            animator.SetBool("Move", true);
        }
        if(attackArea.playerIn && !control.knockBack && !attack && !control.attack && !GameManager.Instance.state_damage)
        {
            animator.SetTrigger("Attack");
            attack = true;
        }
    }
    public void Hit()
    {
        if(!dead)
        {
            animator.SetTrigger("Damage");
        }
        hitCount -= control.attackPower;
        hit = true;
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.8)
        {
            rigid.velocity = Vector3.zero;
            Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
            Vector3 distination = (transform.position - playerPos).normalized;
            rigid.AddForce(distination * knockPower, ForceMode.VelocityChange);
        }
    }

    public void Down()    //切り倒されたら消える
    {
        if (dead)
        {
            Vector3 thispos = this.transform.position;
            GameObject deadEffect = Instantiate(EffectManager.Instance.StageFX[1], transform.position = new(thispos.x + 2 , thispos.y , thispos.z - 2), Quaternion.identity);  //燃えるエフェクト
            SoundManager.Instance.PlaySE_Game(5);
            Destroy(deadEffect, 2f);
            Destroy(this.gameObject, 2f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && attack && !control.invicible)
       {
            GameManager.Instance.state_damage = true;
            GameManager.Instance.Damage(1);
            Debug.Log(other.gameObject.name + "に当たった");
            GameObject HitEffect;
            HitEffect = Instantiate(EffectManager.Instance.StageFX[0], transform.position, Quaternion.identity);
            HitEffect.transform.position = other.gameObject.transform.position;
            HitEffect.transform.parent = other.gameObject.transform;
            Destroy(HitEffect, 1.0f);
            attack = false;
            return;
        }  
        if (other.gameObject.tag == "Weapon") { return; }
    }
}
