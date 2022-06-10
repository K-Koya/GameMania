using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�|�[�Y����R���|�[�l���g</summary>
public class PauseManager : MonoBehaviour
{
    /// <summary>true : ���Ԓ�~��</summary>
    static bool _IsTimerStopped = false;

    [SerializeField, Tooltip("true : �|�[�Y��")]
    bool _IsPaused = false;

    [SerializeField, Tooltip("�{�^���� : �|�[�Y")]
    string _ButtonNamePause = "Cancel";

    [SerializeField, Tooltip("�\������|�[�Y���j���[")]
    GameObject _PauseMenu = default;


    /// <summary>true : ���Ԓ�~��</summary>
    public static bool IsTimerStopped { get => _IsTimerStopped; }


    // Start is called before the first frame update
    void Start()
    {
        
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
            Time.timeScale = 1f;
        }

        //�O������̎��Ԓ�~�v���͎󂯕t����
        if (_IsTimerStopped) return;

        //�|�[�Y���]
        if (Input.GetButtonDown(_ButtonNamePause))
        {
            _IsPaused = !_IsPaused;
            _PauseMenu.SetActive(_IsPaused);
        }
    }
}
