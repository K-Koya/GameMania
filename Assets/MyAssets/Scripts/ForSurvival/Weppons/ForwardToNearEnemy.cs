using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardToNearEnemy : MonoBehaviour
{
    [SerializeField, Tooltip("�G���C���[")]
    LayerMask _EnemyLayer = default;

    [SerializeField, Tooltip("���̃I�u�W�F�N�g���L���ɂȂ�����������߂�")]
    GameObject _GameObject = null;

    /// <summary>true : ���ɕ������߂Ă���</summary>
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
