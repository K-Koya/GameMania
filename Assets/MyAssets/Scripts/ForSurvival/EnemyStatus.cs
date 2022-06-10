using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    /// <summary>�G�̍U����̗͏���</summary>
    public class EnemyStatus : MonoBehaviour
    {
        [SerializeField, Tooltip("�̗�")]
        float _Life = 10f;

        [SerializeField, Tooltip("�|�����Ƃ��Ƀv���C���[�ɗ^������o���l��")]
        short _GiveExp = 1;

        [SerializeField, Tooltip("���C���[�� : �v���C���[")]
        string _LayerNamePlayer = "Player";

        [SerializeField, Tooltip("�^�O�� : �v���C���[�̍U��")]
        string _TagNamePlayerAttack = "PlayerAttack";

        /// <summary>���C���[�ԍ� : �v���C���[</summary>
        int _LayerIndexPlayer = -1;

        // Start is called before the first frame update
        void Start()
        {
            _LayerIndexPlayer = LayerMask.NameToLayer(_LayerNamePlayer);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            //�v���C���[�̍U���ɐڐG����ƃ_���[�W
            if (collision.gameObject.layer == _LayerIndexPlayer && collision.gameObject.CompareTag(_TagNamePlayerAttack))
            {
                EnemyAttack attack = collision.gameObject.GetComponent<EnemyAttack>();
                _Life -= attack.PowerOnEnter;
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (PauseManager.IsTimerStopped) return;

            //�v���C���[�̍U���ɐڐG��������ԃ_���[�W
            if (collision.gameObject.layer == _LayerIndexPlayer && collision.gameObject.CompareTag(_TagNamePlayerAttack))
            {
                EnemyAttack attack = collision.gameObject.GetComponent<EnemyAttack>();
                _Life -= attack.PowerOnStay;
            }
        }
    }
}
