using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Homework
{
    [RequireComponent(typeof(LineRenderer))]
    public class DrawLine : MonoBehaviour
    {
        /// <summary>線を描くコンポーネント</summary>
        LineRenderer _Line = null;

        /// <summary>マウスポインターの位置を保管する配列</summary>
        Vector3[] _MousePosHistroies = null;

        /// <summary>マウスポインターの位置を保管する配列で、最も新しいモノの要素番号</summary>
        byte _FirstOfHistroyIndex = 0;

        [SerializeField, Tooltip("マウスポインターの位置を保管する時間間隔")]
        float _RegisterInterval = 0.1f;

        [SerializeField, Tooltip("マウスポインターが当てられるレイヤ")]
        LayerMask _OnMouseLayer = default;

        // Start is called before the first frame update
        void Start()
        {
            _Line = GetComponent<LineRenderer>();
            // 線の幅
            _Line.startWidth = 0.1f;
            _Line.endWidth = 0.1f;
            // 頂点の数
            _MousePosHistroies = new Vector3[_Line.positionCount];
            _Line.numCapVertices = _MousePosHistroies.Length;

            StartCoroutine(RegisterMousePosition());
        }

        // Update is called once per frame
        void Update()
        {
            for(int i = 0; i < _MousePosHistroies.Length; i++)
            {
                int index = i + _FirstOfHistroyIndex;
                if(index >= _MousePosHistroies.Length) index -= _MousePosHistroies.Length;

                // 頂点を設定
                _Line.SetPosition(i, _MousePosHistroies[index]);
            }
        }

        IEnumerator RegisterMousePosition()
        {
            while (true)
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = 1f;
                Ray ray = Camera.main.ScreenPointToRay(mousePosition);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100f, _OnMouseLayer))
                {
                    _MousePosHistroies[_FirstOfHistroyIndex] = hit.point;
                    if (++_FirstOfHistroyIndex >= _MousePosHistroies.Length) _FirstOfHistroyIndex -= (byte)_MousePosHistroies.Length;

                    yield return new WaitForSeconds(_RegisterInterval);
                }
                else
                {
                    yield return null;
                }
            }
        }
    }
}