using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    /// <summary>たけのこ専用 竹槍攻撃</summary>
    public class BambooSpear : PlayerWeaponBase
    {
        [SerializeField, Tooltip("攻撃力")]
        WeaponInfomation _Power = default;

        [SerializeField, Tooltip("設置する間隔")]
        WeaponInfomation _SetInterval = default;

        [SerializeField, Tooltip("残留時間")]
        WeaponInfomation _Remaining = default;

        [SerializeField, Tooltip("槍の長さ")]
        WeaponInfomation _Range = default;

        /// <summary>親オブジェクトが持っているリジッドボディ</summary>
        Rigidbody _Rb = null;

        /// <summary>発射間隔制御タイマー</summary>
        float _Timer = 0f;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            _WeaponInfomations.Add(_Power);
            _WeaponInfomations.Add(_SetInterval);
            _WeaponInfomations.Add(_Remaining);
            _WeaponInfomations.Add(_Range);

            _Rb = GetComponentInParent<Rigidbody>();
        }


        protected override void DoAttack()
        {
            if (_WeaponObjectPool == null) return;

            //移動中のみタイムカウント
            if (_Rb.velocity.sqrMagnitude > 1f) _Timer += Time.deltaTime;

            //攻撃間隔を制御
            float border = 1f / _SetInterval.NowLevelValue;
            if (_Timer > border)
            {
                _Timer -= border;
                foreach (var val in _WeaponObjectPool.Values)
                {
                    if (!val.gameObject.activeSelf)
                    {
                        val.gameObject.SetActive(true);
                        val.DataSet(_PlayerStatus, 0, _Power.NowLevelValue, 0, transform.forward, byte.MaxValue, _Remaining.NowLevelValue);
                        val.transform.position = transform.position + Vector3.up * 0.1f;
                        val.transform.localScale = Vector3.one * _Range.NowLevelValue;
                        break;
                    }
                }

            }
        }
    }
}
