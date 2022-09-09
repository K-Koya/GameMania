using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reversi
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



        // Start is called before the first frame update
        void Start()
        {
            _Camera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            OnMouseManagement();
        }


        /// <summary>�}�E�X�N���b�N����</summary>
        void OnMouseManagement()
        {
            if (GameManager.Instance.Phase == GamePhase.Playing)
            {
                var hitInfo = IsClickedAnyCell();
                if (hitInfo.Item1)
                {
                    //�}�X�����擾
                    FloorController floor = hitInfo.Item2.collider.GetComponent<FloorController>();
                    floor.OveredMouseCursor();

                    //�E�N���b�N
                    if (Input.GetButtonDown(_InputNameMouseRight))
                    {

                    }
                    //���N���b�N
                    else if (Input.GetButtonDown(_InputNameMouseLeft))
                    {
                        if (floor.IsAbleToDrop)
                        {
                            bool isPass = floor.DropStone(GameManager.Instance.Turn, true);
                            GameManager.Instance.SwitchTurn();
                            if (isPass)
                            {
                                if (ReversiCellMap.Instance.DetectTurnOverCell(GameManager.Instance.Turn))
                                {
                                    GameManager.Instance.Phase = GamePhase.Ending;
                                }
                            }
                        }
                    }
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