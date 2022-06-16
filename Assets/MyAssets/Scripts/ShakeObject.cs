using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeObject : MonoBehaviour
{
    [SerializeField, Tooltip("�h�炵��")]
    public float _ShakeRange = 0f;

    /// <summary>�h�炵�̏����ʒu</summary>
    Vector3 TextInitPos = Vector3.zero;

    private void Start()
    {
        // �J�n���̈ʒu���擾
        TextInitPos = transform.position;
    }

    private void Update()
    {
        // �����_���ɗh�炷
        transform.position = TextInitPos + Random.insideUnitSphere * _ShakeRange;
    }
}
