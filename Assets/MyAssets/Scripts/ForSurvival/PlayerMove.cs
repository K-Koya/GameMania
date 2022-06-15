using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    /// <summary>プレイヤーを移動させる</summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField, Tooltip("入力名 : 移動入力の上下軸")]
        string _InputNameMoveVertical = "Vertical";

        [SerializeField, Tooltip("入力名 : 移動入力の左右軸")]
        string _InputNameMoveHorizontal = "Horizontal";

        [SerializeField, Tooltip("キャラクターの移動力")]
        float _MoveSpeed = 10f;

        [SerializeField, Tooltip("移動速度の強化レベル")]
        byte _MoveSpeedLevel = 1;

        /// <summary>移動速度の強化レベルの最大値</summary>
        byte _MoveSpeedMaxLevel = 9;


        /// <summary>移動でリジッドボディにかける力</summary>
        Vector3 _ForceOfMove = default;

        /// <summary>リジッドボディ</summary>
        Rigidbody _Rb = default;


        /// <summary>移動速度の強化レベル</summary>
        public byte MoveSpeedLevel { get => _MoveSpeedLevel; }
        /// <summary>移動速度の強化レベルの最大値</summary>
        public byte MoveSpeedMaxLevel { get => _MoveSpeedMaxLevel; }


        // Start is called before the first frame update
        void Start()
        {
            _Rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            if (PauseManager.IsTimerStopped) return;

            _ForceOfMove = Vector3.forward * Input.GetAxis(_InputNameMoveVertical) + Vector3.right * Input.GetAxis(_InputNameMoveHorizontal);
            _ForceOfMove = _ForceOfMove.normalized * (_MoveSpeed + _MoveSpeedLevel);
        }

        void FixedUpdate()
        {
            _Rb.AddForce(_ForceOfMove, ForceMode.Acceleration);
        }

        /// <summary>移動速度の強化レベルを加算</summary>
        public void AddMoveSpeedLevel()
        {
            _MoveSpeedLevel++;
        }
    }
}