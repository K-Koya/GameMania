using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MineDetector
{
    /// <summary>カメラを移動入力で動かすコンポーネント</summary>
    [RequireComponent(typeof(Rigidbody))]
    public class CameraMove : MonoBehaviour
    {
        Rigidbody _Rb = default;

        [SerializeField, Tooltip("入力名 : 移動入力の上下軸")]
        string _InputNameMoveVertical = "Vertical";

        [SerializeField, Tooltip("入力名 : 移動入力の左右軸")]
        string _InputNameMoveHorizontal = "Horizontal";

        [SerializeField, Tooltip("入力名 : 移動入力の前後軸")]
        string _InputNameMoveForward = "Mouse ScrollWheel";

        [SerializeField, Tooltip("上下左右移動力")]
        float _PlaneMoveSpeed = 2f;

        [SerializeField, Tooltip("前後移動力")]
        float _ZoomMoveSpeed = 10f;

        /// <summary>移動させるためにかける力</summary>
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