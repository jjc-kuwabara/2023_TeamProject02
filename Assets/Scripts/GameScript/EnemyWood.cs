using UnityEngine;

public class EnemyWood : MonoBehaviour
{

    public int hitCount;   //�؂̗̑�
    public GameObject[] kafun;   //�܂��U�炷�ԕ�

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

    public void Down()    //�؂�|���ꂽ�������
    {
        if (dead)
        {
            animator.SetTrigger("Die");
            GameObject deadEffect = Instantiate(EffectManager.Instance.StageFX[1], transform.position, Quaternion.identity);  //�R����G�t�F�N�g
            SoundManager.Instance.PlaySE_Game(5);
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
