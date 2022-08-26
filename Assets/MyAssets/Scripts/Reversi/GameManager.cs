using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reversi
{
    public class GameManager : MonoBehaviour
    {
        /// <summary>実施中のゲームの状況</summary>
        static GamePhase _Phase = GamePhase.StandBy;

        /// <summary>ゲーム開始時に最初の4つの石を落とすメソッドを登録</summary>
        System.Action DropFirstStone = null;


        /// <summary>実施中のゲームの状況</summary>
        static public GamePhase Phase { get => _Phase; set => _Phase = value; }

        
        /// <summary>ゲームを開始する</summary>
        public void StartGame()
        {
            if(_Phase == GamePhase.StandBy)
            {
                DropFirstStone();
                _Phase = GamePhase.Playing;
            }
        }
        

        // Start is called before the first frame update
        void Start()
        {
            ReversiCellMap cellMap = FindObjectOfType<ReversiCellMap>();
            DropFirstStone = cellMap.DropFirstStones;

            cellMap.CreateMap();

            _Phase = GamePhase.StandBy;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

    /// <summary>実施中のゲームの状況</summary>
    public enum GamePhase
    {
        StandBy,
        Playing,
        Staying,
    }
}
