using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    /// <summary>武器データ</summary>
    abstract public class PlayerWeaponBase : MonoBehaviour
    {
        /// <summary>武器データ一覧</summary>
        protected List<WeaponInfomation> _WeaponInfomations = new List<WeaponInfomation>();

        /// <summary>武器データ一括取得</summary>
        public IReadOnlyList<WeaponInfomation> WeaponInfomations { get => _WeaponInfomations; }


        [SerializeField, Tooltip("攻撃が当たる敵のレイヤー")]
        protected LayerMask _EnemyLayer = default;

        [SerializeField, Tooltip("武器プレハブ")]
        protected GameObject _Pref = null;

        [SerializeField, Tooltip("一度に表示できる武器数"), Range(0, 500)]
        protected int _NumberOfWeapon = 10;

        /// <summary>プレイヤーのステータス情報</summary>
        protected StatusBase _PlayerStatus = null;

        /// <summary>インスタンス化した武器オブジェクトを管理</summary>
        protected ObjectPool<AttackInfoMovable> _WeaponObjectPool = default;

        /// <summary>攻撃を施行</summary>
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

        /// <summary>武器の持つ情報</summary>
        [System.Serializable]
        public struct WeaponInfomation
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
            public float NowLevelValue { get => BaseValue + (BaseValue * (Level - 1) * StrengthenRate); }

            /// <summary>現在からレベルが1上がった時の能力値</summary>
            public float NextLevelValue { get => BaseValue + (BaseValue * (Level < MaxLevel ? Level - 1 : MaxLevel - 1) * MaxLevel); }

            /// <summary>レベルアップ処理</summary>
            public void DoLevelUp()
            {
                if (Level >= MaxLevel) return;
                Level += 1;
            }
        }
    }
}
