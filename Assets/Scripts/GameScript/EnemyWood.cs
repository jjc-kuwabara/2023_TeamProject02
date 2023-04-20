using UnityEngine;

public class EnemyWood : MonoBehaviour
{

    public int hitCount;   //木の体力
    public GameObject[] kafun;   //まき散らす花粉

    int range;    //生成する花粉をランダムにする

    bool attack;
    public bool hit = false;   //殴られた時のフラグ
    public bool dead = false;   //切り倒されたときのフラグ
    public bool death = false;

    Animator animator;
    Rigidbody rigid;
    EnemyAttackArea attackArea;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        attackArea =  transform.Find("EnemyAttackArea").GetComponent<EnemyAttackArea>();
    }

    void Update()
    {
        MoveAni();
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
    }
    public void Hit()
    {
        animator.SetTrigger("Damage");
        hitCount--;
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
            Instantiate(kafun[range], new Vector3(sprpos.x,sprpos.y,sprpos.z),Quaternion.identity);
            hit = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        animator.SetTrigger("Attack");
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
        if (other.gameObject.tag == "Weapon") { return; }
    }
}
