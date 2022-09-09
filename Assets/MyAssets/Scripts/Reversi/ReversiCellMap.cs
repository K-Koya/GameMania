using UnityEngine;

namespace Reversi
{
    public class ReversiCellMap : MonoBehaviour
    {
        /// <summary>�V���O���g���A�N�Z�b�T�[</summary>
        static ReversiCellMap _Instance = null;

        [SerializeField, Tooltip("�Q�[���Ղ̏����\������v���n�u")]
        GameObject _FloorPrefab = null;

        [SerializeField, Tooltip("�}�X���m�̒��S�_�Ԃ̋���")]
        float _CellPositionInterval = 1.05f;

        [SerializeField, Tooltip("�Q�[���Ղ̏c���}�X�̐�")]
        Vector2Int _MapSize = new Vector2Int(8, 8);

        /// <summary>�Q�[���Տ��</summary>
        FloorController[,] _Map = null;


        /// <summary>�V���O���g���A�N�Z�b�T�[</summary>
        public static ReversiCellMap Instance { get => _Instance; }


        void Awake()
        {
            _Instance = this;
        }

        /// <summary>�Ԃɓ��������C�o���̐΂������̂��̂ɂЂ����肩����</summary>
        /// <param name="droppedStone">�����̐�</param>
        public void TurnOverStones(StoneColor droppedStone, int indexX, int indexY)
        {
            StoneColor invert = StoneColor.None;
            switch (droppedStone)
            {
                case StoneColor.Black:
                    invert = StoneColor.White;
                    break;
                case StoneColor.White:
                    invert = StoneColor.Black;
                    break;
                default:
                    return;
            }

            /* ���̓}�X���`�F�b�N */
            //����
            CheckTurnOverStone(indexX - 1, indexY - 1, -1, -1, droppedStone, invert, true);
            //�^��
            CheckTurnOverStone(indexX - 1, indexY, -1, 0, droppedStone, invert, true);
            //�E��
            CheckTurnOverStone(indexX - 1, indexY + 1, -1, 1, droppedStone, invert, true);
            //�^��
            CheckTurnOverStone(indexX, indexY - 1, 0, -1, droppedStone, invert, true);
            //�^�E
            CheckTurnOverStone(indexX, indexY + 1, 0, 1, droppedStone, invert, true);
            //����
            CheckTurnOverStone(indexX + 1, indexY - 1, 1, -1, droppedStone, invert, true);
            //�^��
            CheckTurnOverStone(indexX + 1, indexY, 1, 0, droppedStone, invert, true);
            //�E��
            CheckTurnOverStone(indexX + 1, indexY + 1, 1, 1, droppedStone, invert, true);

            #region �f�o�b�O
            /*
            string code = "";
            for (int x = 0; x < _Map.GetLength(0); x++)
            {
                for (int y = 0; y < _Map.GetLength(1); y++)
                {
                    string mark = "��";
                    switch (_Map[x, y].StoneColor)
                    {
                        case StoneColor.Black:
                            mark = "��";
                            break;
                        case StoneColor.White:
                            mark = "�Z";
                            break;
                    }

                    code += $" {mark}";
                }
                code += "\n";
            }
            Debug.Log(code);
            */
            #endregion
        }

        /// <summary>��_�ƂȂ�Z������direction�����ɂЂ�����Ԃ���΂����݂��Ă��邩��T�����āA�Ђ�����Ԃ�</summary>
        /// <param name="indexX">��_�Z����Y�ʒu</param>
        /// <param name="indexY">��_�Z����X�ʒu</param>
        /// <param name="directionX">�T��������Y�l</param>
        /// <param name="directionY">�T��������X�l</param>
        /// <param name="dropped">���Ƃ��������̐�</param>
        /// <param name="invert">���C�o���̐�</param>
        /// <param name="first">true : �ċN���\�b�h��1�w��</param>
        /// <returns>true : �Ђ����肩������</returns>
        bool CheckTurnOverStone(int indexX, int indexY, int directionX, int directionY, StoneColor dropped, StoneColor invert, bool first)
        {
            //�ՖʊO�Ȃ�T���I��
            if (indexX < 0 || indexY < 0 || indexX + 1 > _MapSize.y || indexY + 1 > _MapSize.x)
            {
                return false;
            }
            //�΂��u����Ă��Ȃ���ΒT���I��
            if (_Map[indexX, indexY].StoneColor == StoneColor.None)
            {
                return false;
            }
            //�΂����C�o���̐΂Ȃ�A�X�ɒT��
            else if (_Map[indexX, indexY].StoneColor == invert)
            {
                //�T��������łЂ�����Ԃ��邱�Ƃ�������΂Ђ�����Ԃ�
                if(CheckTurnOverStone(indexX + directionX, indexY + directionY, directionX, directionY, dropped, invert, false))
                {
                    _Map[indexX, indexY].TurnOverStone(dropped);
                    return true;
                }
            }
            //�΂������̐΂Ȃ�Ђ�����Ԃ���Ƃ��ė��E
            else if (_Map[indexX, indexY].StoneColor == dropped)
            {
                if (!first) return true;
            }

            return false;
        }

