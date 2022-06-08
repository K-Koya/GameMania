using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField, Tooltip("�_���[�W���q�ɐG��Ă���ԗ^���鑱����_���[�W")]
    float _PowerOnStay = 0.05f;

    [SerializeField, Tooltip("�_���[�W���q�ɐG�ꂽ����̂ݗ^����_���[�W")]
    float _PowerOnEnter = 0f;

    /// <summary>�_���[�W���q�ɐG��Ă���ԗ^���鑱����_���[�W</summary>
    public float PowerOnStay { get => _PowerOnStay; }

    /// <summary>�_���[�W���q�ɐG�ꂽ����̂ݗ^����_���[�W</summary>
    public float PowerOnEnter { get => _PowerOnEnter; }
}
