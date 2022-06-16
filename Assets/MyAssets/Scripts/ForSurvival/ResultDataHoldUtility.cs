using TMPro;
using UnityEngine;

namespace Survival
{
    /// <summary>ゲーム終了結果をまとめ、表示するコンポーネント</summary>
    public class ResultDataHoldUtility : MonoBehaviour
    {
        [SerializeField, Tooltip("結果を表示するTextMeshPro")]
        TextMeshProUGUI[] _TextMeshProUGUI = null;

        [SerializeField, Tooltip("プレイヤーのステータス情報")]
        PlayerStatus _PlayerStatus = null;

        [SerializeField, Tooltip("敵出現ウェーブ管理情報")]
        WaveEnemyManager _WaveEnemyManager = null;

        /// <summary>生存時間</summary>
        static string _SurvivalTime = "";

        /// <summary>倒した数</summary>
        static int _DefeatedEnemyCount = 0;

        /// <summary>到達レベル</summary>
        static short _ReachedLevel = 0;

        void Start()
        {
            if (_TextMeshProUGUI == null || _TextMeshProUGUI.Length == 0) return;
            _TextMeshProUGUI[0].text = _SurvivalTime;
            _TextMeshProUGUI[1].text = _DefeatedEnemyCount.ToString();
            _TextMeshProUGUI[2].text = $"Lv.{_ReachedLevel}";
        }

        /// <summary>このメソッドの呼び出しタイミングにおける結果用パラメータを保管</summary>
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