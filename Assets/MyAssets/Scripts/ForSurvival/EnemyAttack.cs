using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField, Tooltip("ダメージ因子に触れている間与える続けるダメージ")]
    float _PowerOnStay = 0.05f;

    [SerializeField, Tooltip("ダメージ因子に触れた直後のみ与えるダメージ")]
    float _PowerOnEnter = 0f;

    /// <summary>ダメージ因子に触れている間与える続けるダメージ</summary>
    public float PowerOnStay { get => _PowerOnStay; }

    /// <summary>ダメージ因子に触れた直後のみ与えるダメージ</summary>
    public float PowerOnEnter { get => _PowerOnEnter; }
}
