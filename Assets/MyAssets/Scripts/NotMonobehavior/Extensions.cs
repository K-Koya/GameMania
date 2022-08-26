/// <summary>�g�����\�b�h�����p�N���X</summary>
static public partial class Extensions
{
    /// <summary>�ׂ荇����</summary>
    public enum KindOfNeighbor : byte
    {
        /// <summary>��</summary>
        Next = 0,
        /// <summary>�O</summary>
        Previous,

        //�ȉ��A�񎟌��z�����
        /// <summary>����</summary>
        LeftUp = 10,
        /// <summary>��</summary>
        Up,
        /// <summary>�E��</summary>
        RightUp,
        /// <summary>��</summary>
        Left,
        /// <summary>�E</summary>
        Right,
        /// <summary>����</summary>
        LeftDown,
        /// <summary>��</summary>
        Down,
        /// <summary>�E��</summary>
        RightDown,
    }

    /// <summary>
    /// �ׂ荇�����񎟌��z��̗v�f���擾
    /// </summary>
    /// <typeparam name="T">�z��v�f�̌^</typeparam>
    /// <param name="array">�Y���z��</param>
    /// <param name="kind">�ǂׂ̗荇�������w��</param>
    /// <param name="index0">���߂̓Y����</param>
    /// <param name="index1">���̓Y����</param>
    /// <returns>T �w�肵���ׂ�̔z��v�f bool false:�͈͊O�ő��݂��Ȃ�</returns>
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
