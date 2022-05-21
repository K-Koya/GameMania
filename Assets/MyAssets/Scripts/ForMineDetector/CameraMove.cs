using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MineDetector
{
    /// <summary>�J�������ړ����͂œ������R���|�[�l���g</summary>
    [RequireComponent(typeof(Rigidbody))]
    public class CameraMove : MonoBehaviour
    {
        Rigidbody _Rb = default;

        [SerializeField, Tooltip("���͖� : �ړ����͂̏㉺��")]
        string _InputNameMoveVertical = "Vertical";

        [SerializeField, Tooltip("���͖� : �ړ����͂̍��E��")]
        string _InputNameMoveHorizontal = "Horizontal";

        [SerializeField, Tooltip("���͖� : �ړ����͂̑O�㎲")]
        string _InputNameMoveForward = "Mouse ScrollWheel";

        [SerializeField, Tooltip("�㉺���E�ړ���")]
        float _PlaneMoveSpeed = 2f;

        [SerializeField, Tooltip("�O��ړ���")]
        float _ZoomMoveSpeed = 10f;

        /// <summary>�ړ������邽�߂ɂ������</summary>
        Vector3 _ForceOfMove = Vector3.zero;


        // Start is called before the first frame update
        void Start()
        {
            _Rb = GetComponent<Rigidbody>();
            _Rb.useGravity = false;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 moveVertical = Vector3.forward * Input.GetAxis(_InputNameMoveVertical);
            Vector3 moveHorizontal = Vector3.right * Input.GetAxis(_InputNameMoveHorizontal);
            Vector3 moveForward = Vector3.down * Input.GetAxis(_InputNameMoveForward);

            _ForceOfMove = (moveVertical + moveHorizontal) * _PlaneMoveSpeed + moveForward * _ZoomMoveSpeed;
        }

        void FixedUpdate()
        {
            _Rb.AddForce(_ForceOfMove, ForceMode.Acceleration);
        }
    }
}