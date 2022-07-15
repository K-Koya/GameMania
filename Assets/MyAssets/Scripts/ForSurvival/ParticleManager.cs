using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    public class ParticleManager : MonoBehaviour
    {
        [SerializeField, Tooltip("�g�p����p�[�e�B�N���������֊i�[")]
        ParticleContainer[] _UseParticles = default;

        /// <summary>Instantiate������Particle���i�[</summary>
        ObjectPool<ParticleSystem>[] _Particles = null;

        /// <summary>static���\�b�h����Particle�𔭐������邽�߂Ɏg���f���Q�[�g</summary>
        static public System.Action<int, Transform, Vector3> Emit = null;

        /// <summary>�g���p�[�e�B�N���̎��</summary>
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
            [SerializeField, Tooltip("�g�p����p�[�e�B�N��")] 
            public GameObject ParticlePrefab;

            [SerializeField, Tooltip("��x�ɐ������鐔"), Range(1, 1000)]
            public int Count;
        }

        void Start()
        {
            //�g�p����p�[�e�B�N���̐������v�[�������
            _Particles = new ObjectPool<ParticleSystem>[_UseParticles.Length];

            //���̉�
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

            //���\�b�h�A�N�Z�X���m��
            Emit = EmitParticle;
        }

        /// <summary>�C�ӂ̈ʒu�ɔC�ӂ̕����Ɏw�肵���ԍ��̃p�[�e�B�N���𔭐�������</summary>
        /// <param name="index">�w��ԍ�</param>
        /// <param name="source">�C�ӂ̈ʒu�E����</param>
        /// <param name="scale">�傫���{��</param>
        void EmitParticle(int index, Transform source, Vector3 scale)
        {
            //�ԍ����͈͊O�ł���Η��E
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