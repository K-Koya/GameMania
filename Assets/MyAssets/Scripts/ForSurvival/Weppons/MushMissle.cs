using UnityEngine;

namespace Survival
{
    /// <summary>きのこ専用 キノコミサイル</summary>
    public class MushMissle : PlayerWeaponBase
    {
        [SerializeField, Tooltip("攻撃力")]
        WeaponInfomation _Power = default;

        [SerializeField, Tooltip("発射間隔")]
        WeaponInfomation _Interval = default;

        [SerializeField, Tooltip("飛行速度")]
        WeaponInfomation _Speed = default;

        [SerializeField, Tooltip("大きさ")]
        WeaponInfomation _Size = default;


        /// <summary>発射間隔制御タイマー</summary>
        float _Timer = 0f;


        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            _WeaponInfomations.Add(_Power);
            _WeaponInfomations.Add(_Interval);
            _WeaponInfomations.Add(_Speed);
            _WeaponInfomations.Add(_Size);
        }

        protected override void DoAttack()
        {
            if (_WeaponObjectPool == null) return;

            //発射間隔を制御
            _Timer += Time.deltaTime;
            float border = 1f / _Interval.NowLevelValue;
            if (_Timer > border)
            {
                _Timer -= border;
                foreach (var val in _WeaponObjectPool.Values)
                {
                    if (!val.gameObject.activeSelf)
                    {
                        Vector3 dir = transform.forward;
                        RaycastHit hit;
                        if (Physics.SphereCast(transform.position + Vector3.up * 11f, 10f, Vector3.down, out hit, 11f, _EnemyLayer))
                        {
                            dir =　Vector3.ProjectOnPlane(hit.point - transform.position, Vector3.up).normalized;
                        }

                        val.gameObject.SetActive(true);
                        val.DataSet(_PlayerStatus, 0, _Power.NowLevelValue, _Speed.NowLevelValue, dir, 1, 100f);
                        val.transform.position = transform.position + Vector3.up * 0.1f;
                        val.transform.localScale = Vector3.one * _Size.NowLevelValue;
                        break;
                    }
                }
            }
        }
    }
}
