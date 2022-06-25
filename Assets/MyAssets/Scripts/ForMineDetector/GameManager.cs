using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MineDetector
{
    public class GameManager : MonoBehaviour
    {
        /// <summary>ゲームの進行状態</summary>
        static GameState _State = GameState.Standby;

        /// <summary>ゲーム盤</summary>
        MineDetectorCellMap _CellMap = default;


        /// <summary>ゲームの進行状態</summary>
        public static GameState State { get => _State; set => _State = value; }



        // Start is called before the first frame update
        void Start()
        {
            _CellMap = GetComponent<MineDetectorCellMap>();
            _State = GameState.Standby;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

    /// <summary>ゲームの進行状態</summary>
    public enum GameState
    {
        /// <summary>ゲーム開始前</summary>
        Standby = 0,
        /// <summary>ゲーム中</summary>
        Playing,
        /// <summary>ゲーム負け</summary>
        Fault,
        /// <summary>ゲーム勝ち</summary>
        Clear,
    }
}
