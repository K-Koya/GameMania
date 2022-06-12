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

        private void OnTriggerEnter(Collider other)
        {
            //プレイヤーの攻撃に接触するとダメージ
            if (other.gameObject.layer == LayerManager.PlayerAttack)
            {
                AttackInfo attack = other.GetComponent<AttackInfo>();

                _Life -= attack.PowerOnEnter;
                if (_Life < 0)
                {
                    gameObject.SetActive(false);
                    WaveEnemyManager.DefeatedEnemyCount++;
                    PlayerStatus pSta = attack.Status as PlayerStatus;
                    pSta.AddExp(_GiveExp);
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

                _Life -= attack.PowerOnStay;
                if (_Life < 0)
                {
                    gameObject.SetActive(false);
                    WaveEnemyManager.DefeatedEnemyCount++;
                    PlayerStatus pSta = attack.Status as PlayerStatus;
                    pSta.AddExp(_GiveExp);
                }
            }
        }
    }
}
