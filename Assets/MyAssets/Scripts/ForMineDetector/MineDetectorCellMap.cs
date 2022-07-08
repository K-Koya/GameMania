using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MineDetector
{
    /// <summary>�}�C���X�C�[�p�[�I�ȓz�̃Q�[���Ղ𐧌䂷��R���|�[�l���g</summary>
    public class MineDetectorCellMap : MonoBehaviour
    {
        /// <summary>�Q�[���Ղ�1�ӂ̃}�X�̐��̍ŏ��l</summary>
        const int _MIN_MAP_LENGTH = 10;

        /// <summary>���e�̍ŏ���</summary>
        const int _MIN_NUMBER_OF_MINE = 10;

        /// <summary>���e��u���Ȃ��}�X�̍ŏ���</summary>
        const int _MIN_NUMBER_OF_SAFE_CELL = 2;

        [SerializeField,Range(_MIN_NUMBER_OF_MINE, 10000) , Tooltip("���e�̐�")]
        int _NumberOfMine = 10;

        [SerializeField, Tooltip("�Q�[���Ղ�1�ӂ̃}�X�̐��̍ő�l")]
        int _LengthLimit = 10000;
        
        [SerializeField, Tooltip("�Q�[���Ղ̉��c��(Length Limit �𒴂���ꍇ�� Length Limit �Ɋۂ߂���)")]
        Vector2Int _MapSize = new Vector2Int(10, 10);

        [SerializeField, Tooltip("�}�X�̊Ԋu")]
        float _Spacing = 1f;

        [SerializeField, Tooltip("�\������Q�[���Ղ̃}�X�̃v���n�u")]
        GameObject _CellPrefab = default;

        /// <summary>�Q�[����</summary>
        CellController[,] _Map = null;

        /// <summary>�Q�[���J�n���\�b�h��ۊ�</summary>
        public static System.Action<int, int> GameStart;

        /// <summary>���̓}�X���J���郁�\�b�h��ۊ�</summary>
        public static System.Action<int, int> CheckAround;


        // Start is called before the first frame update
        void Start()
        {
            Generate();

            //�Q�[���J�n���\�b�h��o�^
            GameStart = BoardInitialize;

            //���̓}�X���`�F�b�N���ĊJ����I�u�W�F�N�g��o�^
            CheckAround = OpenAround;
        }

        /// <summary>�Q�[���Ղ���ׂ�</summary>
        public void Generate()
        {
            //�Q�[���Ղ̍ŏ��l�E�ő�l�w��
            int sizeX = Mathf.Clamp(_MapSize.x, _MIN_MAP_LENGTH, _LengthLimit);
            int sizeY = Mathf.Clamp(_MapSize.y, _MIN_MAP_LENGTH, _LengthLimit);

            //�Q�[���ՃT�C�Y�m��
            _Map = new CellController[sizeY, sizeX];

            //���e���̍ŏ��l�E�ő�l�w��
            _NumberOfMine = Mathf.Clamp(_NumberOfMine, _MIN_NUMBER_OF_MINE, _Map.Length - _MIN_NUMBER_OF_SAFE_CELL);

            //true : ���e�̐��̕��������̂ŁA���e���ɋ󔒂�u���Ă������@��
            bool isSetEmpty = (_Map.Length / 2) < _NumberOfMine;

            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    GameObject cell = Instantiate(_CellPrefab);
                    _Map[y, x] = cell.GetComponent<CellController>();
                    cell.transform.parent = transform;
                    cell.transform.position = new Vector3(x * _Spacing, 0f, -y * _Spacing);
                    _Map[y, x].SetContent(new Vector2Int(x, y), isSetEmpty ? CellController.MINE_CONTENT : CellController.EMPTY_CONTENT);
                }
            }
        }

        /// <summary>�Q�[���Ղ̃}�X�̒��g�����</summary>
        /// <param name="clickedX">�n�߂ɃN���b�N���ꂽ�Z���̗�v�f</param>
        /// <param name="clickedY">�n�߂ɃN���b�N���ꂽ�Z���̍s�v�f</param>
        void BoardInitialize(int clickedX, int clickedY)
        {
            //true�Ȃ�󔒂�ݒ肵�Ă����Afalse�Ȃ�n����ݒ肵�Ă���
            bool isSetEmpty = (_Map.Length / 2) < _NumberOfMine;
            byte setValue = isSetEmpty ? CellController.EMPTY_CONTENT : CellController.MINE_CONTENT;
            byte defaultValue = isSetEmpty ? CellController.MINE_CONTENT : CellController.EMPTY_CONTENT;

            //�S�ċ󔒃Z���̒��ɒn����ݒ� or �S�Ēn���Z���̒��ɋ󔒂�ݒ�
            int loopCount = _NumberOfMine;
            if (isSetEmpty)
            {
                loopCount = _Map.Length - _NumberOfMine - 1;
                _Map[clickedY, clickedX].SetContent(setValue);
            }
            for (int i = 0; i < loopCount; i++)
            {
                bool find = false;
                int r = 0;
                int c = 0;
                while (!find)
                {
                    r = Random.Range(0, _Map.GetLength(0));
                    c = Random.Range(0, _Map.GetLength(1));

                    //�N���b�N�����}�X�Ȃ疳��
                    if (!isSetEmpty && r == clickedY && c == clickedX) continue;

                    find = _Map[r, c].Contant == defaultValue;
                }
                _Map[r, c].SetContent(setValue);
            }

            //�S�Ă̋󔒃Z���ɂ��āA���͂̒n���Z���̐��𐔂��A����Β��g�̐��l��ύX
            foreach(CellController cell in _Map)
            {
                //�n���Z���Ȃ疳��
                if (cell.Contant == CellController.MINE_CONTENT)
                {
                    cell.SetContent(CellController.MINE_CONTENT);
                    continue;
                }
                
                byte countOfMine = 0;
                bool isMinRow = cell.Index.y < 1;
                bool isMaxRow = cell.Index.y > _Map.GetLength(0) - 2;
                bool isMinCol = cell.Index.x < 1;
                bool isMaxCol = cell.Index.x > _Map.GetLength(1) - 2;

                //���と�^�� �c �^�����E���̏��Œ��g������
                if (!isMinRow && !isMinCol && _Map[cell.Index.y - 1, cell.Index.x - 1].Contant == CellController.MINE_CONTENT) countOfMine++;
                if (!isMinRow &&              _Map[cell.Index.y - 1, cell.Index.x    ].Contant == CellController.MINE_CONTENT) countOfMine++;
                if (!isMinRow && !isMaxCol && _Map[cell.Index.y - 1, cell.Index.x + 1].Contant == CellController.MINE_CONTENT) countOfMine++;
                if (             !isMinCol && _Map[cell.Index.y,     cell.Index.x - 1].Contant == CellController.MINE_CONTENT) countOfMine++;
                if (             !isMaxCol && _Map[cell.Index.y,     cell.Index.x + 1].Contant == CellController.MINE_CONTENT) countOfMine++;
                if (!isMaxRow && !isMinCol && _Map[cell.Index.y + 1, cell.Index.x - 1].Contant == CellController.MINE_CONTENT) countOfMine++;
                if (!isMaxRow &&              _Map[cell.Index.y + 1, cell.Index.x    ].Contant == CellController.MINE_CONTENT) countOfMine++;
                if (!isMaxRow && !isMaxCol && _Map[cell.Index.y + 1, cell.Index.x + 1].Contant == CellController.MINE_CONTENT) countOfMine++;

                //���Y�Z���ɔ��f
                cell.SetContent(countOfMine);
            }

            //�Q�[���J�n���\�b�h����o�^����
            GameStart = null;
        }

        /// <summary>from������ӂ̃}�X���J���A�󔒂Ȃ炻�̎��͂̃}�X���J��</summary>
        /// <param name="fromY">�Y���}�X�̏c���W</param>
        /// <param name="fromX">�Y���}�X�̉����W</param>
        void OpenAround(int fromY, int fromX)
        {
            bool isMinRow = fromY < 1;
            bool isMaxRow = fromY > _Map.GetLength(0) - 2;
            bool isMinCol = fromX < 1;
            bool isMaxCol = fromX > _Map.GetLength(1) - 2;

            //���と�^�� �c �^�����E���̏��Œ��g������
            //���̃}�X�����Ă���ΊJ���āA�󔒃}�X�Ȃ炳��ɂ��̎��͂��J����
            if (!isMinRow && !isMinCol)
            {
                _Map[fromY - 1, fromX - 1].OpenMyself();
            }
            if (!isMinRow)
            {
                _Map[fromY - 1, fromX].OpenMyself();
            }
            if (!isMinRow && !isMaxCol)
            {
                _Map[fromY - 1, fromX + 1].OpenMyself();
            }
            if (!isMinCol)
            {
                _Map[fromY, fromX - 1].OpenMyself();
            }
            if (!isMaxCol)
            {
                _Map[fromY, fromX + 1].OpenMyself();
            }
            if (!isMaxRow && !isMinCol)
            {
                _Map[fromY + 1, fromX - 1].OpenMyself();
            }
            if (!isMaxRow)
            {
                _Map[fromY + 1, fromX].OpenMyself();
            }
            if (!isMaxRow && !isMaxCol)
            {
                _Map[fromY + 1, fromX + 1].OpenMyself();
            }
        }


    }
}
