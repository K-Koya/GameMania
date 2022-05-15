using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CellController : MonoBehaviour
{
    /// <summary>地雷を表す数値</summary>
    const byte MINE_POINT = 10;

    [SerializeField, Tooltip("マス目中身（0は空）")]
    byte _Contant = 0;

    /// <summary>蓋情報</summary>
    CoverController _Cover = default;


    public byte Contant { get => _Contant; }
    



    // Start is called before the first frame update
    void Start()
    {
        _Cover = GetComponentInChildren<CoverController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
