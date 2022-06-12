using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Survival
{
    public class GameEndManager : MonoBehaviour
    {
        /// <summary>�v���C���[</summary>
        PlayerStatus _Player = null;

        /// <summary>true : �Q�[���I��</summary>
        bool _IsGameEnd = false;

        /// <summary>true : �Q�[���I�[�o�[���o���N������</summary>
        bool _IsRunGameoverDirecting = false;

        [SerializeField, Tooltip("�Q�[���I�[�o�[���Ɏ��s���������\�b�h���A�T�C��")]
        UnityEvent _ForGameover = null;


        // Start is called before the first frame update
        void Start()
        {
            _Player = FindObjectOfType<PlayerStatus>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_IsGameEnd && !_IsRunGameoverDirecting)
            {
                StartCoroutine(GameoverDirecting());
                _IsRunGameoverDirecting = true;
            }
            else
            {
                _IsGameEnd = !_Player.gameObject.activeSelf;
            }
        }


        IEnumerator GameoverDirecting()
        {
            PauseManager.NowTimeScale = 0.1f;
            yield return new WaitForSeconds(0.3f);

            PauseManager.NowTimeScale = 1f;
            yield return new WaitForSeconds(2f);

            PauseManager.IsTimerStopped = true;
            _ForGameover.Invoke();
        }

    }
}