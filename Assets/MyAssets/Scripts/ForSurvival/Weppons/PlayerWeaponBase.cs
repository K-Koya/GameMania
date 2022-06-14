using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    /// <summary>����f�[�^</summary>
    abstract public class PlayerWeaponBase : MonoBehaviour
    {
        /// <summary>����f�[�^�ꗗ</summary>
        protected List<WeaponInfomation> _WeaponInfomations = new List<WeaponInfomation>();

        /// <summary>����f�[�^�ꊇ�擾</summary>
        public IReadOnlyList<WeaponInfomation> WeaponInfomations { get => _WeaponInfomations; }


        [SerializeField, Tooltip("�U����������G�̃��C���[")]
        protected LayerMask _EnemyLayer = default;

        [SerializeField, Tooltip("����v���n�u")]
        protected GameObject _Pref = null;

        [SerializeField, Tooltip("��x�ɕ\���ł��镐�퐔"), Range(0, 500)]
        protected int _NumberOfWeapon = 10;

        /// <summary>�v���C���[�̃X�e�[�^�X���</summary>
        protected StatusBase _PlayerStatus = null;

        /// <summary>�C���X�^���X����������I�u�W�F�N�g���Ǘ�</summary>
        protected ObjectPool<AttackInfoMovable> _WeaponObjectPool = default;

        /// <summary>�U�����{�s</summary>
        abstract protected void DoAttack();

        protected virtual void Start()
        {
            _PlayerStatus = GetComponentInParent<StatusBase>();

            _WeaponObjectPool = new ObjectPool<AttackInfoMovable>((uint)_NumberOfWeapon);
            for (int i = 0; i < _WeaponObjectPool.Values.Length; i++)
            {
                GameObject obj = Instantiate(_Pref);
                obj.SetActive(false);
                _WeaponObjectPool.Values[i] = obj.GetComponent<AttackInfoMovable>();
            }
        }

        void Update()
        {
            if (PauseManager.IsTimerStopped) return;
            DoAttack();
        }

        /// <summary>����̎����</summary>
        [System.Serializable]
        public struct WeaponInfomation
        {
            [Tooltip("���")]
            public string Name;

            [Tooltip("�����l")]
            public float BaseValue;

            [Tooltip("�������x��")]
            public byte Level;

            [Tooltip("�ō����x��")]
            public byte MaxLevel;

            [Tooltip("1���x���A�b�v�łǂꂾ����������邩�̔{��")]
            public float StrengthenRate;

            /// <summary>���݂̃��x���ɂ��\�͒l</summary>
            public float NowLevelValue { get => BaseValue + (BaseValue * (Level - 1) * StrengthenRate); }

            /// <summary>���݂��烌�x����1�オ�������̔\�͒l</summary>
            public float NextLevelValue { get => BaseValue + (BaseValue * (Level < MaxLevel ? Level - 1 : MaxLevel - 1) * MaxLevel); }

            /// <summary>���x���A�b�v����</summary>
            public void DoLevelUp()
            {
                if (Level >= MaxLevel) return;
                Level += 1;
            }
        }
    }
}
