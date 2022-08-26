using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reversi
{
    public class ReversiCellMap : MonoBehaviour
    {


        [SerializeField, Tooltip("ゲーム盤の床を構成するプレハブ")]
        GameObject _FloorPrefab = null;

        [SerializeField, Tooltip("マス同士の中心点間の距離")]
        float _CellPositionInterval = 1.05f;

        [SerializeField, Tooltip("ゲーム盤の縦横マスの数")]
        Vector2Int _MapSize = new Vector2Int(8, 8);

        /// <summary>ゲーム盤情報</summary>
        FloorController[,] _Map = null;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>盤面を作成</summary>
        public void CreateMap()
        {
            //セル配置の初期地点を指定
            Vector3 basePosition = new Vector3(-(_MapSize.x / 2) * _CellPositionInterval, 0f, -(_MapSize.y / 2) * _CellPositionInterval);
            _Map = new FloorController[_MapSize.y, _MapSize.x];

            for(int y = 0; y < _Map.GetLength(0); y++)
            {
                for(int x = 0; x < _Map.GetLength(1); x++)
                {
                    //場所およびヒエラルキーを指定して実体化
                    GameObject cell = Instantiate(_FloorPrefab);
                    cell.transform.position = basePosition + new Vector3(y * _CellPositionInterval, 0f, x * _CellPositionInterval);
                    cell.transform.parent = transform;

                    //床情報を取得、情報付け加え
                    FloorController floor = cell.GetComponent<FloorController>();
                    _Map[y, x] = floor;
                    floor.FloorIndex = new Vector2Int(y, x);
                }
            }
        }

        /// <summary>盤面に最初期の石4つを置く</summary>
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