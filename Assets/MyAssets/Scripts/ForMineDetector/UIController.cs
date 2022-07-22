using TMPro;
using UnityEngine;

namespace MineDetector
{
    /// <summary>�o�ߎ��Ԃ�UI�\������R���|�[�l���g</summary>
    public class UIController : MonoBehaviour
    {
        /// <summary>�Q�[�����N���A�������̃��b�Z�[�W</summary>
        const string GAME_CLEAR_MESSAGE = "Cleared";

        /// <summary>�Q�[���ɕ��������̃��b�Z�[�W</summary>
        const string GAME_FAULT_MESSAGE = "Gameover";

        [SerializeField, Tooltip("�^�C�}�[��\������e�L�X�gUI")]
        TextMeshProUGUI _TimerUI = null;

        [SerializeField, Tooltip("���e�̑�����\������e�L�X�gUI")]
        TextMeshProUGUI _NumberOfMineUI = null;

        [SerializeField, Tooltip("���e�̑�����\������e�L�X�gUI")]
        TextMeshProUGUI _NumberOfFlagUI = null;

        [SerializeField, Tooltip("�Q�[���̌��ʂ�\������e�L�X�gUI")]
        TextMeshProUGUI _ResultMessage = null;

        /// <summary>�Q�[���𐧌䂷�郄�c</summary>
        GameManager _GameManager = null;


        void Start()
        {
            _GameManager = FindObjectOfType<GameManager>();
            if (_ResultMessage) _ResultMessage.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            switch (_GameManager.State)
            {
                case GameState.Standby:
                case GameState.Playing:
                    int minite = (int)_GameManager.Timer / 60;
                    int second = (int)_GameManager.Timer - (minite * 60);
                    if (_TimerUI) _TimerUI.text = minite.ToString().PadLeft(3) + ":" + second.ToString().PadLeft(2, '0');
                    if (_NumberOfMineUI) _NumberOfMineUI.text = _GameManager.NumberOfMine.ToString();
                    if (_NumberOfFlagUI) _NumberOfFlagUI.text = _GameManager.NumberOfFlag.ToString();
                    break;
                case GameState.Clear:
                    if (_ResultMessage)
                    {
                        _ResultMessage.gameObject.SetActive(true);
                        _ResultMessage.text = GAME_CLEAR_MESSAGE;
                    }
                    break;
                case GameState.Fault:
                    if (_ResultMessage)
                    {
                        _ResultMessage.gameObject.SetActive(true);
                        _ResultMessage.text = GAME_FAULT_MESSAGE;
                    }
                    break;
            }
        }
    }
}
