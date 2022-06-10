using System;
using UnityEngine;

namespace Survival
{
    /// <summary>敵を移動させる</summary>
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyMove : MonoBehaviour
    {
        [SerializeField, Tooltip("移動方法")]
        MovePattern _MovePattern = MovePattern.NotMove;

        [SerializeField, Tooltip("キャラクターの移動力")]
        float _MoveSpeed = 10f;

        [SerializeField, Tooltip("プレイヤーからこの距離分離れると非アクティブ化する")]
        float _ActiveDistance = 28f;

        [SerializeField, Tooltip("移動でリジッドボディにかける力")]
        Vector3 _ForceOfMove = default;

        /// <summary>リジッドボディ</summary>
        Rigidbody _Rb = default;

        /// <summary>移動用メソッド</summary>
        Action Move = default;

        /// <summary>プレイヤー位置</summary>
        static Transform _PlayerTransform = default;
        

        // Start is called before the first frame update
        void Start()
        {
            _Rb = GetComponent<Rigidbody>();
            if(!_PlayerTransform) _PlayerTransform = FindObjectOfType<PlayerMove>().transform;

            switch (_MovePattern)
            {
                case MovePattern.GoStraight:
                    _ForceOfMove = (_PlayerTransform.position - transform.position).normalized * _MoveSpeed;
                    break;
                case MovePattern.Approach:
                    Move = MoveApproach;
                    break;
                default: break;
            }
        }

        void OnEnable()
        {
            switch (_MovePattern)
            {
                case MovePattern.GoStraight:
                    _ForceOfMove = (_PlayerTransform.position - transform.position).normalized * _MoveSpeed;
                    break;
                case MovePattern.Approach:
                    Move = MoveApproach;
                    break;
                default: break;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (PauseManager.IsTimerStopped) return;

            if (Move != null) Move();
            gameObject.SetActive(Vector3.SqrMagnitude(_PlayerTransform.position - transform.position) < _ActiveDistance * _ActiveDistance);
        }

        void FixedUpdate()
        {
            if(_MovePattern != MovePattern.NotMove && _ForceOfMove.sqrMagnitude > 0) 
                _Rb.AddForce(_ForceOfMove, ForceMode.Acceleration);
        }

        /// <summary>プレイヤーに近づくように移動</summary>
        void MoveApproach()
        {
            _ForceOfMove = (_PlayerTransform.position - transform.position).normalized * _MoveSpeed;
        }

        /// <summary>敵の移動手段</summary>
        public enum MovePattern : byte
        {
            /// <summary>動かない</summary>
            NotMove = 0,
            /// <summary>真っ直ぐ移動</summary>
            GoStraight,
            /// <summary>プレイヤーに近づくように移動</summary>
            Approach,
        }
    }
}
