using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reversi
{
    public class GameManager : MonoBehaviour
    {
        /// <summary>���{���̃Q�[���̏�</summary>
        static GamePhase _Phase = GamePhase.StandBy;

        /// <summary>�Q�[���J�n���ɍŏ���4�̐΂𗎂Ƃ����\�b�h��o�^</summary>
        System.Action DropFirstStone = null;


        /// <summary>���{���̃Q�[���̏�</summary>
        static public GamePhase Phase { get => _Phase; set => _Phase = value; }

        
        /// <summary>�Q�[�����J�n����</summary>
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

    /// <summary>���{���̃Q�[���̏�</summary>
    public enum GamePhase
    {
        StandBy,
        Playing,
        Staying,
    }
}
