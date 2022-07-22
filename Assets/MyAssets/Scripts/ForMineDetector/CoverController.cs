using UnityEngine;

namespace MineDetector
{
    public class CoverController : MonoBehaviour
    {
        /// <summary>�W�I�u�W�F�N�g�̃����_�[���[</summary>
        Renderer _Renderer = default;

        /// <summary>���Y�Z���̏��</summary>
        CellController _Cell = null;

        /// <summary>���̋󔒃}�e���A��</summary>
        Material _Original = default;

        [SerializeField, Tooltip("�}�E�X�J�[�\�������킹���Ƃ��Ɏg���󔒃}�e���A��")]
        Material _OriginalOveredMouse = default;

        [SerializeField, Tooltip("���̊��}�e���A��")]
        Material _BuiltFlag = default;

        [SerializeField, Tooltip("�}�E�X�J�[�\�������킹���Ƃ��Ɏg�����}�e���A��")]
        Material _BuiltFlagOveredMouse = default;

        [SerializeField, Tooltip("���̂͂Ăȃ}�e���A��")]
        Material _Question = default;

        [SerializeField, Tooltip("�}�E�X�J�[�\�������킹���Ƃ��Ɏg���͂Ăȃ}�e���A��")]
        Material _QuestionOveredMouse = default;

        [SerializeField, Tooltip("True : �}�E�X�J�[�\���������Ă���")]
        bool _IsOveredMouse = false;

        [SerializeField, Tooltip("True : ���J�ς�")]
        bool _IsOpenned = false;

        [SerializeField, Tooltip("True : �������ĂĂ���")]
        bool _IsBuiltFlag = false;


        /// <summary>True : ���J�ς�</summary>
        public bool IsOpenned { get => _IsOpenned; }

        /// <summary>�Z���̈ʒu</summary>
        public Vector2Int Index { get => _Cell.Index; }



        // Start is called before the first frame update
        void Start()
        {
            _Renderer = GetComponent<Renderer>();
            _Original = _Renderer.materials[0];

            _Cell = GetComponentInParent<CellController>();
            _Cell.OpenMyself = Open;
            _Cell.IsOpenned = () => IsOpenned;
        }

        // Update is called once per frame
        void Update()
        {
            if (_IsOveredMouse)
            {
                if (_IsBuiltFlag)
                {
                    _Renderer.material = _BuiltFlagOveredMouse;
                }
                else
                {
                    _Renderer.material = _OriginalOveredMouse;
                }

                _IsOveredMouse = false;
            }
            else
            {
                if (_IsBuiltFlag)
                {
                    _Renderer.material = _BuiltFlag;
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
            _IsOveredMouse = true;
        }

        /// <summary>�W���O��</summary>
        public void Open()
        {
            //�Q�[���J�n���\�b�h�𖢎��s�Ȃ���s���A�Q�[���J�n
            if (MineDetectorCellMap.GameStart != null)
            {
                MineDetectorCellMap.GameStart(Index.x, Index.y);
                GameManager.GameStartCall();
            }

            //���ɂ��̃}�X���J���Ă���Η��E
            if (_IsOpenned) return;

            //���������Ă���ΊO���Ȃ�
            if (_IsBuiltFlag) return;

            _IsOpenned = true;
            gameObject.SetActive(!_IsOpenned);

            //�}�X�̒��g�ɂ���ď�����ς���
            switch (_Cell.Contant)
            {
                //���̃}�X���󔒂Ȃ���͂̃}�X���J����
                case CellController.EMPTY_CONTENT:
                    MineDetectorCellMap.CheckAround(Index.y, Index.x);
                    break;

                //���̃}�X�����e�Ȃ�s�k�����e�}�X�����ׂĊJ������ŃQ�[���I��
                case CellController.MINE_CONTENT:
                    GameManager.GameFaultCall();
                    MineDetectorCellMap.CheckTheAnswer();
                    break;
            }

            //�Q�[�����N���A���Ă�����A�����񍐂���
            if (MineDetectorCellMap.CheckCleared())
            {
                GameManager.GameClearCall();
            }
        }

        /// <summary>�������ĂĂ���Ƃ��͍~�낵�A�����~�낵�Ă���Ƃ��͌��Ă�</summary>
        /// <returns>�ύX��̊��̏�</returns>
        public bool SwitchFlag()
        {
            _IsBuiltFlag = !_IsBuiltFlag;
            return _IsBuiltFlag;
        }
    }
}
