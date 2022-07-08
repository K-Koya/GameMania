using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MineDetector
{
    public class GameManager : MonoBehaviour
    {
        /// <summary>ゲームの進行状態</summary>
        static GameState _State = GameState.Standby;

        /// <summary>ゲーム盤</summary>
        MineDetectorCellMap _CellMap = default;

        /// <summary>経過時間</summary>
        static double _Timer = 0f;


        /// <summary>ゲームの進行状態</summary>
        public static GameState State { get => _State; }

        /// <summary>経過時間</summary>
        public static double Timer { get => _Timer; }



        // Start is called before the first frame update
        void Start()
        {
            _CellMap = GetComponent<MineDetectorCellMap>();
            _State = GameState.Standby;
            _Timer = 0f;
        }

        // Update is called once per frame
        void Update()
        {
            switch (_State)
            {
                //ゲーム中
                case GameState.Playing:
                    _Timer += Time.deltaTime;

                    break;
            }
        }

        /// <summary>シーンを再ロード</summary>
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        /// <summary>ゲームスタートしてくれっていう要求</summary>
        public static void GameStartCall()
        {
            _State = GameState.Playing;
        }

        /// <summary>ゲームに負けたことにしろっていう要求</summary>
        public static void GameFaultCall()
        {
            _State = GameState.Fault;
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
