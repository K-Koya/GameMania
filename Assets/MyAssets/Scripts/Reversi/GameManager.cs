using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Reversi
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField, Tooltip("�ǂ���̔Ԃ���\������GUI")]
        TextMeshProUGUI _TurnText = null;

        [SerializeField, Tooltip("���s���ʕ\���pGUI")]
        TextMeshProUGUI _ResultText = null;

        /// <summary>�V���O���g���A�N�Z�b�T�[</summary>
        static GameManager instance = null;

        /// <summary>�^�[���̑Ώ�</summary>
        StoneColor _Turn = StoneColor.White;

        [SerializeField, Tooltip("���{���̃Q�[���̏�")]
        GamePhase _Phase = GamePhase.StandBy;

        /// <summary>�Q�[���J�n���ɍŏ���4�̐΂𗎂Ƃ����\�b�h��o�^</summary>
        System.Action DropFirstStone = null;


        /// <summary>�^�[���̑Ώ�</summary>
        public StoneColor Turn { get => _Turn; set => _Turn = value; }
        /// <summary>���{���̃Q�[���̏�</summary>
        public GamePhase Phase { get => _Phase; set => _Phase = value; }
        /// <summary>�V���O���g���A�N�Z�b�T�[</summary>
        public static GameManager Instance { get => instance; set => instance = value; }
        

        void Awake()
        {
            instance = this;
        }

        /// <summary>�Q�[�����J�n����</summary>
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

        /// <summary>�^�[���Ώۂ𔽓]</summary>
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

    /// <summary>���{���̃Q�[���̏�</summary>
    public enum GamePhase
    {
        StandBy,
        Playing,
        Staying,
        Ending
    }
}
