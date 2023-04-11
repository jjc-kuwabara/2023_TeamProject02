using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyMove : MonoBehaviour
{
    // 移動の方法
    enum MoveType
    {
        なし,
        プレイヤーを追跡,
        ルートを巡回,
        巡回しつつ追跡,
            プレイヤーから逃げる,
    }

    // 移動方法をエディターに表示
    // 表示したいが、他のところからアクセスさせたくない
    [SerializeField] private MoveType type;
    // public MoveType type;
    // 他からアクセスしたいが、表示はしたくない
    // [System.NonSerialized] public MoveType type;
    //                        ↳ 無くてもいい

    // 追跡したい対象
    GameObject target;

    // ルート移動用のポイント設定
    [SerializeField] GameObject[] routePoint;
    int nextPoint;

    // NavMeshAgentコンポーネント用
    NavMeshAgent nav;

    // 索敵をしている子オブジェクト
    // [SerializeField] GameObject searchObject;
    EnemySearch seach;

    void Start()
    {
        // 追跡したい対象をTagから検索
        target = GameObject.FindGameObjectWithTag("Player");

        // コンポーネントを取得
        nav = GetComponent<NavMeshAgent>();

        // 自分の子を探す処理
        seach = transform.GetChild(0).GetComponent<EnemySearch>();
        // Transform mySon = transform.GetChild(1);
        // search = searchObject.GetComponent<EnemySearch>();
    }
void Update()
    {
        // ゲームごとの移動法を実行
        switch (type)
        {
            case MoveType.なし:
                break;

            case MoveType.プレイヤーを追跡:
                PlayerChase();
                break;

            case MoveType.ルートを巡回:
                routePatrol();
                break;

            case MoveType.巡回しつつ追跡:
                PatrolAndChase();
                break;

            case MoveType.プレイヤーから逃げる:
                PlayerRun();
                break;
        }
    }
    void PlayerChase()
    {
        // ターゲットが存在している時のみ
        if (target != null)
        {
            if (seach.playerON)
            {
                // プレイヤーがダメージ中じゃなければ
                if (!GameManager.Instance.state_damage)
                {
                    // 行き先を設定(行き先のポジション)
                    nav.SetDestination(target.transform.position);

                }

            }
            else
            {
                nav.SetDestination(transform.position);
            }
            return;

        }
        Debug.LogWarning("ターゲットが存在していない");

    }

    // ルート移動の処理
    void routePatrol()
    {
        // 目標地点に近づいたら次の拠点を設定
        if (nav.pathPending == false && nav.remainingDistance <= 0.1f)
        {
            // 次の地点のポイントを目標に
            nav.destination = routePoint[nextPoint].transform.position;

            // 配列の次の値を設定、次がなければ０に戻る
            nextPoint = (nextPoint + 1) % routePoint.Length;
        }
    }
        //巡回しつつ追跡もする
        void PatrolAndChase()
        {
            //索敵範囲内に侵入したら追跡
            if (seach.playerON)
            {
                PlayerChase();

            }
            else
            {
                //それ以外の時は巡回
                routePatrol();

            }
        }

    //
void PlayerRun()
    {
        if (target != null)
        {
            if (seach.playerON)
            {
                //逃げ先のポジションを決める
                Vector3 dir = transform.position - target.transform.position;

                //行き先を設定
                nav.SetDestination(transform.position + dir * 0.3f);
            }
            else
            {
                nav.SetDestination(transform.position);
            }
        }
    }
}