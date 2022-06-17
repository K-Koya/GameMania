using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardToNearEnemy : MonoBehaviour
{
    [SerializeField, Tooltip("敵レイヤー")]
    LayerMask _EnemyLayer = default;

    [SerializeField, Tooltip("このオブジェクトが有効になったら方向を定める")]
    GameObject _GameObject = null;

    /// <summary>true : 既に方向を定めている</summary>
    bool _IsTargeted = false;

    void OnEnable()
    {
        _GameObject.SetActive(false);
        _IsTargeted = false;
    }

    void Update()
    {
        if (_IsTargeted) return;

        if (_GameObject.activeSelf)
        {
            _IsTargeted = true;

            Vector3 dir = transform.forward;

            RaycastHit hit;
            if (Physics.SphereCast(transform.position + Vector3.up * 11f, 10f, Vector3.down, out hit, 11f, _EnemyLayer))
            {
                dir = Vector3.ProjectOnPlane(hit.point - transform.position, Vector3.up).normalized;
            }

            transform.forward = dir;
        }
    }
}
