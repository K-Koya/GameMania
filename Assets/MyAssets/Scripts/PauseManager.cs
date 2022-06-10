using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>ポーズ制御コンポーネント</summary>
public class PauseManager : MonoBehaviour
{
    /// <summary>true : 時間停止中</summary>
    static bool _IsTimerStopped = false;

    [SerializeField, Tooltip("true : ポーズ中")]
    bool _IsPaused = false;

    [SerializeField, Tooltip("ボタン名 : ポーズ")]
    string _ButtonNamePause = "Cancel";

    [SerializeField, Tooltip("表示するポーズメニュー")]
    GameObject _PauseMenu = default;


    /// <summary>true : 時間停止中</summary>
    public static bool IsTimerStopped { get => _IsTimerStopped; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ポーズフラグでクロックスピード変更
        if (_IsPaused || _IsTimerStopped)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        //外部からの時間停止要請は受け付ける
        if (_IsTimerStopped) return;

        //ポーズ反転
        if (Input.GetButtonDown(_ButtonNamePause))
        {
            _IsPaused = !_IsPaused;
            _PauseMenu.SetActive(_IsPaused);
        }
    }
}
