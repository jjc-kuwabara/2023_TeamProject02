using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //変動できる数値　変数
    public float moveSpeed = 10;
    public float JumpPower = 10;
    float hor;
    public float riseTime = 1;
    public float gravity = 10;

    [Header("回復アイテム")]
    public float itemcount;      //回復アイテムの個数
    public float healing;  //回復量

    //よくわからんけど必要な奴
    CharacterController characon;
    Animator animator;
    bool Jumpflg;
    float riseTimeTemp;

    //移動系の変数
    Vector3 moveDirection;
    Vector3 gravityDirection;

    public bool inputOK = false;  //入力可能
    public bool attack = false;  //攻撃可能
    public bool cantmove = false; //動けないよ

    bool left;  //左向き
    bool right;  //右向き

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
        cantmoveing();
    }

    public void InputCheck()
    {
        if (GameManager.Instance.mainGame) { inputOK = true; }  //mainGame中なら操作可能
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
            moveSpeed = 10;
            JumpPower = 5;
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

        
    }

    void Movement()
    {
        hor = Input.GetAxis("Horizontal");     //水平（左右）

        /*Debug.Log("左右=" + hor);
        Debug.Log("上下=" + ver);*/

        //各ベクトルに（移動方向）に入力の値を入れる
        moveDirection.x = hor * moveSpeed;
        /*moveDirection.z = ver * moveSpeed;*/

        //normalizedで正規化してそろえる。
        moveDirection = new Vector3(moveDirection.normalized.x, 0, moveDirection.normalized.z) * moveSpeed;

        //キャラクターの移動処理の反映
        characon.Move(moveDirection * Time.deltaTime);

    }

    void Jumping()
    {
        if (characon.isGrounded)
        {
            //Debug.Log("地面にいる");
            //地面に接地している時のみ入力可能
            if (Input.GetButtonDown("Jump"))
            {
                Jumpflg = true;
                gravityDirection.y = JumpPower;
                Debug.Log("ジャンプしてるよ");
            }
        }
        else
        {
            //Debug.Log("地面にいない");
            if (Jumpflg && Input.GetButtonUp("Jump") || riseTimeTemp > riseTime)
            {
                Jumpflg = false;
            }
            //ボタンを押しっぱなしだったら、時間を計測して
            //一定時間まで上昇し続ける
            if (Jumpflg && Input.GetButton("Jump") && riseTimeTemp <= riseTime)
            {
                riseTimeTemp += Time.deltaTime;
                gravityDirection.y = JumpPower;
                //ジャンプが押してる時間が指定された時間より長くなってもそれ以上ジャンプしない
            }
        }

        //Move関数を使ってY方向に落ちる
        characon.Move(gravityDirection * Time.deltaTime);
    }
    void Gravity()
    {

        gravityDirection.y -= gravity * Time.deltaTime;

        //Move関数を使ってY方向に落ちる
        characon.Move(gravityDirection * Time.deltaTime);

        //もしも地面に接触したら
        if (characon.isGrounded)
        {
            riseTimeTemp = 0;
            Jumpflg = false;
            gravityDirection.y = -0.1f;
            //Debug.Log("地面にいるよ");

        }
        else
        {
            //Debug.Log("空中にいるよ");
        }

        //characterControllerのisGrrounded関数で
        //地面に立っているかどうかを判定してくれる
        //地面に接地している時は重力は働かないようにする


        if (characon.isGrounded && gravityDirection.y < 0)
        {
            gravityDirection.y = -0.5f;
            //Debug.Log("地面");
        }
        else
        {
            gravityDirection.y -= (gravity * Time.deltaTime);
            //Debug.Log("空中");
        }


    }

    public void Healing()  //回復
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

    public void Attack()  //攻撃
    {
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (!attack)
            {
                attack = true;
                cantmove = true;
                animator.SetTrigger("Attack");//攻撃のアニメーション
                StartCoroutine("AttackOff");

            }
            else
            { return; }
        }
    }
    IEnumerator AttackOff()
    {
        yield return new WaitForSeconds(1f);
        attack = false;
        cantmove = false;
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
                myTransform.eulerAngles = rote;  //右向き
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
                myTransform.eulerAngles = rote;  //左向き
                return;
            }
        }
    } 
  
    public void Dead(bool flg)
    {
        characon.enabled = flg;
    }
}