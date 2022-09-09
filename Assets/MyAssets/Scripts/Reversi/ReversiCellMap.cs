using UnityEngine;

namespace Reversi
{
    public class ReversiCellMap : MonoBehaviour
    {
        /// <summary>シングルトンアクセッサー</summary>
        static ReversiCellMap _Instance = null;

        [SerializeField, Tooltip("ゲーム盤の床を構成するプレハブ")]
        GameObject _FloorPrefab = null;

        [SerializeField, Tooltip("マス同士の中心点間の距離")]
        float _CellPositionInterval = 1.05f;

        [SerializeField, Tooltip("ゲーム盤の縦横マスの数")]
        Vector2Int _MapSize = new Vector2Int(8, 8);

        /// <summary>ゲーム盤情報</summary>
        FloorController[,] _Map = null;


        /// <summary>シングルトンアクセッサー</summary>
        public static ReversiCellMap Instance { get => _Instance; }


        void Awake()
        {
            _Instance = this;
        }

        /// <summary>間に入ったライバルの石を自分のものにひっくりかえす</summary>
        /// <param name="droppedStone">自分の石</param>
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

            /* 周囲マスをチェック */
            //左上
            CheckTurnOverStone(indexX - 1, indexY - 1, -1, -1, droppedStone, invert, true);
            //真上
            CheckTurnOverStone(indexX - 1, indexY, -1, 0, droppedStone, invert, true);
            //右上
            CheckTurnOverStone(indexX - 1, indexY + 1, -1, 1, droppedStone, invert, true);
            //真左
            CheckTurnOverStone(indexX, indexY - 1, 0, -1, droppedStone, invert, true);
            //真右
            CheckTurnOverStone(indexX, indexY + 1, 0, 1, droppedStone, invert, true);
            //左下
            CheckTurnOverStone(indexX + 1, indexY - 1, 1, -1, droppedStone, invert, true);
            //真下
            CheckTurnOverStone(indexX + 1, indexY, 1, 0, droppedStone, invert, true);
            //右下
            CheckTurnOverStone(indexX + 1, indexY + 1, 1, 1, droppedStone, invert, true);

