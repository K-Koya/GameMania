using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    /// <summary>����f�[�^</summary>
    abstract public class PlayerWepponBase : MonoBehaviour
    {
        /// <summary>����f�[�^�ꗗ</summary>
        protected List<WepponInfomation> _WepponInfomations = new List<WepponInfomation>();

        /// <summary>����f�[�^�ꊇ�擾</summary>
        public IReadOnlyList<WepponInfomation> WepponInfomations { get => _WepponInfomations; }

        /// <summary>�U�����{�s</summary>
        abstract protected void DoAttack();


        void Update()
        {
            if (PauseManager.IsTimerStopped) return;
            DoAttack();
        }


        /// <summary>����̎����</summary>
        [System.Serializable]
        public struct WepponInfomation
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
            public float NowLevelValue { get => BaseValue + (BaseValue * Level * StrengthenRate); }

            /// <summary>���݂��烌�x����1�オ�������̔\�͒l</summary>
            public float NextLevelValue { get => BaseValue + (BaseValue * (Level < MaxLevel ? Level : MaxLevel) * MaxLevel); }

            /// <summary>���x���A�b�v����</summary>
            public void DoLevelUp()
            {
                if (Level >= MaxLevel) return;
                Level += 1;
            }


        }
    }
}
