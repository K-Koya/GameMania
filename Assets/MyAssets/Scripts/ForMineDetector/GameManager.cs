using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MineDetector
{
    public class GameManager : MonoBehaviour
    {
        /// <summary>�Q�[���̐i�s���</summary>
        static GameState _State = GameState.Standby;

        /// <summary>�Q�[����</summary>
        MineDetectorCellMap _CellMap = default;

        /// <summary>�o�ߎ���</summary>
        static double _Timer = 0f;


        /// <summary>�Q�[���̐i�s���</summary>
        public static GameState State { get => _State; }

        /// <summary>�o�ߎ���</summary>
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
                //�Q�[����
                case GameState.Playing:
                    _Timer += Time.deltaTime;

                    break;
            }
        }

        /// <summary>�V�[�����ă��[�h</summary>
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        /// <summary>�Q�[���X�^�[�g���Ă�����Ă����v��</summary>
        public static void GameStartCall()
        {
            _State = GameState.Playing;
        }

        /// <summary>�Q�[���ɕ��������Ƃɂ�����Ă����v��</summary>
        public static void GameFaultCall()
        {
            _State = GameState.Fault;
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
