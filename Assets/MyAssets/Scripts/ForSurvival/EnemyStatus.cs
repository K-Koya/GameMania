using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    /// <summary>敵の攻撃や体力処理</summary>
    public class EnemyStatus : MonoBehaviour
    {
        [SerializeField, Tooltip("体力")]
        float _Life = 10f;

        [SerializeField, Tooltip("倒したときにプレイヤーに与えられる経験値量")]
        short _GiveExp = 1;

        [SerializeField, Tooltip("レイヤー名 : プレイヤー")]
        string _LayerNamePlayer = "Player";

        [SerializeField, Tooltip("タグ名 : プレイヤーの攻撃")]
        string _TagNamePlayerAttack = "PlayerAttack";

        /// <summary>レイヤー番号 : プレイヤー</summary>
        int _LayerIndexPlayer = -1;

        // Start is called before the first frame update
        void Start()
        {
            _LayerIndexPlayer = LayerMask.NameToLayer(_LayerNamePlayer);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            //プレイヤーの攻撃に接触するとダメージ
            if (collision.gameObject.layer == _LayerIndexPlayer && collision.gameObject.CompareTag(_TagNamePlayerAttack))
            {
                EnemyAttack attack = collision.gameObject.GetComponent<EnemyAttack>();
                _Life -= attack.PowerOnEnter;
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (PauseManager.IsTimerStopped) return;

            //プレイヤーの攻撃に接触し続ける間ダメージ
            if (collision.gameObject.layer == _LayerIndexPlayer && collision.gameObject.CompareTag(_TagNamePlayerAttack))
            {
                EnemyAttack attack = collision.gameObject.GetComponent<EnemyAttack>();
                _Life -= attack.PowerOnStay;
            }
        }
    }
}
