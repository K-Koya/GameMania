using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    /// <summary>�v���C���[���ړ�������</summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField, Tooltip("���͖� : �ړ����͂̏㉺��")]
        string _InputNameMoveVertical = "Vertical";

        [SerializeField, Tooltip("���͖� : �ړ����͂̍��E��")]
        string _InputNameMoveHorizontal = "Horizontal";

        [SerializeField, Tooltip("�L�����N�^�[�̈ړ���")]
        float _MoveSpeed = 10f;

        /// <summary>�ړ��Ń��W�b�h�{�f�B�ɂ������</summary>
        Vector3 _ForceOfMove = default;

        /// <summary>���W�b�h�{�f�B</summary>
        Rigidbody _Rb = default;



        // Start is called before the first frame update
        void Start()
        {
            _Rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            _ForceOfMove = Vector3.forward * Input.GetAxis(_InputNameMoveVertical) + Vector3.right * Input.GetAxis(_InputNameMoveHorizontal);
            _ForceOfMove = _ForceOfMove.normalized * _MoveSpeed;
        }

        void FixedUpdate()
        {
            _Rb.AddForce(_ForceOfMove, ForceMode.Acceleration);
        }
    }
}