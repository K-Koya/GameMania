using System;
using UnityEngine;

namespace Survival
{
    /// <summary>�G���ړ�������</summary>
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyMove : MonoBehaviour
    {
        [SerializeField, Tooltip("�ړ����@")]
        MovePattern _MovePattern = MovePattern.NotMove;

        [SerializeField, Tooltip("�L�����N�^�[�̈ړ���")]
        float _MoveSpeed = 10f;

        [SerializeField, Tooltip("�v���C���[���炱�̋����������Ɣ�A�N�e�B�u������")]
        float _ActiveDistance = 28f;

        [SerializeField, Tooltip("�ړ��Ń��W�b�h�{�f�B�ɂ������")]
        Vector3 _ForceOfMove = default;

        /// <summary>���W�b�h�{�f�B</summary>
        Rigidbody _Rb = default;

        /// <summary>�ړ��p���\�b�h</summary>
        Action Move = default;

        /// <summary>�v���C���[�ʒu</summary>
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

        /// <summary>�v���C���[�ɋ߂Â��悤�Ɉړ�</summary>
        void MoveApproach()
        {
            _ForceOfMove = (_PlayerTransform.position - transform.position).normalized * _MoveSpeed;
        }

        /// <summary>�G�̈ړ���i</summary>
        public enum MovePattern : byte
        {
            /// <summary>�����Ȃ�</summary>
            NotMove = 0,
            /// <summary>�^�������ړ�</summary>
            GoStraight,
            /// <summary>�v���C���[�ɋ߂Â��悤�Ɉړ�</summary>
            Approach,
        }
    }
}
