using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MineDetector
{
    /// <summary>マインスイーパー的な奴のゲーム盤を制御するコンポーネント</summary>
    public class MineDetectorCellMap : MonoBehaviour
    {
        /// <summary>ゲーム盤の1辺のマスの数の最小値</summary>
        const int _MIN_MAP_LENGTH = 10;

        /// <summary>爆弾の最小数</summary>
        const int _MIN_NUMBER_OF_MINE = 10;

        /// <summary>爆弾を置かないマスの最小数</summary>
        const int _MIN_NUMBER_OF_SAFE_CELL = 2;

        [SerializeField,Range(_MIN_NUMBER_OF_MINE, 10000) , Tooltip("爆弾の数")]
        int _NumberOfMine = 10;

        [SerializeField, Tooltip("ゲーム盤の1辺のマスの数の最大値")]
        int _LengthLimit = 10000;
        
        [SerializeField, Tooltip("ゲーム盤の横縦数(Length Limit を超える場合は Length Limit に丸められる)")]
        Vector2Int _MapSize = new Vector2Int(10, 10);

        [SerializeField, Tooltip("マスの間隔")]
        float _Spacing = 1f;

        [SerializeField, Tooltip("構成するゲーム盤のマスのプレハブ")]
        GameObject _CellPrefab = default;

        /// <summary>ゲーム盤</summary>
        CellController[,] _Map = null;

        /// <summary>ゲーム開始メソッドを保管</summary>
        public static System.Action<int, int> GameStart;

        /// <summary>周囲マスを開けるメソッドを保管</summary>
        public static System.Action<int, int> CheckAround;


        // Start is called before the first frame update
        void Start()
        {
            Generate();

            //ゲーム開始メソッドを登録
            GameStart = BoardInitialize;

            //周囲マスをチェックして開けるオブジェクトを登録
            CheckAround = OpenAround;
        }

        /// <summary>ゲーム盤を並べる</summary>
        public void Generate()
        {
            //ゲーム盤の最小値・最大値指定
            int sizeX = Mathf.Clamp(_MapSize.x, _MIN_MAP_LENGTH, _LengthLimit);
            int sizeY = Mathf.Clamp(_MapSize.y, _MIN_MAP_LENGTH, _LengthLimit);

            //ゲーム盤サイズ確定
            _Map = new CellController[sizeY, sizeX];

            //爆弾数の最小値・最大値指定
            _NumberOfMine = Mathf.Clamp(_NumberOfMine, _MIN_NUMBER_OF_MINE, _Map.Length - _MIN_NUMBER_OF_SAFE_CELL);

            //true : 爆弾の数の方が多いので、爆弾原に空白を置いていく方法で
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

        /// <summary>ゲーム盤のマスの中身を作る</summary>
        /// <param name="clickedX">始めにクリックされたセルの列要素</param>
        /// <param name="clickedY">始めにクリックされたセルの行要素</param>
        void BoardInitialize(int clickedX, int clickedY)
        {
            //trueなら空白を設定していく、falseなら地雷を設定していく
            bool isSetEmpty = (_Map.Length / 2) < _NumberOfMine;
            byte setValue = isSetEmpty ? CellController.EMPTY_CONTENT : CellController.MINE_CONTENT;
            byte defaultValue = isSetEmpty ? CellController.MINE_CONTENT : CellController.EMPTY_CONTENT;

            //全て空白セルの中に地雷を設定 or 全て地雷セルの中に空白を設定
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

                    //クリックしたマスなら無視
                    if (!isSetEmpty && r == clickedY && c == clickedX) continue;

                    find = _Map[r, c].Contant == defaultValue;
                }
                _Map[r, c].SetContent(setValue);
            }

            //全ての空白セルについて、周囲の地雷セルの数を数え、あれば中身の数値を変更
            foreach(CellController cell in _Map)
            {
                //地雷セルなら無視
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

                //左上→真上 … 真下→右下の順で中身を見る
                if (!isMinRow && !isMinCol && _Map[cell.Index.y - 1, cell.Index.x - 1].Contant == CellController.MINE_CONTENT) countOfMine++;
                if (!isMinRow &&              _Map[cell.Index.y - 1, cell.Index.x    ].Contant == CellController.MINE_CONTENT) countOfMine++;
                if (!isMinRow && !isMaxCol && _Map[cell.Index.y - 1, cell.Index.x + 1].Contant == CellController.MINE_CONTENT) countOfMine++;
                if (             !isMinCol && _Map[cell.Index.y,     cell.Index.x - 1].Contant == CellController.MINE_CONTENT) countOfMine++;
                if (             !isMaxCol && _Map[cell.Index.y,     cell.Index.x + 1].Contant == CellController.MINE_CONTENT) countOfMine++;
                if (!isMaxRow && !isMinCol && _Map[cell.Index.y + 1, cell.Index.x - 1].Contant == CellController.MINE_CONTENT) countOfMine++;
                if (!isMaxRow &&              _Map[cell.Index.y + 1, cell.Index.x    ].Contant == CellController.MINE_CONTENT) countOfMine++;
                if (!isMaxRow && !isMaxCol && _Map[cell.Index.y + 1, cell.Index.x + 1].Contant == CellController.MINE_CONTENT) countOfMine++;

                //当該セルに反映
                cell.SetContent(countOfMine);
            }

            //ゲーム開始メソッドから登録解除
            GameStart = null;
        }

        /// <summary>fromから周辺のマスを開き、空白ならその周囲のマスも開く</summary>
        /// <param name="fromY">該当マスの縦座標</param>
        /// <param name="fromX">該当マスの横座標</param>
        void OpenAround(int fromY, int fromX)
        {
            bool isMinRow = fromY < 1;
            bool isMaxRow = fromY > _Map.GetLength(0) - 2;
            bool isMinCol = fromX < 1;
            bool isMaxCol = fromX > _Map.GetLength(1) - 2;

            //左上→真上 … 真下→右下の順で中身を見る
            //そのマスが閉じていれば開けて、空白マスならさらにその周囲を開ける
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
