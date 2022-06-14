using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    /// <summary>�����ɐG�ꂽ�牽���I�u�W�F�N�g���o��</summary>
    public class HitToEmitObject : MonoBehaviour
    {
        [SerializeField, Tooltip("�o���I�u�W�F�N�g")]
        GameObject[] _Emits = null;

        [SerializeField, Tooltip("true : �G�ꂽ�珜������")]
        bool _IsDestroyOnHit = false;

        [SerializeField, Tooltip("true : �G�ꂽ���A�N�e�B�u�ɂ���")]
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