using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MineDetector
{
    /// <summary>経過時間をUI表示するコンポーネント</summary>
    public class UITimerController : MonoBehaviour
    {
        [SerializeField, Tooltip("タイマーを表示するテキストUI")]
        TextMeshProUGUI _TimerUI = null;


        // Update is called once per frame
        void Update()
        {
            int minite = (int)GameManager.Timer / 60;
            int second = (int)GameManager.Timer - (minite * 60);
            _TimerUI.text = minite.ToString().PadLeft(3) + ":" + second.ToString().PadLeft(2, '0');
        }
    }
}
