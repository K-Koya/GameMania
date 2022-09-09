using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Reversi
{
    public class StoneController : MonoBehaviour
    {
        /// <summary>�΂̍��̐���</summary>
        Rigidbody _Rb = null;

        [SerializeField, Tooltip("�΂̔��a")]
        float _StoneRadius = 0.5f;

        [SerializeField, Tooltip("�΂��Ђ�����Ԃ��͂̑傫��")]
        float _RollOverPower = 5f;

        /// <summary>�΂̐^������̃x�N�g��</summary>
        Vector3 _StoneUpVector = Vector3.up;

        // Start is called before the first frame update
        void Start()
        {
            _Rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>�΂��u���ɂЂ�����Ԃ�</summary>
        public void Inverse()
        {
            //_StoneUpVector = -transform.up;
            //transform.up = _StoneUpVector;
        }

        /// <summary>�΂���]�����ĂЂ�����Ԃ�</summary>
        public void TurnOver()
        {
            //���̏������ݒ肵�������]
            //transform.up = _StoneUpVector;
            //_StoneUpVector = -transform.up;

            //�͂̂�����ʒu��ݒ�
            Vector3 forcePos = transform.forward;
            switch(Random.Range(0, 4))
            {
                case 0: forcePos = transform.forward; break;
                case 1: forcePos = transform.right; break;
                case 2: forcePos = -transform.forward; break;
                case 3: forcePos = -transform.right; break;
                default: break;
            }

            //�Ђ�����Ԃ�
            _Rb.AddForceAtPosition(Vector3.up * _RollOverPower, transform.position + forcePos * _StoneRadius, ForceMode.VelocityChange);
        }
    }

    /// <summary>�΂̐F</summary>
    public enum StoneColor : sbyte
    {
        Black = 1,
        None = 0,
        White = -1,
    }
}