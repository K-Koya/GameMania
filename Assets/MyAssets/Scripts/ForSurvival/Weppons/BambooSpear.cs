using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    /// <summary>�����̂���p �|���U��</summary>
    public class BambooSpear : PlayerWeaponBase
    {
        [SerializeField, Tooltip("�U����")]
        WeaponInfomation _Power = default;

        [SerializeField, Tooltip("�ݒu����Ԋu")]
        WeaponInfomation _SetInterval = default;

        [SerializeField, Tooltip("�c������")]
        WeaponInfomation _Remaining = default;

        [SerializeField, Tooltip("���̒���")]
        WeaponInfomation _Range = default;

        /// <summary>�e�I�u�W�F�N�g�������Ă��郊�W�b�h�{�f�B</summary>
        Rigidbody _Rb = null;

        /// <summary>���ˊԊu����^�C�}�[</summary>
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

            //�ړ����̂݃^�C���J�E���g
            if (_Rb.velocity.sqrMagnitude > 1f) _Timer += Time.deltaTime;

            //�U���Ԋu�𐧌�
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
