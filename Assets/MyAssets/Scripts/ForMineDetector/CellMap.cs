using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MineDetector
{
    /// <summary>マインスイーパー的な奴のゲーム盤を制御するコンポーネント</summary>
    public class CellMap : MonoBehaviour
    {
        [SerializeField, Tooltip("構成するゲーム盤のマスのプレハブ")]
        GameObject _CellPrefab = default;

        [SerializeField, Tooltip("ゲーム盤の1辺のマスの数の最大値")]
        int _LengthLimit = 10000;
        
        [SerializeField, Tooltip("ゲーム盤の横縦数(Length Limit を超える場合は Length Limit に丸められる)")]
        Vector2Int _MapSize = new Vector2Int(10, 10);

        [SerializeField, Tooltip("マスの間隔")]
        float _Spacing = 1f;

        /// <summary>ゲーム盤</summary>
        CellController[,] _Map = null;


        // Start is called before the first frame update
        void Start()
        {
            Generate();
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>ゲーム盤を並べる</summary>
        public void Generate()
        {
            int sizeX = _MapSize.x > _LengthLimit ? _LengthLimit : _MapSize.x;
            int sizeY = _MapSize.y > _LengthLimit ? _LengthLimit : _MapSize.y;

            _Map = new CellController[sizeX, sizeY];

            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    GameObject cell = Instantiate(_CellPrefab);
                    _Map[x,y] = cell.GetComponent<CellController>();
                    cell.transform.parent = transform;
                    cell.transform.position = new Vector3(x * _Spacing, 0f, -y * _Spacing);
                }
            }
        }

        /// <summary>ゲーム盤のマスの中身を作る</summary>
        void GameStart()
        {

        }
    }
}
