using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    /// <summary>�v���C���[�̍U����̗͏���</summary>
    public class PlayerStatus : MonoBehaviour
    {
        #region �����o
        [SerializeField, Tooltip("�v���C���[�̌��݃��x��")]
        short _Level = 1;

        [SerializeField, Tooltip("�̗�")]
        float _Life = 100f;

        [SerializeField, Tooltip("�̗͍ő�l")]
        float _MaxLife = 100f;

        [SerializeField, Tooltip("�擾�o���l��")]
        int _Exp = 0;

        [SerializeField, Tooltip("���̃��x���A�b�v�܂łɕK�v�Ȍo���l��")]
        int _NextLevelExp = 6;

        [SerializeField, Tooltip("���C���[�� : �G")]
        string _LayerNameEnemy = "Enemy";

        /// <summary>���C���[�ԍ� : �G</summary>
        int _LayerIndexEnemy = -1;
        #endregion

        #region �v���p�e�B
        /// <summary>�̗�</summary>
        public float Life { get => _Life; }
        /// <summary>�̗͍ő�l</summary>
        public float MaxLife { get => _MaxLife; }
        /// <summary>�擾�o���l��</summary>
        public int Exp { get => _Exp; }
        /// <summary>���̃��x���A�b�v�܂łɕK�v�Ȍo���l��</summary>
        public int NextLevelExp { get => _NextLevelExp; }
        /// <summary>�v���C���[�̌��݃��x��</summary>
        public short Level { get => _Level; }
        #endregion


        // Start is called before the first frame update
        void Start()
        {
            _LayerIndexEnemy = LayerMask.NameToLayer(_LayerNameEnemy);
        }

        // Update is called once per frame
        void Update()
        {
            //���x���A�b�v����
            if(_Exp >= _NextLevelExp)
            {
                _Level++;
                _NextLevelExp += _NextLevelExp / 2;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            //�G�ɐڐG����ƃ_���[�W
            if (collision.gameObject.layer == _LayerIndexEnemy)
            {
                EnemyAttack attack = collision.gameObject.GetComponent<EnemyAttack>();
                _Life -= attack.PowerOnEnter;
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (PauseManager.IsTimerStopped) return;

            //�G�ɐڐG��������ԃ_���[�W
            if (collision.gameObject.layer == _LayerIndexEnemy)
            {
                EnemyAttack attack = collision.gameObject.GetComponent<EnemyAttack>();
                _Life -= attack.PowerOnStay;
            }
        }
    }
}
