using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    /// <summary>�G�̍U����̗͏���</summary>
    public class EnemyStatus : StatusBase
    {
        [SerializeField, Tooltip("�|�����Ƃ��Ƀv���C���[�ɗ^������o���l��")]
        short _GiveExp = 1;

        private void OnTriggerEnter(Collider other)
        {
            //�v���C���[�̍U���ɐڐG����ƃ_���[�W
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

            //�v���C���[�̍U���ɐڐG��������ԃ_���[�W
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
