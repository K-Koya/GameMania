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

        /// <summary>true : �΂𗎂Ƃ���</summary>
        bool _IsAbleToDrop = false;

        [SerializeField, Tooltip("�Z���̏ꏊ")]
        Vector2Int _FloorIndex = Vector2Int.zero;

        [SerializeField, Tooltip("�΂������鏰�̃}�e���A��")]
        Material _AbleToDropStone = default;

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
        /// <summary>����Ă���΂̐F</summary>
        public StoneColor StoneColor { get => _StoneColor; }
        /// <summary>�΂̃f�[�^</summary>
        public StoneController StoneController { get => _StoneController; }
        /// <summary>true : �΂𗎂Ƃ���</summary>
        public bool IsAbleToDrop { get => _IsAbleToDrop; set => _IsAbleToDrop = value; }
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
                if (_IsAbleToDrop)
                {
                    _Renderer.material = _AbleToDropStone;
                }
                else
                {
                    _Renderer.material = _Original;
                }
            }
        }

        /// <summary>�J�[�\�������킹�Ă���</summary>
        public void OveredMouseCursor()
        {
            if (_StoneColor == StoneColor.None && _IsAbleToDrop)
            {
                _IsOveredMouse = true;
            }
        }

        /// <summary>�΂𓊉�����</summary>
        /// <param name="stoneColor">�F</param>
        /// <returns>true : �p�X������</returns>
        public bool DropStone(StoneColor stoneColor, bool useAnim)
        {
            //���ɐ΂�����Ă����Ȃ痣�E
            if (_StoneColor != StoneColor.None) return false;

            //���F��n���ė����痣�E
            if (stoneColor == StoneColor.None) return false;

            //�΂�������悤�ɂ��ē�������
            if(useAnim) _StoneController.transform.position = transform.position + transform.up * 3f;
            else _StoneController.transform.position = transform.position + transform.up * 0.5f;
            _StoneController.transform.up = Vector3.up * (int)stoneColor;
            _StoneController.gameObject.SetActive(true);

            //�F���X�V
            _StoneColor = stoneColor;
            //���̐΂𗠕Ԃ�
            ReversiCellMap.Instance.TurnOverStones(stoneColor, _FloorIndex.x, _FloorIndex.y);
            //�Ֆʂ��X�V
            if (ReversiCellMap.Instance.DetectTurnOverCell(stoneColor))
            {
                return false;
            }

            return true;
        }

        /// <summary>�΂��w�肵���F�ɂ���</summary>
        /// <param name="stoneColor">�F</param>
        public void TurnOverStone(StoneColor stoneColor)
        {
            if (_StoneColor == stoneColor) return;

            _StoneColor = stoneColor;
            _StoneController.TurnOver();
        }

        /// <summary>�}�X�ɉ����u����Ă��Ȃ���Ԃɖ߂�</summary>
        public void ResetCell()
        {
            _StoneColor = StoneColor.None;
            _StoneController.gameObject.SetActive(false);
        }
    }
}