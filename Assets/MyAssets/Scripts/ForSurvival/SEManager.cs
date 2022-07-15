using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    /// <summary>効果音を管理するコンポーネント</summary>
    public class SEManager : MonoBehaviour
    {
        [SerializeField, Tooltip("使用する効果音の発生源数を指定"), Range(1, 100)]
        int _NumberOfAudioSource = 20;

        [SerializeField, Tooltip("量産する効果音発生源プレハブ")]
        GameObject _AudioSourcePref = null;

        [SerializeField, Tooltip("使用する効果音をここへ格納")]
        AudioClip[] _UseSes = default;

        /// <summary>Instantiateさせた効果音オブジェクトを格納</summary>
        ObjectPool<AudioSource> _SEObjects = null;

        /// <summary>staticメソッドから効果音を発生させるために使うデリゲート</summary>
        static public System.Action<int, Transform> Emit = null;


        /// <summary>使う効果音の種類</summary>
        public enum Kind
        {
            None = -1,

            SelectMove = 0,
            SelectDecide = 1,

            EnemyDamaged = 2,
            PlayerDamaged = 3,

            PlayerDefeated = 4,

            PlayerWeapon1 = 5,
            PlayerWeapon2 = 6,

            PlayerHealing = 7,
        }


        // Start is called before the first frame update
        void Start()
        {
            _SEObjects = new ObjectPool<AudioSource>((uint)_NumberOfAudioSource);

            for(int i = 0; i < _NumberOfAudioSource; i++)
            {
                GameObject ins = Instantiate(_AudioSourcePref);
                _SEObjects.Create(ins.GetComponent<AudioSource>());
            }

            //メソッドアクセスを確立
            Emit = EmitAudioSourceObject;
        }

        /// <summary>任意の位置に任意の方向に指定した番号の効果音を発生させる</summary>
        /// <param name="index">指定番号</param>
        /// <param name="source">任意の位置・方向</param>
        void EmitAudioSourceObject(int index, Transform source)
        {
            //番号が範囲外であれば離脱
            if (index >= _UseSes.Length) return;

            foreach (AudioSource aud in _SEObjects.Values)
            {
                if (!aud.isPlaying)
                {
                    aud.gameObject.SetActive(true);
                    aud.transform.position = source.position;
                    aud.PlayOneShot(_UseSes[index]);

                    break;
                }
            }
        }
    }
}