using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    /// <summary>プレイヤーの攻撃や体力処理</summary>
    public class PlayerStatus : MonoBehaviour
    {
        #region メンバ
        [SerializeField, Tooltip("プレイヤーの現在レベル")]
        short _Level = 1;

        [SerializeField, Tooltip("体力")]
        float _Life = 100f;

        [SerializeField, Tooltip("体力最大値")]
        float _MaxLife = 100f;

        [SerializeField, Tooltip("取得経験値量")]
        int _Exp = 0;

        [SerializeField, Tooltip("次のレベルアップまでに必要な経験値量")]
        int _NextLevelExp = 6;

        [SerializeField, Tooltip("レイヤー名 : 敵")]
        string _LayerNameEnemy = "Enemy";

        /// <summary>レイヤー番号 : 敵</summary>
        int _LayerIndexEnemy = -1;
        #endregion

        #region プロパティ
        /// <summary>体力</summary>
        public float Life { get => _Life; }
        /// <summary>体力最大値</summary>
        public float MaxLife { get => _MaxLife; }
        /// <summary>取得経験値量</summary>
        public int Exp { get => _Exp; }
        /// <summary>次のレベルアップまでに必要な経験値量</summary>
        public int NextLevelExp { get => _NextLevelExp; }
        /// <summary>プレイヤーの現在レベル</summary>
        public short Level { get => _Level; }
        #endregion


        // Start is called before the first frame update
        void Start()
        {
            _LayerIndexEnemy = LayerMask.NameToLayer(_LayerNameEnemy);
        }

        // Update is called once per frame
        void Update()
        {
            //レベルアップ処理
            if(_Exp >= _NextLevelExp)
            {
                _Level++;
                _NextLevelExp += _NextLevelExp / 2;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            //敵に接触するとダメージ
            if (collision.gameObject.layer == _LayerIndexEnemy)
            {
                EnemyAttack attack = collision.gameObject.GetComponent<EnemyAttack>();
                _Life -= attack.PowerOnEnter;
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (PauseManager.IsTimerStopped) return;

            //敵に接触し続ける間ダメージ
            if (collision.gameObject.layer == _LayerIndexEnemy)
            {
                EnemyAttack attack = collision.gameObject.GetComponent<EnemyAttack>();
                _Life -= attack.PowerOnStay;
            }
        }
    }
}
