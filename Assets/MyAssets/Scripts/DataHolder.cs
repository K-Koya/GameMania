using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>シーンをまたぐ等、データを保持するクラス</summary>
public static class DataHolder
{
    /// <summary>データテーブル</summary>
    public static Dictionary<string, object> DataTable = new Dictionary<string, object>();

    /// <summary>データを全て除去</summary>
    public static void RemoveAll()
    {
        DataTable.Clear();
        DataTable = new Dictionary<string, object>();
    }

    /// <summary>データをハッシュ値により取得</summary>
    /// <param name="hash">ハッシュ値</param>
    /// <param name="isGetToRemove">true : 取得直後に破棄する</param>
    /// <returns>取得したオブジェクト</returns>
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
