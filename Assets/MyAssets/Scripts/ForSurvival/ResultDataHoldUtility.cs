using TMPro;
using UnityEngine;

namespace Survival
{
    /// <summary>�Q�[���I�����ʂ��܂Ƃ߁A�\������R���|�[�l���g</summary>
    public class ResultDataHoldUtility : MonoBehaviour
    {
        [SerializeField, Tooltip("���ʂ�\������TextMeshPro")]
        TextMeshProUGUI[] _TextMeshProUGUI = null;

        [SerializeField, Tooltip("�v���C���[�̃X�e�[�^�X���")]
        PlayerStatus _PlayerStatus = null;

        [SerializeField, Tooltip("�G�o���E�F�[�u�Ǘ����")]
        WaveEnemyManager _WaveEnemyManager = null;

        /// <summary>��������</summary>
        static string _SurvivalTime = "";

        /// <summary>�|������</summary>
        static int _DefeatedEnemyCount = 0;

        /// <summary>���B���x��</summary>
        static short _ReachedLevel = 0;

        void Start()
        {
            if (_TextMeshProUGUI == null || _TextMeshProUGUI.Length == 0) return;
            _TextMeshProUGUI[0].text = _SurvivalTime;
            _TextMeshProUGUI[1].text = _DefeatedEnemyCount.ToString();
            _TextMeshProUGUI[2].text = $"Lv.{_ReachedLevel}";
        }

        /// <summary>���̃��\�b�h�̌Ăяo���^�C�~���O�ɂ����錋�ʗp�p�����[�^��ۊ�</summary>
        public void SetData()
        {
            int minite = (int)_WaveEnemyManager.Timer / 60;
            int second = (int)_WaveEnemyManager.Timer - (minite * 60);
            _SurvivalTime = minite.ToString().PadLeft(3) + ":" + second.ToString().PadLeft(2, '0');

            _DefeatedEnemyCount = WaveEnemyManager.DefeatedEnemyCount;
            _ReachedLevel = _PlayerStatus.Level;
        }
    }
}