using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    /// <summary>���̂���p �E�q���e</summary>
    public class SporeBomb : PlayerWeaponBase
    {
        [SerializeField, Tooltip("�U����")]
        WeaponInfomation _Power = default;

        [SerializeField, Tooltip("���ː�")]
        WeaponInfomation _NumberOfEmittion = default;
                
        [SerializeField, Tooltip("�傫��")]
        WeaponInfomation _Size = default;

        [SerializeField, Tooltip("�c������")]
        WeaponInfomation _Remaining = default;


        [SerializeField, Tooltip("���e�̎ˏo�͂̍ő�l(x������, z������)")]
        Vector2 _EmitPowerMax = default;

        /// <summary>�ˏo����I�u�W�F�N�g�̃��W�b�h�{�f�B</summary>
        Rigidbody[] _EmitRbs = null;
        

        /// <summary>���ˊԊu����^�C�}�[</summary>
        float _Timer = 0f;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            _WeaponInfomations.Add(_Power);
            _WeaponInfomations.Add(_NumberOfEmittion);
            _WeaponInfomations.Add(_Size);
            _WeaponInfomations.Add(_Remaining);

            _EmitRbs = new Rigidbody[_NumberOfWeapon];
            for(int i = 0; i < _NumberOfWeapon; i++)
            {
                _EmitRbs[i] = _WeaponObjectPool.Values[i].gameObject.GetComponent<Rigidbody>(); ;
            }

            gameObject.SetActive(false);
        }


        protected override void DoAttack()
        {
            //���ˊԊu�𐧌�
            _Timer += Time.deltaTime;
            float border = 2.5f;
            if (_Timer > border)
            {
                _Timer -= border;

                for (int i = 0; i < _NumberOfEmittion.NowLevelValue; i++)
                {
                    for (int k = 0; k < _WeaponObjectPool.Values.Length; k++)
                    {
                        AttackInfoMovable val = _WeaponObjectPool.Values[k];

                        if (!val.gameObject.activeSelf)
                        {
                            val.gameObject.SetActive(true);

                            Vector3 emitPower = new Vector3(Random.Range(-_EmitPowerMax.x, _EmitPowerMax.x)
                                                                , 5f
                                                                , Random.Range(-_EmitPowerMax.y, _EmitPowerMax.y));
                            _EmitRbs[k].AddForce(emitPower, ForceMode.VelocityChange);

                            val.DataSet(_PlayerStatus, _Power.NowLevelValue, 0, 0, Vector3.zero, byte.MaxValue, _Remaining.NowLevelValue);
                            val.transform.position = transform.position + Vector3.up * 5f;
                            val.transform.localScale = Vector3.one * _Size.NowLevelValue;
                            break;
                        }
                    }
                }
            }
        }
    }
}