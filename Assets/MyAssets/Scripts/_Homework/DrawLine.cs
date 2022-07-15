using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Homework
{
    [RequireComponent(typeof(LineRenderer))]
    public class DrawLine : MonoBehaviour
    {
        /// <summary>����`���R���|�[�l���g</summary>
        LineRenderer _Line = null;

        /// <summary>�}�E�X�|�C���^�[�̈ʒu��ۊǂ���z��</summary>
        Vector3[] _MousePosHistroies = null;

        /// <summary>�}�E�X�|�C���^�[�̈ʒu��ۊǂ���z��ŁA�ł��V�������m�̗v�f�ԍ�</summary>
        byte _FirstOfHistroyIndex = 0;

        [SerializeField, Tooltip("�}�E�X�|�C���^�[�̈ʒu��ۊǂ��鎞�ԊԊu")]
        float _RegisterInterval = 0.1f;

        [SerializeField, Tooltip("�}�E�X�|�C���^�[�����Ă��郌�C��")]
        LayerMask _OnMouseLayer = default;

        // Start is called before the first frame update
        void Start()
        {
            _Line = GetComponent<LineRenderer>();
            // ���̕�
            _Line.startWidth = 0.1f;
            _Line.endWidth = 0.1f;
            // ���_�̐�
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

                // ���_��ݒ�
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