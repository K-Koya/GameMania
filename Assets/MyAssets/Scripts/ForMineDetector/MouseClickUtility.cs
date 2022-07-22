using UnityEngine;

namespace MineDetector
{
    public class MouseClickUtility : MonoBehaviour
    {
        [SerializeField, Tooltip("�ΏۂɂȂ�J����")]
        Camera _Camera = default;

        [SerializeField, Tooltip("�N���b�N�\�ȃ��C���[")]
        LayerMask _ClickableLayer = default;

        [SerializeField, Tooltip("�N���b�N���T������Ray�̒���")]
        float _RayLength = 100f;

        [SerializeField, Tooltip("���͖� : �}�E�X���N���b�N")]
        string _InputNameMouseLeft = "Fire1";

        [SerializeField, Tooltip("���͖� : �}�E�X�E�N���b�N")]
        string _InputNameMouseRight = "Fire3";

        /// <summary>���̖{��</summary>
        int _NumberOfFlag = 0;

        /// <summary>���̖{��</summary>
        public int NumberOfFlag { get => _NumberOfFlag; }


        // Start is called before the first frame update
        void Start()
        {
            _Camera = Camera.main;
            _NumberOfFlag = 0;
        }

        // Update is called once per frame
        void Update()
        {
            MouseClickManagement();
        }


        /// <summary>�}�E�X�N���b�N����</summary>
        void MouseClickManagement()
        {
            var hitInfo = IsClickedAnyCell();
            if (hitInfo.Item1)
            {
                //�}�X�̊W�����擾
                CoverController cover = hitInfo.Item2.collider.GetComponent<CoverController>();
                cover.OveredMouseCursor();

                //�E�N���b�N
                if (Input.GetButtonDown(_InputNameMouseRight))
                {
                    //���̔��]����
                    if (cover.SwitchFlag())
                    {
                        //���̖{����ǉ�
                        _NumberOfFlag++;
                    }
                    else
                    {
                        //���̖{�������炷
                        _NumberOfFlag--;
                    }
                }
                //���N���b�N
                else if (Input.GetButtonDown(_InputNameMouseLeft))
                {
                    //�W���O������
                    cover.Open();
                }
            }
        }

        /// <summary>
        /// �}�X�ڂ̃N���b�N��������A���ʂ�Ԃ�
        /// </summary>
        /// <returns>bool True : �}�X��������  RaycastHit Ray�𓖂Ă���̏��</returns>
        (bool, RaycastHit) IsClickedAnyCell()
        {
            //�N���b�N�ʒu���擾���ăQ�[�������W�ɕϊ�
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 1f;
            Ray ray = _Camera.ScreenPointToRay(mousePosition);

            //Ray���J�������ʂɌ����Ĕ�΂�
            RaycastHit hit;
            bool isHit = Physics.Raycast(ray, out hit, _RayLength, _ClickableLayer);

            return (isHit, hit);
        }
    }
}
