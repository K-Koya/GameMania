using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    public class BambooSword : PlayerWeaponBase
    {
        [SerializeField, Tooltip("攻撃力")]
        WeaponInfomation _Power = default;

        [SerializeField, Tooltip("振る間隔")]
        WeaponInfomation _SwingInterval = default;

        [SerializeField, Tooltip("振る広角")]
        WeaponInfomation _Angler = default;

        [SerializeField, Tooltip("剣の長さ")]
        WeaponInfomation _Range = default;


        /// <summary>発射間隔制御タイマー</summary>
        float _Timer = 0f;

        /// <summary>今のスイング角</summary>
        float _SwingAngle = 0f;

        /// <summary>1フレームあたりのスイング角度</summary>
        float _SwingDeltaAngle = 0f;

        /// <summary>親オブジェクトが持っているリジッドボディ</summary>
        Rigidbody _Rb = null;


        // Start is called before the first frame update
        protected override void Start()
        {
            _NumberOfWeapon = 1;

            base.Start();

            _WeaponInfomations.Add(_Power);
            _WeaponInfomations.Add(_SwingInterval);
            _WeaponInfomations.Add(_Angler);
            _WeaponInfomations.Add(_Range);

            _Rb = GetComponentInParent<Rigidbody>();

            _WeaponObjectPool.Values.First().transform.parent = transform;
            _WeaponObjectPool.Values.First().transform.localPosition = Vector3.zero;
        }

        protected override void DoAttack()
        {
            if (_WeaponObjectPool == null) return;

            //攻撃間隔を制御
            float border = 1f / _SwingInterval.NowLevelValue;
            AttackInfoMovable attackInfo = _WeaponObjectPool.Values.First();
            if (_Timer > border)
            {
                _Timer = 0f;
                if (_Rb.velocity.sqrMagnitude > 0f) transform.forward = _Rb.velocity;

                if (!attackInfo.gameObject.activeSelf)
                {
                    _SwingDeltaAngle = _Angler.NowLevelValue * 5f;
                    _SwingAngle = _Angler.NowLevelValue / 2f;
                    attackInfo.gameObject.transform.localEulerAngles = Vector3.up * _SwingAngle;
                    attackInfo.DataSet(_PlayerStatus, 0f, _Power.NowLevelValue);
                    attackInfo.transform.localScale = Vector3.one * _Range.NowLevelValue;
                    attackInfo.gameObject.SetActive(true);
                }
            }
            else if (attackInfo.gameObject.activeSelf)
            {
                _SwingAngle -= _SwingDeltaAngle * Time.deltaTime;
                attackInfo.gameObject.transform.localEulerAngles = Vector3.up * _SwingAngle;

                if (_SwingAngle < (-_Angler.NowLevelValue / 2f)) attackInfo.gameObject.SetActive(false);
            }
            else
            {
                _Timer += Time.deltaTime;
            }
        }
    }
}