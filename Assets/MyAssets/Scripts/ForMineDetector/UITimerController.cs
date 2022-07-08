using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MineDetector
{
    /// <summary>�o�ߎ��Ԃ�UI�\������R���|�[�l���g</summary>
    public class UITimerController : MonoBehaviour
    {
        [SerializeField, Tooltip("�^�C�}�[��\������e�L�X�gUI")]
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
