using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Survival
{
    /// <summary>一般情報用UIの表示制御</summary>
    public class UICockpitManager : MonoBehaviour
    {
        #region メンバ
        [SerializeField, Tooltip("経験値用メーターオブジェクト")]
        Image _ForExpMeter = default;

        [SerializeField, Tooltip("プレイヤー体力表示用メーターオブジェクト")]
        Image _ForLifeMeter = default;

        [SerializeField, Tooltip("プレイヤーのレベル値表示用テキストオブジェクト")]
        TextMeshProUGUI _ForLevel = default;

        [SerializeField, Tooltip("ゲーム経過時間の表示用テキストオブジェクト")]
        TextMeshProUGUI _ForPassageTimer = default;

        [SerializeField, Tooltip("敵の撃破数の表示用テキストオブジェクト")]
        TextMeshProUGUI _ForDefeatedEnemy = default;

        WaveEnemyManager _WaveEnemyManager = default;
        PlayerStatus _PlayerStatus = default;
        #endregion

        #region プロパティ
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
