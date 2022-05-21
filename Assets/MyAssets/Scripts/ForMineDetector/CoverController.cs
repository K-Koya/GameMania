using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MineDetector
{
    public class CoverController : MonoBehaviour
    {
        /// <summary>�W�I�u�W�F�N�g�̃����_�[���[</summary>
        Renderer _Renderer = default;

        /// <summary>���̃}�e���A��</summary>
        Material _Original = default;

        [SerializeField, Tooltip("�}�E�X�J�[�\�������킹���Ƃ��Ɏg���}�e���A��")]
        Material _OveredMouse = default;

        [SerializeField, Tooltip("True : �}�E�X�J�[�\���������Ă���")]
        bool _IsOveredMouse = false;

        [SerializeField, Tooltip("True : ���J�ς�")]
        bool _IsOpenned = false;

        [SerializeField, Tooltip("True : �������ĂĂ���")]
        bool _IsBuiltFlag = false;

        /// <summary>True : ���J�ς�</summary>
        public bool IsOpenned { get => _IsOpenned; }



        // Start is called before the first frame update
        void Start()
        {
            _Renderer = GetComponent<Renderer>();
            _Original = _Renderer.materials[0];
        }

        // Update is called once per frame
        void Update()
        {
            if (_IsOveredMouse)
            {
                _Renderer.material = _OveredMouse;
                _IsOveredMouse = false;
            }
            else
            {
                _Renderer.material = _Original;
            }
        }

        /// <summary>�J�[�\�������킹�Ă���</summary>
        public void OveredMouseCursor()
        {
            _IsOveredMouse = true;
        }

        /// <summary>�W���O��</summary>
        public void Open()
        {
            //���������Ă���ΊO���Ȃ�
            if (_IsBuiltFlag) return;

            _IsOpenned = true;
            gameObject.SetActive(!_IsOpenned);
        }

        /// <summary>�������ĂĂ���Ƃ��͍~�낵�A�����~�낵�Ă���Ƃ��͌��Ă�</summary>
        public void SwitchFlag()
        {
            _IsBuiltFlag = !_IsBuiltFlag;
        }
    }
}
