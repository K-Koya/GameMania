using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    /// <summary>���̂���p �E�q���e</summary>
    public class SporeBomb : PlayerWepponBase
    {
        [SerializeField, Tooltip("�U����")]
        WepponInfomation _Power = default;

        [SerializeField, Tooltip("���ˊԊu")]
        WepponInfomation _Interval = default;
                
        [SerializeField, Tooltip("�傫��")]
        WepponInfomation _Size = default;

        [SerializeField, Tooltip("�c������")]
        WepponInfomation _Remaining = default;

        // Start is called before the first frame update
        void Start()
        {
            _WepponInfomations.Add(_Power);
            _WepponInfomations.Add(_Interval);
            _WepponInfomations.Add(_Size);
            _WepponInfomations.Add(_Remaining);
        }

        protected override void DoAttack()
        {
            throw new System.NotImplementedException();
        }
    }
}