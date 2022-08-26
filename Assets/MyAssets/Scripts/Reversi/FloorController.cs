using UnityEngine;

namespace Reversi
{
    [RequireComponent(typeof(Renderer))]
    public class FloorController : MonoBehaviour
    {
        #region �����o
        [SerializeField, Tooltip("�΂̎��̃v���n�u")]
        GameObject _StonePrefab = null;

        /// <summary>���̃����_�[���[</summary>
        Renderer _Renderer = default;

        /// <summary>���̏��̃}�e���A��</summary>
        Material _Original = default;

        [SerializeField, Tooltip("�Z���̏ꏊ")]
        Vector2Int _FloorIndex = Vector2Int.zero;

        [SerializeField, Tooltip("�}�E�X�J�[�\�������킹���Ƃ��Ɏg�����̃}�e���A��")]
        Material _OriginalOveredMouse = default;

        [SerializeField, Tooltip("True : �}�E�X�J�[�\���������Ă���")]
        bool _IsOveredMouse = false;

        /// <summary>����Ă���΂̐F</summary>
        StoneColor _StoneColor = StoneColor.None;

        /// <summary>�΂̃f�[�^</summary>
        StoneController _StoneController = null;
        #endregion

        #region �v���p�e�B
        /// <summary>���̏����ǂ̈ʒu�ɂ��邩�̏��</summary>
        public Vector2Int FloorIndex { get => _FloorIndex; set => _FloorIndex = value; }
        #endregion


        // Start is called before the first frame update
        void Start()
        {
            //�����_�[���[�ƌ��̃}�e���A�����擾
            _Renderer = GetComponent<Renderer>();
            _Original = _Renderer.material;

            //�΂𐶐����ăf�[�^�������N
            GameObject obj = Instantiate(_StonePrefab);
            obj.SetActive(false);
            obj.transform.parent = transform;
            _StoneController = obj.GetComponent<StoneController>();
            _StoneController.transform.localPosition = transform.up;

            _StoneColor = StoneColor.None;
        }

        // Update is called once per frame
        void Update()
        {
            if (_IsOveredMouse)
            {
                _Renderer.material = _OriginalOveredMouse;
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
            if (_StoneColor == StoneColor.None)
            {
                _IsOveredMouse = true;
            }
        }

        /// <summary>�΂𓊉�����</summary>
        /// <param name="stoneColor">�F</param>
        public void DropStone(StoneColor stoneColor)
        {
            //���ɐ΂�����Ă����Ȃ痣�E
            if (_StoneColor != StoneColor.None) return;

            //���F��n���ė����痣�E
            if (stoneColor == StoneColor.None) return;

            //�΂�������悤�ɂ��ē�������
            _StoneController.transform.position = transform.position + transform.up * 9f;
            _StoneController.transform.up = Vector3.up * (int)stoneColor;
            _StoneController.gameObject.SetActive(true);

            //�F���X�V
            _StoneColor = stoneColor;
        }

        /// <summary>�΂𗎉������邱�ƂȂ����Ֆʏ�ɒu��</summary>
        /// <param name="stoneColor">�F</param>
        public void SetStone(StoneColor stoneColor)
        {

        }
    }
}