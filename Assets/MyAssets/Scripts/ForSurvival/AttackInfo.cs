using UnityEngine;

namespace Survival
{
    /// <summary>攻撃情報</summary>
    public class AttackInfo : MonoBehaviour
    {
        /// <summary>攻撃者情報</summary>
        StatusBase _Status = default;

        [SerializeField, Tooltip("ダメージ因子に触れている間与える続けるダメージ")]
        float _PowerOnStay = 0.05f;

        [SerializeField, Tooltip("ダメージ因子に触れた直後のみ与えるダメージ")]
        float _PowerOnEnter = 0f;

        [SerializeField, Tooltip("効果音を鳴らす場合、その種類を定義")]
        SEManager.Kind _SoundKind = SEManager.Kind.None;

        /// <summary>ダメージ因子に触れている間与える続けるダメージ</summary>
        public float PowerOnStay { get => _PowerOnStay; }
        /// <summary>ダメージ因子に触れた直後のみ与えるダメージ</summary>
        public float PowerOnEnter { get => _PowerOnEnter; }
        /// <summary>攻撃者情報</summary>
        public StatusBase Status { get => _Status; }


        /// <summary>武器として出力するオブジェクト</summary>
        /// <param name="status"></param>
        /// <param name="powerOnStay"></param>
        /// <param name="powerOnEnter"></param>
        public void DataSet(StatusBase status, float powerOnStay, float powerOnEnter)
        {
            _Status = status;
            _PowerOnStay = powerOnStay;
            _PowerOnEnter = powerOnEnter;
        }

        /// <summary>主にアニメーションイベントとして呼び出すための効果音発生メソッド</summary>
        public void EmitSound()
        {
            if(_SoundKind != SEManager.Kind.None) SEManager.Emit((int)_SoundKind, transform);
        }
    }
}
