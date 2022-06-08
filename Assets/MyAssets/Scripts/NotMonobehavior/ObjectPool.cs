using UnityEngine;
using System;
using System.Linq;

[System.Serializable]
public class ObjectPool<T>
{
    [SerializeField, Tooltip("�ő�A�C�e����")]
    uint _Length = 100;

    /// <summary>�A�C�e���z��</summary>
    T[] _Values = null;

    /// <summary>�A�C�e���z��</summary>
    public T[] Values { get => _Values; }

    /// <summary>�A�C�e�������Ǘ�����N���X</summary>
    /// <param name="length">�A�C�e����</param>
    public ObjectPool(uint? length = null)
    {
        if (length != null) _Length = (uint)length;
        _Values = new T[_Length];
    }

    /// <summary>���݂̃v�[���ɃA�C�e�������̗]�T������ΐ���</summary>
    /// <param name="instant">�����������A�C�e��</param>
    /// <returns>���������A�C�e���@�����ł��Ȃ������ꍇ�͌^T��default�l</returns>
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

    /// <summary>�w��Index�̃A�C�e����r��</summary>
    /// <param name="index">�폜�������A�C�e����Index</param>
    /// <returns>�v�[������O�����A�C�e����</returns>
    public T Remove(uint index)
    {
        T t = _Values[index];
        _Values[index] = default;
        return t;
    } 
}
