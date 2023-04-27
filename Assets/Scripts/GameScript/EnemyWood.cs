using UnityEngine;

public class EnemyWood : MonoBehaviour
{

    public int hitCount;   //木の体力
    public GameObject[] kafun;   //まき散らす花粉

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

    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        attackArea =  transform.Find("EnemyAttackArea").GetComponent<EnemyAttackArea>();
        control = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        attackRefSave = attackReflesh;
    }

    void Update()
    {
        MoveAni();
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
            EnemyMove move = gameObject.GetComponent<EnemyMove>();
            move.enabled = false;    //コンポーネントの非有効化
            dead = true;
            death = true;
            Down();
        }
        //Sprinkle();
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
        if(attackArea.playerIn && !GameManager.Instance.state_damage && !attack)
        {
            animator.SetTrigger("Attack");
            attack = true;
        }
    }
    public void Hit()
    {
        animator.SetTrigger("Damage");
        hitCount -= control.attackPower;
        hit = true;
    }

    public void Down()    //切り倒されたら消える
    {
        if (dead)
        {
            animator.SetTrigger("Die");
            GameObject deadEffect = Instantiate(EffectManager.Instance.StageFX[1], transform.position, Quaternion.identity);  //燃えるエフェクト
            Destroy(deadEffect, 3);
            Destroy(this.gameObject, 3);
        }
    }

    public void Sprinkle()    //殴られたら花粉を撒く
    {
        if(hit)
        {
            range = Random.Range(0, 5);
            Vector3 sprpos = this.transform.position;
            float pos = Random.Range(-4, 5);
            Instantiate(kafun[range], new Vector3(sprpos.x + pos, sprpos.y,sprpos.z),Quaternion.identity);
            hit = false;
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
