using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>ポーズ制御コンポーネント</summary>
public class PauseManager : MonoBehaviour
{
    /// <summary>true : 時間停止中</summary>
    static bool _IsTimerStopped = false;

    /// <summary>ポーズ等を考慮しない現在のTimeScale</summary>
    static float _NowTimeScale = 1f; 

    [SerializeField, Tooltip("true : ポーズ中")]
    bool _IsPaused = false;

    [SerializeField, Tooltip("ボタン名 : ポーズ")]
    string _ButtonNamePause = "Cancel";

    [SerializeField, Tooltip("表示するポーズメニュー")]
    GameObject _PauseMenu = default;


    /// <summary>true : 時間停止中</summary>
    public static bool IsTimerStopped { get => _IsTimerStopped; set => _IsTimerStopped = value; }
    /// <summary>ポーズ等を考慮しない現在のTimeScale</summary>
    public static float NowTimeScale { get => _NowTimeScale; set => _NowTimeScale = value; }


    // Start is called before the first frame update
    void Start()
    {
        _IsTimerStopped = false;
        _NowTimeScale = 1f;
        Time.timeScale = 1f;
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
            Time.timeScale = _NowTimeScale;
        }

        //外部からの時間停止要請は受け付ける
        if (_IsTimerStopped) return;

        //ポーズ反転
        if (Input.GetButtonDown(_ButtonNamePause))
        {
            PauseOrder();
        }
    }

    void OnDestroy()
    {
        Time.timeScale = 1f;
    }

    //ポーズ指示
    public void PauseOrder()
    {
        _IsPaused = !_IsPaused;
        _PauseMenu.SetActive(_IsPaused);
    }
}
