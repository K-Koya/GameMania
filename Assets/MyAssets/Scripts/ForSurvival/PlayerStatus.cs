using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    /// <summary>�v���C���[�̍U����̗͏���</summary>
    public class PlayerStatus : StatusBase
    {
        #region �����o
        /// <summary>�v���C���[���痣��Ă��č폜�ΏۂɂȂ鋗��/summary>
        public const float FAR_POINT = 26f;

        /// <summary>�v���C���[�ʒu</summary>
        public static Transform Transform = default;

        [SerializeField, Tooltip("�v���C���[�̌��݃��x��")]
        short _Level = 1;

        [SerializeField, Tooltip("�̗͍ő�l")]
        float _MaxLife = 100f;

        [SerializeField, Tooltip("�擾�o���l��")]
        int _Exp = 0;

        [SerializeField, Tooltip("���̃��x���A�b�v�܂łɕK�v�Ȍo���l��")]
        int _NextLevelExp = 6;
        #endregion

        #region �v���p�e�B
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
            Transform = transform;
        }

        // Update is called once per frame
        void Update()
        {
            //���x���A�b�v����
            if(_Exp >= _NextLevelExp)
            {
                _Level++;
                _Exp -= _NextLevelExp;
                _NextLevelExp += _NextLevelExp / 2;
            }
        }

        void OnDestroy()
        {
            Transform = null;
        }

        private void OnCollisionEnter(Collision collision)
        {
            //�G�ɐڐG����ƃ_���[�W
            if (collision.gameObject.layer == LayerManager.Enemy)
            {
                AttackInfo attack = collision.gameObject.GetComponent<AttackInfo>();
                _Life -= attack.PowerOnEnter;
                if(_Life < 0)
                {
                    gameObject.SetActive(false);
                }
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (PauseManager.IsTimerStopped) return;

            //�G�ɐڐG��������ԃ_���[�W
            if (collision.gameObject.layer == LayerManager.Enemy)
            {
                AttackInfo attack = collision.gameObject.GetComponent<AttackInfo>();
                _Life -= attack.PowerOnStay;
                if (_Life < 0)
                {
                    gameObject.SetActive(false);
                }
            }
        }

        /// <summary>�o���l�����Z</summary>
        /// <param name="exp">�|�����G�����o���l</param>
        public void AddExp(short exp)
        {
            _Exp += exp;
        }
    }
}
