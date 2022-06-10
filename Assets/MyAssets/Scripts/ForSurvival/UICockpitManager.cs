using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Survival
{
    /// <summary>��ʏ��pUI�̕\������</summary>
    public class UICockpitManager : MonoBehaviour
    {
        #region �����o
        [SerializeField, Tooltip("�o���l�p���[�^�[�I�u�W�F�N�g")]
        Image _ForExpMeter = default;

        [SerializeField, Tooltip("�v���C���[�̗͕\���p���[�^�[�I�u�W�F�N�g")]
        Image _ForLifeMeter = default;

        [SerializeField, Tooltip("�v���C���[�̃��x���l�\���p�e�L�X�g�I�u�W�F�N�g")]
        TextMeshProUGUI _ForLevel = default;

        [SerializeField, Tooltip("�Q�[���o�ߎ��Ԃ̕\���p�e�L�X�g�I�u�W�F�N�g")]
        TextMeshProUGUI _ForPassageTimer = default;

        [SerializeField, Tooltip("�G�̌��j���̕\���p�e�L�X�g�I�u�W�F�N�g")]
        TextMeshProUGUI _ForDefeatedEnemy = default;

        WaveEnemyManager _WaveEnemyManager = default;
        PlayerStatus _PlayerStatus = default;
        #endregion

        #region �v���p�e�B
        short Level => _PlayerStatus.Level;

        float PlayerLife => _PlayerStatus.Life;

        float PlayerMaxLife => _PlayerStatus.MaxLife;

        float PlayerExp => _PlayerStatus.Exp;

        float PlayerNextLevelExp => _PlayerStatus.NextLevelExp;

        double Timer => _WaveEnemyManager.Timer;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            _WaveEnemyManager = FindObjectOfType<WaveEnemyManager>();
            _PlayerStatus = FindObjectOfType<PlayerStatus>();
        }

        // Update is called once per frame
        void Update()
        {
            _ForLevel.text = $"Lv.{Level}";

            _ForExpMeter.fillAmount = PlayerExp / PlayerNextLevelExp;
            _ForLifeMeter.fillAmount = PlayerLife / PlayerMaxLife;

            int minite = (int)Timer / 60;
            int second = (int)Timer - (minite * 60);
            _ForPassageTimer.text = $"{minite.ToString("000")}:{second.ToString("00")}";
        }
    }
}
