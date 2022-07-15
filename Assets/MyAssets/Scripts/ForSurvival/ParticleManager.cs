using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    public class ParticleManager : MonoBehaviour
    {
        [SerializeField, Tooltip("使用するパーティクルをここへ格納")]
        ParticleContainer[] _UseParticles = default;

        /// <summary>InstantiateさせたParticleを格納</summary>
        ObjectPool<ParticleSystem>[] _Particles = null;

        /// <summary>staticメソッドからParticleを発生させるために使うデリゲート</summary>
        static public System.Action<int, Transform, Vector3> Emit = null;

        /// <summary>使うパーティクルの種類</summary>
        public enum Kind
        {
            PlayerDamaged = 0,
            PlayerDefeated = 1,
            EnemyDamaged = 2,
            EnemyDefeated = 3,
        }

        [System.Serializable]
        struct ParticleContainer
        {
            [SerializeField, Tooltip("使用するパーティクル")] 
            public GameObject ParticlePrefab;

            [SerializeField, Tooltip("一度に生成する数"), Range(1, 1000)]
            public int Count;
        }

        void Start()
        {
            //使用するパーティクルの数だけプールを作る
            _Particles = new ObjectPool<ParticleSystem>[_UseParticles.Length];

            //実体化
            for(int i = 0; i < _UseParticles.Length; i++)
            {
                if (!_UseParticles[i].ParticlePrefab) continue;

                _Particles[i] = new ObjectPool<ParticleSystem>((uint)_UseParticles[i].Count);
                for (int k = 0; k < _UseParticles[i].Count; k++)
                {
                    GameObject ins = Instantiate(_UseParticles[i].ParticlePrefab);
                    ins.SetActive(false);
                    _Particles[i].Create(ins.GetComponent<ParticleSystem>());
                }
            }

            //メソッドアクセスを確立
            Emit = EmitParticle;
        }

        /// <summary>任意の位置に任意の方向に指定した番号のパーティクルを発生させる</summary>
        /// <param name="index">指定番号</param>
        /// <param name="source">任意の位置・方向</param>
        /// <param name="scale">大きさ倍率</param>
        void EmitParticle(int index, Transform source, Vector3 scale)
        {
            //番号が範囲外であれば離脱
            if (index >= _Particles.Length) return;

            foreach(ParticleSystem par in _Particles[index].Values)
            {
                if (!par.gameObject.activeSelf)
                {
                    par.gameObject.SetActive(true);
                    par.transform.position = source.position;
                    par.transform.rotation = source.rotation;
                    par.transform.localScale = scale;
                    par.Play();

                    break;
                }
            }
        }
    }
}