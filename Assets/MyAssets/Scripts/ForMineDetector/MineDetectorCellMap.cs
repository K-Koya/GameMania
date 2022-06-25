using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MineDetector
{
    /// <summary>�}�C���X�C�[�p�[�I�ȓz�̃Q�[���Ղ𐧌䂷��R���|�[�l���g</summary>
    public class MineDetectorCellMap : MonoBehaviour
    {
        [SerializeField, Tooltip("���e�̐�")]
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


        // Start is called before the first frame update
        void Start()
        {
            Generate();

            //�Q�[���J�n���\�b�h��o�^
            GameStart = BoardInitialize;
        }

        /// <summary>�Q�[���Ղ���ׂ�</summary>
        public void Generate()
        {
            int sizeX = _MapSize.x > _LengthLimit ? _LengthLimit : _MapSize.x;
            int sizeY = _MapSize.y > _LengthLimit ? _LengthLimit : _MapSize.y;

            _Map = new CellController[sizeX, sizeY];

            //true : �n���̐��̕��������̂ŁA�n�����ɋ󔒂�u���Ă������@��
            bool isSetEmpty = (_Map.Length / 2) < _NumberOfMine;

            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    GameObject cell = Instantiate(_CellPrefab);
                    _Map[y, x] = cell.GetComponent<CellController>();
                    cell.transform.parent = transform;
                    cell.transform.position = new Vector3(x * _Spacing, 0f, -y * _Spacing);
                    _Map[y, x].SetContent(new Vector2Int(y, x), isSetEmpty ? CellController.MINE_CONTENT : CellController.EMPTY_CONTENT);
                }
            }
        }

        /// <summary>�Q�[���Ղ̃}�X�̒��g�����</summary>
        /// <param name="clickedX">�n�߂ɃN���b�N���ꂽ�Z���̗�v�f</param>
        /// <param name="clickedY">�n�߂ɃN���b�N���ꂽ�Z���̍s�v�f</param>
        void BoardInitialize(int clickedX, int clickedY)
        {
            _NumberOfMine = Mathf.Clamp(_NumberOfMine, 0, _Map.Length - 1);

            //true�Ȃ�󔒂�ݒ肵�Ă����Afalse�Ȃ�n����ݒ肵�Ă���
            bool isSetEmpty = (_Map.Length / 2) < _NumberOfMine;
            byte setValue = isSetEmpty ? CellController.EMPTY_CONTENT : CellController.MINE_CONTENT;
            byte defaultValue = isSetEmpty ? CellController.MINE_CONTENT : CellController.EMPTY_CONTENT;

            //�S�ċ󔒃Z���̒��ɒn����ݒ� or �S�Ēn���Z���̒��ɋ󔒂�ݒ�
            for (int i = 0; i < _NumberOfMine; i++)
            {
                bool find = false;
                int r = 0;
                int c = 0;
                while (!find)
                {
                    r = Random.Range(0, _Map.GetLength(0));
                    c = Random.Range(0, _Map.GetLength(1));

                    //�N���b�N�����}�X�Ȃ疳��
                    if (r == clickedY && c == clickedX) continue;

                    find = _Map[r, c].Contant == defaultValue;
                }
                _Map[r, c].SetContent(setValue);
            }

            //�S�Ă̋󔒃Z���ɂ��āA���͂̒n���Z���̐��𐔂��A����Β��g�̐��l��ύX
            foreach(CellController cell in _Map)
            {
                //�n���Z���Ȃ疳��
                if (cell.Contant == CellController.MINE_CONTENT) continue;

                byte countOfMine = 0;
                bool isMinRow = cell.Index.y < 1;
                bool isMaxRow = cell.Index.y > _Map.GetLength(0);
                bool isMinCol = cell.Index.x < 1;
                bool isMaxCol = cell.Index.x > _Map.GetLength(1);

                //���と�^�� �c �^�����E���̏��Œ��g������
                if (!isMinRow && !isMinCol && _Map[cell.Index.y - 1, cell.Index.x - 1].Contant == CellController.MINE_CONTENT) countOfMine++;
                if (!isMinRow &&              _Map[cell.Index.y - 1, cell.Index.x].Contant == CellController.MINE_CONTENT) countOfMine++;
                if (!isMinRow && !isMaxCol && _Map[cell.Index.y - 1, cell.Index.x + 1].Contant == CellController.MINE_CONTENT) countOfMine++;
                if (!isMinCol &&              _Map[cell.Index.y, cell.Index.x - 1].Contant == CellController.MINE_CONTENT) countOfMine++;
                if (!isMaxCol &&              _Map[cell.Index.y, cell.Index.x + 1].Contant == CellController.MINE_CONTENT) countOfMine++;
                if (!isMaxRow && !isMinCol && _Map[cell.Index.y + 1, cell.Index.x - 1].Contant == CellController.MINE_CONTENT) countOfMine++;
                if (!isMaxRow &&              _Map[cell.Index.y + 1, cell.Index.x].Contant == CellController.MINE_CONTENT) countOfMine++;
                if (!isMaxRow && !isMaxCol && _Map[cell.Index.y + 1, cell.Index.x + 1].Contant == CellController.MINE_CONTENT) countOfMine++;

                //���Y�Z���ɔ��f
                cell.SetContent(countOfMine);
            }

            //�Q�[���J�n���\�b�h����o�^����
            GameStart = null;
        }
    }
}
