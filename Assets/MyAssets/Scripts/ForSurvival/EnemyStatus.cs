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

        /// <summary>�G�̃��W�b�h�{�f�B</summary>
        Rigidbody _Rb = null;

        void Start()
        {
            _Rb = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            //�v���C���[�̍U���ɐڐG����ƃ_���[�W
            if (other.gameObject.layer == LayerManager.PlayerAttack)
            {
                AttackInfo attack = other.GetComponent<AttackInfo>();

                if (attack)
                {
                    _Life -= attack.PowerOnEnter;
                    if (_Life < 0)
                    {
                        gameObject.SetActive(false);
                        ParticleManager.Emit((int)ParticleManager.Kind.EnemyDefeated, transform, transform.localScale);
                        WaveEnemyManager.DefeatedEnemyCount++;
                        PlayerStatus pSta = attack.Status as PlayerStatus;
                        pSta.AddExp(_GiveExp);
                    }
                    else
                    {
                        ParticleManager.Emit((int)ParticleManager.Kind.EnemyDamaged, transform, transform.localScale);
                        Vector3 pow = (transform.position - other.transform.position) * 5f;
                        _Rb.AddForce(pow, ForceMode.Impulse);
                    }

                    SEManager.Emit((int)SEManager.Kind.EnemyDamaged, transform);
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

                if (attack)
                {
                    _Life -= attack.PowerOnStay;
                    if (_Life < 0)
                    {
                        ParticleManager.Emit((int)ParticleManager.Kind.EnemyDefeated, transform, transform.localScale);
                        SEManager.Emit((int)SEManager.Kind.EnemyDamaged, transform);
                        gameObject.SetActive(false);
                        WaveEnemyManager.DefeatedEnemyCount++;
                        PlayerStatus pSta = attack.Status as PlayerStatus;
                        pSta.AddExp(_GiveExp);
                    }
                    else
                    {
                        ParticleManager.Emit((int)ParticleManager.Kind.EnemyDamaged, transform, transform.localScale);
                        Vector3 pow = transform.position - other.transform.position;
                       _Rb.AddForce(pow);
                    }
                }
            }
        }
    }
}
