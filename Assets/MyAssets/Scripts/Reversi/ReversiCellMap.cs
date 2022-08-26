using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reversi
{
    public class ReversiCellMap : MonoBehaviour
    {


        [SerializeField, Tooltip("�Q�[���Ղ̏����\������v���n�u")]
        GameObject _FloorPrefab = null;

        [SerializeField, Tooltip("�}�X���m�̒��S�_�Ԃ̋���")]
        float _CellPositionInterval = 1.05f;

        [SerializeField, Tooltip("�Q�[���Ղ̏c���}�X�̐�")]
        Vector2Int _MapSize = new Vector2Int(8, 8);

        /// <summary>�Q�[���Տ��</summary>
        FloorController[,] _Map = null;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>�Ֆʂ��쐬</summary>
        public void CreateMap()
        {
            //�Z���z�u�̏����n�_���w��
            Vector3 basePosition = new Vector3(-(_MapSize.x / 2) * _CellPositionInterval, 0f, -(_MapSize.y / 2) * _CellPositionInterval);
            _Map = new FloorController[_MapSize.y, _MapSize.x];

            for(int y = 0; y < _Map.GetLength(0); y++)
            {
                for(int x = 0; x < _Map.GetLength(1); x++)
                {
                    //�ꏊ����уq�G�����L�[���w�肵�Ď��̉�
                    GameObject cell = Instantiate(_FloorPrefab);
                    cell.transform.position = basePosition + new Vector3(y * _CellPositionInterval, 0f, x * _CellPositionInterval);
                    cell.transform.parent = transform;

                    //�������擾�A���t������
                    FloorController floor = cell.GetComponent<FloorController>();
                    _Map[y, x] = floor;
                    floor.FloorIndex = new Vector2Int(y, x);
                }
            }
        }

        /// <summary>�Ֆʂɍŏ����̐�4��u��</summary>
        public void DropFirstStones()
        {
            Vector2Int centerPos = new Vector2Int(_MapSize.x / 2, _MapSize.y / 2);

            _Map[centerPos.y, centerPos.x].DropStone(StoneColor.Black);
            _Map[centerPos.y - 1, centerPos.x - 1].DropStone(StoneColor.Black);
            _Map[centerPos.y, centerPos.x - 1].DropStone(StoneColor.White);
            _Map[centerPos.y - 1, centerPos.x].DropStone(StoneColor.White);
        }
    }
}