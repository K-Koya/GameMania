using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    /// <summary>何かに触れたら何かオブジェクトを出す</summary>
    public class HitToEmitObject : MonoBehaviour
    {
        [SerializeField, Tooltip("出すオブジェクト")]
        GameObject[] _Emits = null;

        [SerializeField, Tooltip("true : 触れたら除去する")]
        bool _IsDestroyOnHit = false;

        [SerializeField, Tooltip("true : 触れたら非アクティブにする")]
        bool _IsDisableOnHit = false;

         void Update()
        {
            
        }

        void OnTriggerEnter(Collider other)
        {
            foreach (GameObject emit in _Emits)
            {
                GameObject obj = Instantiate(emit);
                obj.transform.position = transform.position;
            }

            if (_IsDestroyOnHit)
            {
                Destroy(gameObject);
            }

            if (_IsDisableOnHit)
            {
                gameObject.SetActive(false);
            }
        }
    }
}