using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //�ϓ��ł��鐔�l�@�ϐ�
    public float moveSpeed = 10;
    public float JumpPower = 10;
    float hor, ver;
    public float riseTime = 1;
    public float gravity = 10;


    //�悭�킩��񂯂ǕK�v�ȓz
    CharacterController characon;
    bool Jumpflg;
    float riseTimeTemp;

    //�ړ��n�̕ϐ�
    Vector3 moveDirection;
    Vector3 gravityDirection;


    void Start()
    {
        characon = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Gravity();
        Jumping();
    }

    void Movement()
    {
        hor = Input.GetAxis("Horizontal");     //�����i���E�j
        ver = Input.GetAxis("Vertical");      //�����i�㉺�j

        Debug.Log("���E=" + hor);
        Debug.Log("�㉺=" + ver);

        //�e�x�N�g���Ɂi�ړ������j�ɓ��͂̒l������
        moveDirection.x = hor * moveSpeed;
        moveDirection.z = ver * moveSpeed;

        //normalized�Ő��K�����Ă��낦��B
        moveDirection = new Vector3(moveDirection.normalized.x, 0, moveDirection.z) * moveSpeed;

        //�L�����N�^�[�̈ړ������̔��f
        characon.Move(moveDirection * Time.deltaTime);

    }

    void Jumping()
    {
        if (characon.isGrounded)
        {
            Debug.Log("�n�ʂɂ���");
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
            Debug.Log("�n�ʂɂ��Ȃ�");
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
            //Debug.Log("�n�ʂɂ����Ă��");
        }
        else
        {
            gravityDirection.y -= (gravity * Time.deltaTime);
            //Debug.Log("�󒆂ɂ����");
        }
        

    }
}