using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�|�[�Y����R���|�[�l���g</summary>
public class PauseManager : MonoBehaviour
{
    /// <summary>true : ���Ԓ�~��</summary>
    static bool _IsTimerStopped = false;

    /// <summary>�|�[�Y�����l�����Ȃ����݂�TimeScale</summary>
    static float _NowTimeScale = 1f; 

    [SerializeField, Tooltip("true : �|�[�Y��")]
    bool _IsPaused = false;

    [SerializeField, Tooltip("�{�^���� : �|�[�Y")]
    string _ButtonNamePause = "Cancel";

    [SerializeField, Tooltip("�\������|�[�Y���j���[")]
    GameObject _PauseMenu = default;


    /// <summary>true : ���Ԓ�~��</summary>
    public static bool IsTimerStopped { get => _IsTimerStopped; set => _IsTimerStopped = value; }
    /// <summary>�|�[�Y�����l�����Ȃ����݂�TimeScale</summary>
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
        //�|�[�Y�t���O�ŃN���b�N�X�s�[�h�ύX
        if (_IsPaused || _IsTimerStopped)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = _NowTimeScale;
        }

        //�O������̎��Ԓ�~�v���͎󂯕t����
        if (_IsTimerStopped) return;

        //�|�[�Y���]
        if (Input.GetButtonDown(_ButtonNamePause))
        {
            PauseOrder();
        }
    }

    void OnDestroy()
    {
        Time.timeScale = 1f;
    }

    //�|�[�Y�w��
    public void PauseOrder()
    {
        _IsPaused = !_IsPaused;
        _PauseMenu.SetActive(_IsPaused);
    }
}
