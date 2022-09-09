using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Reversi
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField, Tooltip("どちらの番かを表示するGUI")]
        TextMeshProUGUI _TurnText = null;

        [SerializeField, Tooltip("勝敗結果表示用GUI")]
        TextMeshProUGUI _ResultText = null;

        /// <summary>シングルトンアクセッサー</summary>
        static GameManager instance = null;

        /// <summary>ターンの対象</summary>
        StoneColor _Turn = StoneColor.White;

        [SerializeField, Tooltip("実施中のゲームの状況")]
        GamePhase _Phase = GamePhase.StandBy;

        /// <summary>ゲーム開始時に最初の4つの石を落とすメソッドを登録</summary>
        System.Action DropFirstStone = null;


        /// <summary>ターンの対象</summary>
        public StoneColor Turn { get => _Turn; set => _Turn = value; }
        /// <summary>実施中のゲームの状況</summary>
        public GamePhase Phase { get => _Phase; set => _Phase = value; }
        /// <summary>シングルトンアクセッサー</summary>
        public static GameManager Instance { get => instance; set => instance = value; }
        

        void Awake()
        {
            instance = this;
        }

        /// <summary>ゲームを開始する</summary>
        public void StartGame()
        {
            switch (_Phase)
            {
                case GamePhase.StandBy:
                    DropFirstStone();
                    _TurnText?.gameObject.SetActive(true);
                    _Phase = GamePhase.Playing;
                    break;
                case GamePhase.Playing:
                case GamePhase.Ending:
                    _ResultText?.gameObject.SetActive(false);
                    _TurnText?.gameObject.SetActive(true);
                    DropFirstStone();
                    _Turn = StoneColor.White;
                    break;
            }
        }

        /// <summary>ターン対象を反転</summary>
        public void SwitchTurn()
        {
            switch (_Turn)
            {
                case StoneColor.White:
                    _Turn = StoneColor.Black;
                    _TurnText.color = Color.black;
                    break;
                case StoneColor.Black:
                    _Turn = StoneColor.White;
                    _TurnText.color = Color.white;
                    break;
            }
        }
        

        // Start is called before the first frame update
        void Start()
        {
            ReversiCellMap cellMap = FindObjectOfType<ReversiCellMap>();
            DropFirstStone = cellMap.DropFirstStones;

            cellMap.CreateMap();

            _Phase = GamePhase.StandBy;

            _ResultText?.gameObject.SetActive(false);
            _TurnText?.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            switch (_Phase)
            {
                case GamePhase.Playing:

                    if (_TurnText)
                    {
                        _TurnText.text = $"{_Turn}'s Turn";
                    }

                    break;
                case GamePhase.Ending:

                    if (_ResultText)
                    {
                        _ResultText.gameObject.SetActive(true);
                        var counter = ReversiCellMap.Instance.CountStones();

                        if (counter.Item1 > counter.Item2)
                        {
                            _ResultText.color = Color.black;
                            _ResultText.text = "White Win!!";
                        }
                        else if (counter.Item1 < counter.Item2)
                        {
                            _ResultText.color = Color.white;
                            _ResultText.text = "Black Win!!";
                        }
                        else
                        {
                            _ResultText.color = Color.gray;
                            _ResultText.text = "Draw...";
                        }

                        _TurnText?.gameObject.SetActive(false);
                    }

                    break;
            }
        }
    }

    /// <summary>実施中のゲームの状況</summary>
    public enum GamePhase
    {
        StandBy,
        Playing,
        Staying,
        Ending
    }
}
