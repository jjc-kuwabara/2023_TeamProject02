using UnityEngine;

public class EnemyWood : MonoBehaviour
{

    public int hitCount;   //�؂̗̑�

    int range;    //��������ԕ��������_���ɂ���

    float attackReflesh = 1f;   //�U�����肪�c�鎞��
    float attackRefSave;   //���̃f�[�^�ۑ��p

    public bool attack;   //�U��
    public bool hit = false;   //����ꂽ���̃t���O
    public bool dead = false;   //�؂�|���ꂽ�Ƃ��̃t���O
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
        if (!dead) { MoveAni(); }
       
        if(attack)
        {
            attackReflesh -= Time.deltaTime;
            if(attackReflesh <= 0)
            {
                attack = false;
                attackReflesh = attackRefSave;
            }
        }
        if (hitCount <= 0 && !death)   //��x������������
        {
            animator.SetTrigger("Die");
            EnemyMove move = gameObject.GetComponent<EnemyMove>();
            move.enabled = false;    //�R���|�[�l���g�̔�L����
            coll.isTrigger = true;
            dead = true;
            death = true;
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
        if(attackArea.playerIn && !GameManager.Instance.state_damage && !attack && !control.attack)
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

    public void Down()    //�؂�|���ꂽ�������
    {
        if (dead)
        {
            Vector3 thispos = this.transform.position;
            GameObject deadEffect = Instantiate(EffectManager.Instance.StageFX[1], transform.position = new(thispos.x + 2 , thispos.y , thispos.z - 2), Quaternion.identity);  //�R����G�t�F�N�g
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
            Debug.Log(other.gameObject.name + "�ɓ�������");
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
