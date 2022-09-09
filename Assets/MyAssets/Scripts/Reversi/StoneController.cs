using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Reversi
{
    public class StoneController : MonoBehaviour
    {
        /// <summary>石の剛体性質</summary>
        Rigidbody _Rb = null;

        [SerializeField, Tooltip("石の半径")]
        float _StoneRadius = 0.5f;

        [SerializeField, Tooltip("石をひっくり返す力の大きさ")]
        float _RollOverPower = 5f;

        /// <summary>石の真上方向のベクトル</summary>
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

        /// <summary>石を瞬時にひっくり返す</summary>
        public void Inverse()
        {
            //_StoneUpVector = -transform.up;
            //transform.up = _StoneUpVector;
        }

        /// <summary>石を回転させてひっくり返す</summary>
        public void TurnOver()
        {
            //今の上方向を設定し直し反転
            //transform.up = _StoneUpVector;
            //_StoneUpVector = -transform.up;

            //力のかける位置を設定
            Vector3 forcePos = transform.forward;
            switch(Random.Range(0, 4))
            {
                case 0: forcePos = transform.forward; break;
                case 1: forcePos = transform.right; break;
                case 2: forcePos = -transform.forward; break;
                case 3: forcePos = -transform.right; break;
                default: break;
            }

            //ひっくり返す
            _Rb.AddForceAtPosition(Vector3.up * _RollOverPower, transform.position + forcePos * _StoneRadius, ForceMode.VelocityChange);
        }
    }

    /// <summary>石の色</summary>
    public enum StoneColor : sbyte
    {
        Black = 1,
        None = 0,
        White = -1,
    }
}