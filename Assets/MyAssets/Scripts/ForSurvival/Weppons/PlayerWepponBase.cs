using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    /// <summary>武器データ</summary>
    abstract public class PlayerWepponBase : MonoBehaviour
    {
        /// <summary>武器データ一覧</summary>
        protected List<WepponInfomation> _WepponInfomations = new List<WepponInfomation>();

        /// <summary>武器データ一括取得</summary>
        public IReadOnlyList<WepponInfomation> WepponInfomations { get => _WepponInfomations; }

        /// <summary>攻撃を施行</summary>
        abstract protected void DoAttack();


        void Update()
        {
            if (PauseManager.IsTimerStopped) return;
            DoAttack();
        }


        /// <summary>武器の持つ情報</summary>
        [System.Serializable]
        public struct WepponInfomation
        {
            [Tooltip("情報名")]
            public string Name;

            [Tooltip("実数値")]
            public float BaseValue;

            [Tooltip("強化レベル")]
            public byte Level;

            [Tooltip("最高レベル")]
            public byte MaxLevel;

            [Tooltip("1レベルアップでどれだけ強化されるかの倍率")]
            public float StrengthenRate;

            /// <summary>現在のレベルによる能力値</summary>
            public float NowLevelValue { get => BaseValue + (BaseValue * Level * StrengthenRate); }

            /// <summary>現在からレベルが1上がった時の能力値</summary>
            public float NextLevelValue { get => BaseValue + (BaseValue * (Level < MaxLevel ? Level : MaxLevel) * MaxLevel); }

            /// <summary>レベルアップ処理</summary>
            public void DoLevelUp()
            {
                if (Level >= MaxLevel) return;
                Level += 1;
            }


        }
    }
}
