using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reversi
{
    public class GameManager : MonoBehaviour
    {
        /// <summary>À{’†‚ÌƒQ[ƒ€‚Ìó‹µ</summary>
        static GamePhase _Phase = GamePhase.StandBy;

        /// <summary>ƒQ[ƒ€ŠJn‚ÉÅ‰‚Ì4‚Â‚ÌÎ‚ğ—‚Æ‚·ƒƒ\ƒbƒh‚ğ“o˜^</summary>
        System.Action DropFirstStone = null;


        /// <summary>À{’†‚ÌƒQ[ƒ€‚Ìó‹µ</summary>
        static public GamePhase Phase { get => _Phase; set => _Phase = value; }

        
        /// <summary>ƒQ[ƒ€‚ğŠJn‚·‚é</summary>
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

    /// <summary>À{’†‚ÌƒQ[ƒ€‚Ìó‹µ</summary>
    public enum GamePhase
    {
        StandBy,
        Playing,
        Staying,
    }
}
