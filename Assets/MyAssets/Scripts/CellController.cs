using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CellController : MonoBehaviour
{
    /// <summary>�n����\�����l</summary>
    const byte MINE_POINT = 10;

    [SerializeField, Tooltip("�}�X�ڒ��g�i0�͋�j")]
    byte _Contant = 0;

    /// <summary>�W���</summary>
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
