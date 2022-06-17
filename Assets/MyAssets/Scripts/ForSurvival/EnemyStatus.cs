using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    /// <summary>敵の攻撃や体力処理</summary>
    public class EnemyStatus : StatusBase
    {
        [SerializeField, Tooltip("倒したときにプレイヤーに与えられる経験値量")]
        short _GiveExp = 1;

        /// <summary>プレイヤーのリジッドボディ</summary>
        Rigidbody _Rb = null;

        void Start()
        {
            _Rb = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            //プレイヤーの攻撃に接触するとダメージ
            if (other.gameObject.layer == LayerManager.PlayerAttack)
            {
                AttackInfo attack = other.GetComponent<AttackInfo>();

                if (attack)
                {
                    _Life -= attack.PowerOnEnter;
                    if (_Life < 0)
                    {
                        gameObject.SetActive(false);
                        WaveEnemyManager.DefeatedEnemyCount++;
                        PlayerStatus pSta = attack.Status as PlayerStatus;
                        pSta.AddExp(_GiveExp);
                    }
                    else
                    {
                        Vector3 pow = (transform.position - other.transform.position) * 5f;
                        _Rb.AddForce(pow, ForceMode.Impulse);
                    }
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (PauseManager.IsTimerStopped) return;

            //プレイヤーの攻撃に接触し続ける間ダメージ
            if (other.gameObject.layer == LayerManager.PlayerAttack)
            {
                AttackInfo attack = other.GetComponent<AttackInfo>();

                if (attack)
                {
                    _Life -= attack.PowerOnStay;
                    if (_Life < 0)
                    {
                        gameObject.SetActive(false);
                        WaveEnemyManager.DefeatedEnemyCount++;
                        PlayerStatus pSta = attack.Status as PlayerStatus;
                        pSta.AddExp(_GiveExp);
                    }
                    else
                    {
                        Vector3 pow = transform.position - other.transform.position;
                       _Rb.AddForce(pow);
                    }
                }
            }
        }
    }
}
