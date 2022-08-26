/// <summary>拡張メソッド導入用クラス</summary>
static public partial class Extensions
{
    /// <summary>隣り合い先</summary>
    public enum KindOfNeighbor : byte
    {
        /// <summary>次</summary>
        Next = 0,
        /// <summary>前</summary>
        Previous,

        //以下、二次元配列向け
        /// <summary>左上</summary>
        LeftUp = 10,
        /// <summary>上</summary>
        Up,
        /// <summary>右上</summary>
        RightUp,
        /// <summary>左</summary>
        Left,
        /// <summary>右</summary>
        Right,
        /// <summary>左下</summary>
        LeftDown,
        /// <summary>下</summary>
        Down,
        /// <summary>右下</summary>
        RightDown,
    }

    /// <summary>
    /// 隣り合った二次元配列の要素を取得
    /// </summary>
    /// <typeparam name="T">配列要素の型</typeparam>
    /// <param name="array">該当配列</param>
    /// <param name="kind">どの隣り合いかを指定</param>
    /// <param name="index0">初めの添え字</param>
    /// <param name="index1">次の添え字</param>
    /// <returns>T 指定した隣りの配列要素 bool false:範囲外で存在しない</returns>
    static public (T, bool) Neighbor<T>(this T[,] array, KindOfNeighbor kind, int index0, int index1)
    {
        T t = default;
        bool isExist = true;

        switch (kind)
        {
            case KindOfNeighbor.Next:
                t = array[index0, index1 + 1];
                break;
            case KindOfNeighbor.Previous:
                t = array[index0, index1 - 1];
                break;
            case KindOfNeighbor.LeftUp:
                if (0 < index0 && 0 < index1)
                {
                    t = array[index0 - 1, index1 - 1];
                }
                break;
            case KindOfNeighbor.Up:
                if (0 < index0)
                {
                    t = array[index0 - 1, index1];
                }
                break;
            case KindOfNeighbor.RightUp:
                if (0 < index0 && array.GetLength(1) - 1 > index1)
                {
                    t = array[index0 - 1, index1 + 1];
                }
                break;
            case KindOfNeighbor.Left:
                if (0 < index1)
                {
                    t = array[index0, index1 - 1];
                }
                break;
            case KindOfNeighbor.Right:
                if (array.GetLength(1) - 1 > index1)
                {
                    t = array[index0, index1 + 1];
                }
                break;
            case KindOfNeighbor.LeftDown:
                if (array.GetLength(0) - 1 > index0 && 0 < index1)
                {
                    t = array[index0 + 1, index1 - 1];
                }
                break;
            case KindOfNeighbor.Down:
                if (array.GetLength(0) - 1 > index0)
                {
                    t = array[index0 + 1, index1];
                }
                break;
            case KindOfNeighbor.RightDown:
                if (array.GetLength(0) - 1 > index0 && array.GetLength(1) - 1 > index1)
                {
                    t = array[index0 + 1, index1 + 1];
                }
                break;
            default: break;
        }

        return (t, isExist);
    }
}
