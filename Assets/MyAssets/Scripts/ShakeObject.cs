using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeObject : MonoBehaviour
{
    [SerializeField, Tooltip("揺らし幅")]
    public float _ShakeRange = 0f;

    /// <summary>揺らしの初期位置</summary>
    Vector3 TextInitPos = Vector3.zero;

    private void Start()
    {
        // 開始時の位置を取得
        TextInitPos = transform.position;
    }

    private void Update()
    {
        // ランダムに揺らす
        transform.position = TextInitPos + Random.insideUnitSphere * _ShakeRange;
    }
}
