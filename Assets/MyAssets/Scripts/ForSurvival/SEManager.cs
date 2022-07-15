using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    /// <summary>���ʉ����Ǘ�����R���|�[�l���g</summary>
    public class SEManager : MonoBehaviour
    {
        [SerializeField, Tooltip("�g�p������ʉ��̔����������w��"), Range(1, 100)]
        int _NumberOfAudioSource = 20;

        [SerializeField, Tooltip("�ʎY������ʉ��������v���n�u")]
        GameObject _AudioSourcePref = null;

        [SerializeField, Tooltip("�g�p������ʉ��������֊i�[")]
        AudioClip[] _UseSes = default;

        /// <summary>Instantiate���������ʉ��I�u�W�F�N�g���i�[</summary>
        ObjectPool<AudioSource> _SEObjects = null;

        /// <summary>static���\�b�h������ʉ��𔭐������邽�߂Ɏg���f���Q�[�g</summary>
        static public System.Action<int, Transform> Emit = null;


        /// <summary>�g�����ʉ��̎��</summary>
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

            //���\�b�h�A�N�Z�X���m��
            Emit = EmitAudioSourceObject;
        }

        /// <summary>�C�ӂ̈ʒu�ɔC�ӂ̕����Ɏw�肵���ԍ��̌��ʉ��𔭐�������</summary>
        /// <param name="index">�w��ԍ�</param>
        /// <param name="source">�C�ӂ̈ʒu�E����</param>
        void EmitAudioSourceObject(int index, Transform source)
        {
            //�ԍ����͈͊O�ł���Η��E
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