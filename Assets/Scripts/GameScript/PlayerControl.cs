using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //�ϓ��ł��鐔�l�@�ϐ�
    public float moveSpeed = 10;
    public float JumpPower = 10;
    float hor;
    public float riseTime = 1;
    public float gravity = 10;

    [Header("�񕜃A�C�e��")]
    public float itemcount;      //�񕜃A�C�e���̌�
    public float healing;  //�񕜗�

    //�悭�킩��񂯂ǕK�v�ȓz
    CharacterController characon;
    Animator animator;
    bool Jumpflg;
    float riseTimeTemp;

    //�ړ��n�̕ϐ�
    Vector3 moveDirection;
    Vector3 gravityDirection;

    public bool inputOK = false;  //���͉\
    public bool attack = false;  //�U���\

    bool left;  //������
    bool right;  //�E����

    void Start()
    {
        characon = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        InputCheck();
        Animation();
        if (inputOK)
        {
            Movement();
            Gravity();
            Jumping();
            Healing();
            Attack();
        }
        trun();
    }

    public void InputCheck()
    {
        if (GameManager.Instance.mainGame) { inputOK = true; }  //mainGame���Ȃ瑀��\
        else { inputOK = false; }
    }
    public void Animation()
    {
        if(Jumpflg)
        {
            animator.SetBool("Jump", true);
            return;
        }
        animator.SetBool("Jump", false);
        if(hor != 0)
        {
            animator.SetBool("Run", true);
            return;
        }
        animator.SetBool("Run", false);

        if(attack)
        {
            animator.SetTrigger("Attack");
            StartCoroutine("AttackOff");
        }
    }

    void Movement()
    {
        hor = Input.GetAxis("Horizontal");     //�����i���E�j

        /*Debug.Log("���E=" + hor);
        Debug.Log("�㉺=" + ver);*/

        //�e�x�N�g���Ɂi�ړ������j�ɓ��͂̒l������
        moveDirection.x = hor * moveSpeed;
        /*moveDirection.z = ver * moveSpeed;*/

        //normalized�Ő��K�����Ă��낦��B
        moveDirection = new Vector3(moveDirection.normalized.x, 0, moveDirection.normalized.z) * moveSpeed;

        //�L�����N�^�[�̈ړ������̔��f
        characon.Move(moveDirection * Time.deltaTime);

    }

    void Jumping()
    {
        if (characon.isGrounded)
        {
            //Debug.Log("�n�ʂɂ���");
            //�n�ʂɐڒn���Ă��鎞�̂ݓ��͉\
            if (Input.GetButtonDown("Jump"))
            {
                Jumpflg = true;
                gravityDirection.y = JumpPower;
                Debug.Log("�W�����v���Ă��");
            }
        }
        else
        {
            //Debug.Log("�n�ʂɂ��Ȃ�");
            if (Jumpflg && Input.GetButtonUp("Jump") || riseTimeTemp > riseTime)
            {
                Jumpflg = false;
            }
            //�{�^�����������ςȂ���������A���Ԃ��v������
            //��莞�Ԃ܂ŏ㏸��������
            if (Jumpflg && Input.GetButton("Jump") && riseTimeTemp <= riseTime)
            {
                riseTimeTemp += Time.deltaTime;
                gravityDirection.y = JumpPower;
                //�W�����v�������Ă鎞�Ԃ��w�肳�ꂽ���Ԃ�蒷���Ȃ��Ă�����ȏ�W�����v���Ȃ�
            }
        }

        //Move�֐����g����Y�����ɗ�����
        characon.Move(gravityDirection * Time.deltaTime);
    }
    void Gravity()
    {

        gravityDirection.y -= gravity * Time.deltaTime;

        //Move�֐����g����Y�����ɗ�����
        characon.Move(gravityDirection * Time.deltaTime);

        //�������n�ʂɐڐG������
        if (characon.isGrounded)
        {
            riseTimeTemp = 0;
            Jumpflg = false;
            gravityDirection.y = -0.1f;
            //Debug.Log("�n�ʂɂ����");

        }
        else
        {
            //Debug.Log("�󒆂ɂ����");
        }

        //characterController��isGrrounded�֐���
        //�n�ʂɗ����Ă��邩�ǂ����𔻒肵�Ă����
        //�n�ʂɐڒn���Ă��鎞�͏d�͓͂����Ȃ��悤�ɂ���


        if (characon.isGrounded && gravityDirection.y < 0)
        {
            gravityDirection.y = -0.5f;
            //Debug.Log("�n��");
        }
        else
        {
            gravityDirection.y -= (gravity * Time.deltaTime);
            //Debug.Log("��");
        }


    }

    public void Healing()  //��
    {
        if (Input.GetKeyDown(KeyCode.L) && itemcount >= 1)
            if (GameManager.Instance.hpFull != true)
            {
                {
                    GameManager.Instance.Heal(healing);
                    itemcount--;
                }
            }
    }

    public void Attack()  //�U��
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (!attack)
            {
                attack = true;
            }
            else { return; }
        }
    }
    IEnumerator AttackOff()
    {
        yield return new WaitForSeconds(0.5f);
        attack = false;
    }

    public void trun()
    {
        if (Input.GetKeyDown(KeyCode.D)) 
        {
          if(!right)
            {
                right = true;
                left = false;
                Transform myTransform = this.transform;
                Vector3 rote = myTransform.localEulerAngles;
                rote.y = 90f;
                myTransform.eulerAngles = rote;  //�E����
                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!left)
            {
                right = false;
                left = true;
                Transform myTransform = this.transform;
                Vector3 rote = myTransform.localEulerAngles;
                rote.y = -90f;
                myTransform.eulerAngles = rote;  //������
                return;
            }
        }
    } 
  
    public void Dead(bool flg)
    {
        characon.enabled = flg;
    }
}