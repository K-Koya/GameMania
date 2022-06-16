using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�V�[�����܂������A�f�[�^��ێ�����N���X</summary>
public static class DataHolder
{
    /// <summary>�f�[�^�e�[�u��</summary>
    public static Dictionary<string, object> DataTable = new Dictionary<string, object>();

    /// <summary>�f�[�^��S�ď���</summary>
    public static void RemoveAll()
    {
        DataTable.Clear();
        DataTable = new Dictionary<string, object>();
    }

    /// <summary>�f�[�^���n�b�V���l�ɂ��擾</summary>
    /// <param name="hash">�n�b�V���l</param>
    /// <param name="isGetToRemove">true : �擾����ɔj������</param>
    /// <returns>�擾�����I�u�W�F�N�g</returns>
    public static object GetByHash(string hash, bool isGetToRemove = true)
    {
        object obj = null;
        if (DataTable.TryGetValue(hash, out obj) && isGetToRemove)
        {
            DataTable.Remove(hash);
        }
        return obj;
    }
}
