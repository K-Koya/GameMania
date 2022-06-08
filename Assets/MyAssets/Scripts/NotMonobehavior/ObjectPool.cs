using UnityEngine;
using System;
using System.Linq;

[System.Serializable]
public class ObjectPool<T>
{
    [SerializeField, Tooltip("最大アイテム数")]
    uint _Length = 100;

    /// <summary>アイテム配列</summary>
    T[] _Values = null;

    /// <summary>アイテム配列</summary>
    public T[] Values { get => _Values; }

    /// <summary>アイテム数を管理するクラス</summary>
    /// <param name="length">アイテム数</param>
    public ObjectPool(uint? length = null)
    {
        if (length != null) _Length = (uint)length;
        _Values = new T[_Length];
    }

    /// <summary>現在のプールにアイテム生成の余裕があれば生成</summary>
    /// <param name="instant">生成したいアイテム</param>
    /// <returns>生成したアイテム　生成できなかった場合は型Tのdefault値</returns>
    public T Create(T instant)
    {
        T t = default;
        for(int i = 0; i < _Values.Length; i++)
        {
            if (_Values[i] == null)
            {
                _Values[i] = instant;
                t = _Values[i];
                break;
            }
        }
        return t;
    }

    /// <summary>指定Indexのアイテムを排除</summary>
    /// <param name="index">削除したいアイテムのIndex</param>
    /// <returns>プールから外したアイテムの</returns>
    public T Remove(uint index)
    {
        T t = _Values[index];
        _Values[index] = default;
        return t;
    } 
}
