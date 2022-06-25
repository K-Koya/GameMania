using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MineDetector
{
    public class GameManager : MonoBehaviour
    {
        /// <summary>�Q�[���̐i�s���</summary>
        static GameState _State = GameState.Standby;

        /// <summary>�Q�[����</summary>
        MineDetectorCellMap _CellMap = default;


        /// <summary>�Q�[���̐i�s���</summary>
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

    /// <summary>�Q�[���̐i�s���</summary>
    public enum GameState
    {
        /// <summary>�Q�[���J�n�O</summary>
        Standby = 0,
        /// <summary>�Q�[����</summary>
        Playing,
        /// <summary>�Q�[������</summary>
        Fault,
        /// <summary>�Q�[������</summary>
        Clear,
    }
}
