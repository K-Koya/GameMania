using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    public class SporeBombBlastCtrl : MonoBehaviour
    {
        /// <summary>攻撃情報</summary>
        AttackInfoMovable _AIM = null;

        /// <summary>爆発範囲エフェクト</summary>
        GameObject _BlastEffect = null;

        /// <summary>true : 着弾した</summary>
        bool _IsLanding = false;

        /// <summary>本オブジェクトのリジッドボディ</summary>
        Rigidbody _Rb = null;

        /// <summary>本オブジェクトのコライダー</summary>
        SphereCollider _SphereCollider = null;

        /// <summary>本オブジェクトのコライダーの元の半径</summary>
        float _BaseColliderRadius = 1f;

        /// <summary>アニメーションを再生するデリゲート</summary>
        System.Action<int> _PlayAnim = null;

        // Start is called before the first frame update
        void Start()
        {
            _AIM = GetComponent<AttackInfoMovable>();
            _BlastEffect = transform.GetChild(0).gameObject;
            _SphereCollider = GetComponent<SphereCollider>();
            _Rb = GetComponent<Rigidbody>();
            _BaseColliderRadius = _SphereCollider.radius;

            _PlayAnim = _BlastEffect.GetComponent<Animator>().Play;

            OnEnable();
        }

        // Update is called once per frame
        void Update()
        {

        }

        

        void OnEnable()
        {
            _IsLanding = false;
            if (_AIM) _AIM.enabled = false;
            if (_BlastEffect) _BlastEffect.SetActive(false);
            if (_SphereCollider) _SphereCollider.radius = _BaseColliderRadius;
            if (_Rb) _Rb.useGravity = true;
            gameObject.layer = LayerManager.Default;
        }

        void OnTriggerEnter(Collider other)
        {
            if (_IsLanding) return;

            if (other.gameObject.layer == LayerManager.Ground)
            {
                _IsLanding = true;
                _AIM.enabled = true;
                _BlastEffect.SetActive(true);
                _PlayAnim(0);
                _SphereCollider.radius = _BaseColliderRadius * 10f;
                _Rb.velocity = Vector3.zero;
                _Rb.useGravity = false;
                gameObject.layer = LayerManager.PlayerAttack;
                SEManager.Emit((int)SEManager.Kind.PlayerWeapon2, transform);
            }
        }
    }
}