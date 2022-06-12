using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    /// <summary>‚«‚Ì‚±ê—p –Eq”š’e</summary>
    public class SporeBomb : PlayerWepponBase
    {
        [SerializeField, Tooltip("UŒ‚—Í")]
        WepponInfomation _Power = default;

        [SerializeField, Tooltip("”­ËŠÔŠu")]
        WepponInfomation _Interval = default;
                
        [SerializeField, Tooltip("‘å‚«‚³")]
        WepponInfomation _Size = default;

        [SerializeField, Tooltip("c—¯ŠÔ")]
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