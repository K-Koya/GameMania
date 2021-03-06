using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Survival
{
    /// <summary>プレイヤーの攻撃や体力処理</summary>
    public class PlayerStatus : StatusBase
    {
        #region メンバ
        /// <summary>プレイヤーから離れていて削除対象になる距離/summary>
        public const float FAR_POINT = 35f;

        /// <summary>プレイヤー位置</summary>
        public static Transform Transform = default;

        [SerializeField, Tooltip("プレイヤーの現在レベル")]
        short _Level = 1;

        [SerializeField, Tooltip("体力最大値")]
        float _MaxLife = 100f;

        [SerializeField, Tooltip("取得経験値量")]
        int _Exp = 1;

        [SerializeField, Tooltip("次のレベルアップまでに必要な経験値量")]
        int _NextLevelExp = 6;

        [SerializeField, Tooltip("レベルアップしたときに実行したいメソッドをアサイン")]
        UnityEvent _RequireForLevelup = default;
        #endregion

        #region プロパティ
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
            Transform = transform;
        }

        // Update is called once per frame
        void Update()
        {
            //レベルアップ処理
            if(_Exp >= _NextLevelExp)
            {
                _Level++;
                _Exp -= _NextLevelExp;
                _NextLevelExp += _NextLevelExp / 3;
                _RequireForLevelup.Invoke();
            }
        }

        void OnDestroy()
        {
            Transform = null;
        }

        private void OnCollisionEnter(Collision collision)
        {
            //敵に接触するとダメージ
            if (collision.gameObject.layer == LayerManager.Enemy)
            {
                AttackInfo attack = collision.gameObject.GetComponent<AttackInfo>();
                _Life -= attack.PowerOnEnter;
                if(_Life < 0)
                {
                    gameObject.SetActive(false);
                    ParticleManager.Emit((int)ParticleManager.Kind.PlayerDefeated, transform, transform.localScale * 3f);
                    SEManager.Emit((int)SEManager.Kind.PlayerDefeated, transform);
                }
                else
                {
                    ParticleManager.Emit((int)ParticleManager.Kind.PlayerDamaged, transform, transform.localScale * 2f);
                }
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (PauseManager.IsTimerStopped) return;

            //敵に接触し続ける間ダメージ
            if (collision.gameObject.layer == LayerManager.Enemy)
            {
                AttackInfo attack = collision.gameObject.GetComponent<AttackInfo>();
                _Life -= attack.PowerOnStay;
                if (_Life < 0)
                {
                    gameObject.SetActive(false);
                    ParticleManager.Emit((int)ParticleManager.Kind.PlayerDefeated, transform, transform.localScale * 3f);
                    SEManager.Emit((int)SEManager.Kind.PlayerDefeated, transform);
                }
                else
                {
                    ParticleManager.Emit((int)ParticleManager.Kind.PlayerDamaged, transform, transform.localScale * 0.75f);
                    SEManager.Emit((int)SEManager.Kind.PlayerDamaged, transform);
                }
            }
        }

        /// <summary>経験値を加算</summary>
        /// <param name="exp">倒した敵が持つ経験値</param>
        public void AddExp(short exp)
        {
            _Exp += exp;
        }

        /// <summary>体力を回復</summary>
        /// <param name="Ratio">回復量 最大値との割合</param>
        public void HealLife(float Ratio)
        {
            _Life += _MaxLife * Ratio;
            if (_Life > _MaxLife) _Life = _MaxLife;
            SEManager.Emit((int)SEManager.Kind.PlayerHealing, transform);
        }

    }
}
