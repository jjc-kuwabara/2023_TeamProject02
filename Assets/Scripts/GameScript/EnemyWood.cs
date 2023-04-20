using UnityEngine;

public class EnemyWood : MonoBehaviour
{

    public int hitCount;   //�؂̗̑�
    public GameObject[] kafun;   //�܂��U�炷�ԕ�

    int range;    //��������ԕ��������_���ɂ���

    bool attack;
    public bool hit = false;   //����ꂽ���̃t���O
    public bool dead = false;   //�؂�|���ꂽ�Ƃ��̃t���O
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
        if (hitCount <= 0 && !death)   //��x������������
        {
            EnemyMove move = gameObject.GetComponent<EnemyMove>();
            move.enabled = false;    //�R���|�[�l���g�̔�L����
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
        if(attackArea.playerIn)
        {
            animator.SetTrigger("Attack");
            attack = true;
        }
    }
    public void Hit()
    {
        animator.SetTrigger("Damage");
        hitCount--;
        hit = true;
    }

    public void Down()    //�؂�|���ꂽ�������
    {
        if (dead)
        {
            animator.SetTrigger("Die");
            GameObject deadEffect = Instantiate(EffectManager.Instance.StageFX[1], transform.position, Quaternion.identity);  //�R����G�t�F�N�g
            Destroy(deadEffect, 3);
            Destroy(this.gameObject, 3);
        }
    }

    public void Sprinkle()    //����ꂽ��ԕ����T��
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
        if (other.gameObject.tag == "Player" && attack)
       {
            GameManager.Instance.state_damage = true;
            GameManager.Instance.Damage(10);
            Debug.Log(other.gameObject.name + "�ɓ���������");
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
