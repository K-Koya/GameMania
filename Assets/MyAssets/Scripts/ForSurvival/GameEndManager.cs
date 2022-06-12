using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Survival
{
    public class GameEndManager : MonoBehaviour
    {
        /// <summary>プレイヤー</summary>
        PlayerStatus _Player = null;

        /// <summary>true : ゲーム終了</summary>
        bool _IsGameEnd = false;

        /// <summary>true : ゲームオーバー演出を起動した</summary>
        bool _IsRunGameoverDirecting = false;

        [SerializeField, Tooltip("ゲームオーバー時に実行したいメソッドをアサイン")]
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