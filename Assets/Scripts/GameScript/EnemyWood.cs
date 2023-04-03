using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWood : MonoBehaviour
{

    public int hitCount;   //�؂̗̑�
    public GameObject[] kafun;   //�܂��U�炷�ԕ�

    int range;    //��������ԕ��������_���ɂ���

    [SerializeField] bool hit = false;   //����ꂽ���̃t���O
    [SerializeField] bool dead = false;   //�؂�|���ꂽ�Ƃ��̃t���O

    void Start()
    {  
    }

    void Update()
    {
        if (hitCount <= 0) 
        { 
            dead = true;
            Down();
        }
        Sprinkle();
    }
    public void Hit()
    {
        hitCount--;
        hit = true;
    }

    public void Down()    //�؂�|���ꂽ�������
    {
        if(dead)
        {
            Destroy(this.gameObject,3);
        }
    }

    public void Sprinkle()    //����ꂽ��ԕ����T��
    {
        if(hit)
        {
            range = Random.Range(0, 5);
            Vector3 sprpos = this.transform.position;
            Instantiate(kafun[range], new Vector3(sprpos.x,sprpos.y,sprpos.z),Quaternion.identity);
        }
    }
}
