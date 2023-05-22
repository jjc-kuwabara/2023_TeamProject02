using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    //�ϓ��ł��鐔�l�@�ϐ�
    public float moveSpeed = 10;
    float moveSpeSave;
    public float JumpPower = 5;
    float jumpPowSave;
    float hor;
    public float riseTime = 1;
    public float gravity = 10;
    public int attackPower = 1;  //�U����
    public float invicibleSec = 30;  //�K�E�Z�̖��G����
    public float flameGaugeRefleshSec = 0.3f;  //�Q�[�W�̌����
    public float invicibleCurrentTimer = 0;  //�Q�[�W�̃^�C�}�[

    [Header("�񕜃A�C�e��")]
    public float itemcount;      //�񕜃A�C�e���̌�
    public float healing = 1;  //�񕜗�
    public GameObject itemtext;

    [Header("�K�E�Z�Q�[�W")]
    public float flameCharge;   //�Q�[�W�̑�����
    public GameObject FlameGauge; //�Q�[�W
    public float flameValue;   //�Q�[�W�ւ̑���p

    //����ɕK�v
    CharacterController characon;
    Animator animator;
    bool Jumpflg;
    float riseTimeTemp;
    float stateOff = 1f;

    //�ړ��n�̕ϐ�
    Vector3 moveDirection;
    Vector3 gravityDirection;

    public bool inputOK = false;  //���͉\
    public bool attack = false;  //�U���\
    public bool cantmove = false; //�����Ȃ�
    public bool knockBack = false;  //�_���[�W���[�V������
    public bool invicible = false;  //���G
    bool clearAni;
    bool overAni;

    bool left;  //������
    bool right;  //�E����

    void Start()
    {
        characon = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        flameValue = 0;
        moveSpeSave = moveSpeed;
        jumpPowSave = JumpPower;
        animator.SetTrigger("Up");
    }

    void Update()
    {
        InputCheck();
        Animation();
        Invicible();
        if (inputOK)
        {
            Movement();
            Gravity();
            Jumping();
            Healing();
            Attack();
            trun();
        }
        if (GameManager.Instance.gameClear && !clearAni)
        {
            animator.SetTrigger("Win");
            clearAni = true;
            return;
        }
        if (GameManager.Instance.gameOver && !overAni)
        {
            animator.SetTrigger("Death");
            overAni = true;
            return;
        }
        cantmoveing();
        flameValue = Mathf.Clamp(flameValue, 0, 1);   //1�𒴂��Ȃ��悤�ɐݒ�
        stateOff = Mathf.Clamp(stateOff, 0, 1);
        if(characon.isGrounded && Jumpflg)
        {
            SoundManager.Instance.PlaySE_Game(2);
        }
        if(GameManager.Instance.hpCheck && stateOff >= 0f)
        {
            stateOff -= Time.deltaTime;
            if(stateOff <= 0.3f)
            {
                GameManager.Instance.state_damage = false;
            }
            if(stateOff <= 0f)
            {
                GameManager.Instance.hpCheck = false;
                knockBack = false;
                stateOff = 1f;
            }
        } 
    }

    public void InputCheck()
    {
        if (GameManager.Instance.mainGame && !GameManager.Instance.state_damage) 
        { inputOK = true; }  //mainGame���Ȃ瑀��\
        else { inputOK = false; }
    }
    public void cantmoveing()
    {
        if (cantmove)
        {
            moveSpeed = 0;
            JumpPower = 0;
        }
        else
        {
            moveSpeed = moveSpeSave;
            JumpPower = jumpPowSave;
        }
         
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
        if(GameManager.Instance.state_damage && !knockBack)
        {
            knockBack = true;
            animator.SetTrigger("Damage");
            return;
        }
    }

    void Movement()
    {
        hor = Input.GetAxis("Horizontal");     //�����i���E�j
        SoundManager.Instance.PlaySE_Game(4);
        
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
                SoundManager.Instance.PlaySE_Game(0);
                cantmove = true;
                animator.SetTrigger("Attack");//�U���̃A�j���[�V����
                StartCoroutine("AttackOff");
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.5)
                { 
                    attack = true;
                }
            }
            else { return; }
        }
    }
    IEnumerator AttackOff()
    {
        yield return new WaitForSeconds(1f);
        attack = false;
        cantmove = false;
    }
    IEnumerator InvicibleOff()
    {
        while (invicibleCurrentTimer > 0)
        {
            flameValue = invicibleCurrentTimer / invicibleSec;
            FlameGauge.GetComponent<Image>().fillAmount = flameValue;
            yield return new WaitForSeconds(flameGaugeRefleshSec);
            invicibleCurrentTimer -= flameGaugeRefleshSec;
        }
        invicible = false;
        attackPower = 1;
        flameValue = 0f;
        FlameGauge.GetComponent<Image>().fillAmount = flameValue;
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
    public void Invicible()
    {
        if (FlameGauge.GetComponent<Image>().fillAmount == 1 && !invicible)
        {
            invicible = true;
            SoundManager.Instance.PlaySE_Game(3);
            attackPower = 3;
            invicibleCurrentTimer = invicibleSec;
            StartCoroutine("InvicibleOff");
        }
    }
    public void FlameCharge()
    {
        if (!invicible)
        { 
            flameValue += flameCharge;
            FlameGauge.GetComponent<Image>().fillAmount = flameValue;
        } 
    }

    public void FullFlame()
    {
            flameValue = 1;
            FlameGauge.GetComponent<Image>().fillAmount = 1;
    }


    public void Dead(bool flg)
    {
        characon.enabled = flg;
    }
}