            #region デバッグ
            /*
            string code = "";
            for (int x = 0; x < _Map.GetLength(0); x++)
            {
                for (int y = 0; y < _Map.GetLength(1); y++)
                {
                    string mark = "□";
                    switch (_Map[x, y].StoneColor)
                    {
                        case StoneColor.Black:
                            mark = "●";
                            break;
                        case StoneColor.White:
                            mark = "〇";
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

        /// <summary>基点となるセルからdirection方向にひっくり返せる石が存在しているかを探索して、ひっくり返す</summary>
        /// <param name="indexX">基点セルのY位置</param>
        /// <param name="indexY">基点セルのX位置</param>
        /// <param name="directionX">探索方向のY値</param>
        /// <param name="directionY">探索方向のX値</param>
        /// <param name="dropped">落とした自分の石</param>
        /// <param name="invert">ライバルの石</param>
        /// <param name="first">true : 再起メソッドの1層目</param>
        /// <returns>true : ひっくりかえせる</returns>
        bool CheckTurnOverStone(int indexX, int indexY, int directionX, int directionY, StoneColor dropped, StoneColor invert, bool first)
        {
            //盤面外なら探索終了
            if (indexX < 0 || indexY < 0 || indexX + 1 > _MapSize.y || indexY + 1 > _MapSize.x)
            {
                return false;
            }
            //石が置かれていなければ探索終了
            if (_Map[indexX, indexY].StoneColor == StoneColor.None)
            {
                return false;
            }
            //石がライバルの石なら、更に探索
            else if (_Map[indexX, indexY].StoneColor == invert)
            {
                //探索した上でひっくり返せることが分かればひっくり返す
                if(CheckTurnOverStone(indexX + directionX, indexY + directionY, directionX, directionY, dropped, invert, false))
                {
                    _Map[indexX, indexY].TurnOverStone(dropped);
                    return true;
                }
            }
            //石が自分の石ならひっくり返せるとして離脱
            else if (_Map[indexX, indexY].StoneColor == dropped)
            {
                if (!first) return true;
            }

            return false;
        }

        /// <summary>ひっくりかえせるセルを探す</summary>
        /// <param name="droppedStone">前のターンで落とされた石</param>
        /// <returns>true : ひっくりかえせる</returns>
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

            //ひっくり返せるかの情報を初期化
            foreach (FloorController floor in _Map) floor.IsAbleToDrop = false;

            bool isAbleToTurn = false;
            foreach (FloorController floor in _Map)
            {
                if (floor.StoneColor == invert)
                {
                    /* 周囲マスをチェック */
                    (int, int) current = (floor.FloorIndex.x, floor.FloorIndex.y);
                    //左上
                    isAbleToTurn = CheckTurnOverCell(current.Item1 - 1, current.Item2 - 1, -1, -1, droppedStone, true) || isAbleToTurn;
                    //真上
                    isAbleToTurn = CheckTurnOverCell(current.Item1 - 1, current.Item2, -1, 0, droppedStone, true) || isAbleToTurn;
                    //右上
                    isAbleToTurn = CheckTurnOverCell(current.Item1 - 1, current.Item2 + 1, -1, 1, droppedStone, true) || isAbleToTurn;
                    //真左
                    isAbleToTurn = CheckTurnOverCell(current.Item1, current.Item2 - 1, 0, -1, droppedStone, true) || isAbleToTurn;
                    //真右
                    isAbleToTurn = CheckTurnOverCell(current.Item1, current.Item2 + 1, 0, 1, droppedStone, true) || isAbleToTurn;
                    //左下
                    isAbleToTurn = CheckTurnOverCell(current.Item1 + 1, current.Item2 - 1, 1, -1, droppedStone, true) || isAbleToTurn;
                    //真下
                    isAbleToTurn = CheckTurnOverCell(current.Item1 + 1, current.Item2, 1, 0, droppedStone, true) || isAbleToTurn;
                    //右下
                    isAbleToTurn = CheckTurnOverCell(current.Item1 + 1, current.Item2 + 1, 1, 1, droppedStone, true) || isAbleToTurn;
                }
            }

            return isAbleToTurn;
        }

        /// <summary>基点となるセルからdirection方向にひっくり返せる石が存在しているかを探索して、石を置けるマーカーをかける</summary>
        /// <param name="indexX">基点セルのY位置</param>
        /// <param name="indexY">基点セルのX位置</param>
        /// <param name="directionX">探索方向のY値</param>
        /// <param name="directionY">探索方向のX値</param>
        /// <param name="first">true : 再起メソッドの1層目</param>
        /// <param name="invert">ライバルの石</param>
        /// <returns>true : マーカーをかけた</returns>
        bool CheckTurnOverCell(int indexX, int indexY, int directionX, int directionY, StoneColor invert, bool first)
        {
            //盤面外なら探索終了
            if (indexX < 0 || indexY < 0 || indexX + 1 > _MapSize.x || indexY + 1 > _MapSize.y)
            {
                return false;
            }
            //石が置かれていなければ、石を落とせるフラグを立てる
            if (_Map[indexX, indexY].StoneColor == StoneColor.None)
            {
                if (!first)
                {
                    _Map[indexX, indexY].IsAbleToDrop = true;
                    return true;
                }
            }
            //石がライバルの石なら、更に探索
            else if (_Map[indexX, indexY].StoneColor == invert)
            {
                return CheckTurnOverCell(indexX + directionX, indexY + directionY, directionX, directionY, invert, false);
            }

            return false;
        }

        /// <summary>盤面を作成</summary>
        public void CreateMap()
        {
            //セル配置の初期地点を指定
            Vector3 basePosition = new Vector3(-(_MapSize.x / 2) * _CellPositionInterval, 0f, -(_MapSize.y / 2) * _CellPositionInterval);
            _Map = new FloorController[_MapSize.x, _MapSize.y];

            for (int x = 0; x < _Map.GetLength(0); x++)
            {
                for (int y = 0; y < _Map.GetLength(1); y++)
                {
                    //場所およびヒエラルキーを指定して実体化
                    GameObject cell = Instantiate(_FloorPrefab);
                    cell.transform.position = basePosition + new Vector3(x * _CellPositionInterval, 0f, y * _CellPositionInterval);
                    cell.transform.parent = transform;

                    //床情報を取得、情報付け加え
                    FloorController floor = cell.GetComponent<FloorController>();
                    _Map[x, y] = floor;
                    floor.FloorIndex = new Vector2Int(x, y);
                }
            }
        }

        /// <summary>盤面に最初期の石4つを置く</summary>
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

        /// <summary>各石の色の数を数える</summary>
        /// <returns>(白色の石の数, 黒色の石の数)</returns>
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