        /// <summary>�Ђ����肩������Z����T��</summary>
        /// <param name="droppedStone">�O�̃^�[���ŗ��Ƃ��ꂽ��</param>
        /// <returns>true : �Ђ����肩������</returns>
        public bool DetectTurnOverCell(StoneColor droppedStone)
        {
            StoneColor invert = StoneColor.None;
            switch (droppedStone)
            {
                case StoneColor.Black:
                    invert = StoneColor.White;
                    break;
                case StoneColor.White:
                    invert = StoneColor.Black;
                    break;
                default:
                    return false;
            }

            //�Ђ�����Ԃ��邩�̏���������
            foreach (FloorController floor in _Map) floor.IsAbleToDrop = false;

            bool isAbleToTurn = false;
            foreach (FloorController floor in _Map)
            {
                if (floor.StoneColor == invert)
                {
                    /* ���̓}�X���`�F�b�N */
                    (int, int) current = (floor.FloorIndex.x, floor.FloorIndex.y);
                    //����
                    isAbleToTurn = CheckTurnOverCell(current.Item1 - 1, current.Item2 - 1, -1, -1, droppedStone, true) || isAbleToTurn;
                    //�^��
                    isAbleToTurn = CheckTurnOverCell(current.Item1 - 1, current.Item2, -1, 0, droppedStone, true) || isAbleToTurn;
                    //�E��
                    isAbleToTurn = CheckTurnOverCell(current.Item1 - 1, current.Item2 + 1, -1, 1, droppedStone, true) || isAbleToTurn;
                    //�^��
                    isAbleToTurn = CheckTurnOverCell(current.Item1, current.Item2 - 1, 0, -1, droppedStone, true) || isAbleToTurn;
                    //�^�E
                    isAbleToTurn = CheckTurnOverCell(current.Item1, current.Item2 + 1, 0, 1, droppedStone, true) || isAbleToTurn;
                    //����
                    isAbleToTurn = CheckTurnOverCell(current.Item1 + 1, current.Item2 - 1, 1, -1, droppedStone, true) || isAbleToTurn;
                    //�^��
                    isAbleToTurn = CheckTurnOverCell(current.Item1 + 1, current.Item2, 1, 0, droppedStone, true) || isAbleToTurn;
                    //�E��
                    isAbleToTurn = CheckTurnOverCell(current.Item1 + 1, current.Item2 + 1, 1, 1, droppedStone, true) || isAbleToTurn;
                }
            }

            return isAbleToTurn;
        }

        /// <summary>��_�ƂȂ�Z������direction�����ɂЂ�����Ԃ���΂����݂��Ă��邩��T�����āA�΂�u����}�[�J�[��������</summary>
        /// <param name="indexX">��_�Z����Y�ʒu</param>
        /// <param name="indexY">��_�Z����X�ʒu</param>
        /// <param name="directionX">�T��������Y�l</param>
        /// <param name="directionY">�T��������X�l</param>
        /// <param name="first">true : �ċN���\�b�h��1�w��</param>
        /// <param name="invert">���C�o���̐�</param>
        /// <returns>true : �}�[�J�[��������</returns>
        bool CheckTurnOverCell(int indexX, int indexY, int directionX, int directionY, StoneColor invert, bool first)
        {
            //�ՖʊO�Ȃ�T���I��
            if (indexX < 0 || indexY < 0 || indexX + 1 > _MapSize.x || indexY + 1 > _MapSize.y)
            {
                return false;
            }
            //�΂��u����Ă��Ȃ���΁A�΂𗎂Ƃ���t���O�𗧂Ă�
            if (_Map[indexX, indexY].StoneColor == StoneColor.None)
            {
                if (!first)
                {
                    _Map[indexX, indexY].IsAbleToDrop = true;
                    return true;
                }
            }
            //�΂����C�o���̐΂Ȃ�A�X�ɒT��
            else if (_Map[indexX, indexY].StoneColor == invert)
            {
                return CheckTurnOverCell(indexX + directionX, indexY + directionY, directionX, directionY, invert, false);
            }

            return false;
        }

        /// <summary>�Ֆʂ��쐬</summary>
        public void CreateMap()
        {
            //�Z���z�u�̏����n�_���w��
            Vector3 basePosition = new Vector3(-(_MapSize.x / 2) * _CellPositionInterval, 0f, -(_MapSize.y / 2) * _CellPositionInterval);
            _Map = new FloorController[_MapSize.x, _MapSize.y];

            for (int x = 0; x < _Map.GetLength(0); x++)
            {
                for (int y = 0; y < _Map.GetLength(1); y++)
                {
                    //�ꏊ����уq�G�����L�[���w�肵�Ď��̉�
                    GameObject cell = Instantiate(_FloorPrefab);
                    cell.transform.position = basePosition + new Vector3(x * _CellPositionInterval, 0f, y * _CellPositionInterval);
                    cell.transform.parent = transform;

                    //�������擾�A���t������
                    FloorController floor = cell.GetComponent<FloorController>();
                    _Map[x, y] = floor;
                    floor.FloorIndex = new Vector2Int(x, y);
                }
            }
        }

        /// <summary>�Ֆʂɍŏ����̐�4��u��</summary>
        public void DropFirstStones()
        {
            Vector2Int centerPos = new Vector2Int(_MapSize.x / 2, _MapSize.y / 2);

            foreach(FloorController fc in _Map)
            {
                fc.ResetCell();
            }

            _Map[centerPos.x, centerPos.y - 1].DropStone(StoneColor.White, true);
            _Map[centerPos.x, centerPos.y].DropStone(StoneColor.Black, true);
            _Map[centerPos.x - 1, centerPos.y].DropStone(StoneColor.White, true);
            _Map[centerPos.x - 1, centerPos.y - 1].DropStone(StoneColor.Black, true);
        }

        /// <summary>�e�΂̐F�̐��𐔂���</summary>
        /// <returns>(���F�̐΂̐�, ���F�̐΂̐�)</returns>
        public (int, int) CountStones()
        {
            int white = 0;
            int black = 0;

            foreach (FloorController fc in _Map)
            {
                switch (fc.StoneColor)
                {
                    case StoneColor.White:
                        white++;
                        break;
                    case StoneColor.Black:
                        black++;
                        break;
                    default: break;
                }
            }

            return (white, black);
        }
    }
}