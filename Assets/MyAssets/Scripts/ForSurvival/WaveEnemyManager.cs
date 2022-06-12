using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    public class WaveEnemyManager : MonoBehaviour
    {
        #region �����o
        /// <summary>�^�O�� : �X�|�i�[</summary>
        const string _TAG_NAME_SPAWNER = "Spawner";


        [SerializeField, Tooltip("�o�ߎ��Ԃ��L��(s)")]
        double _Timer = 0f;

        /// <summary>���̃E�F�[�u�ɍs�����߂̕K�v����</summary>
        double _WaveBorder = 0f;

        [SerializeField, Tooltip("1Wave�̊���(s)")]
        float _WaveCoolTime = 60f;

        [SerializeField, Tooltip("���݂̃E�F�[�u�J�E���g")]
        short _WaveCount = 0;

        [SerializeField, Tooltip("���ۂɏo���\��̓G�̃v���n�u")]
        GameObject[] _EnemyPrefs = default;

        [SerializeField, Tooltip("Wave���Ƃɏo���\��̓G�̎�ނƐ����i�[")]
        SummonEnemyOnWave[] _EnemyDataForWaves = default;

        /// <summary>�o�����Ă���G���܂Ƃ߂ĊǗ�����C���X�^���X</summary>
        ObjectPool<GameObject>[] _Enemies = default;

        /// <summary>�o������E�F�[�u���I�����G���܂Ƃ߂ĊǗ�����C���X�^���X</summary>
        ObjectPool<GameObject>[] _EndWaveEnemies = default;

        /// <summary>�G���o��������ꏊ</summary>
        Transform[] EnemySpawners = default;

        /// <summary>�|�����G�̐�</summary>
        static int _DefeatedEnemyCount = 0;
        #endregion


        #region �v���p�e�B
        /// <summary>�o�ߎ���(s)</summary>
        public double Timer { get => _Timer; }
        /// <summary>���̃E�F�[�u�ŏo������G</summary>
        public ObjectPool<GameObject>[] Enemies { get => _Enemies; }
        /// <summary>�o������E�F�[�u���I�����G</summary>
        public ObjectPool<GameObject>[] EndWaveEnemies { get => _EndWaveEnemies; }
        /// <summary>�|�����G�̐�</summary>
        public static int DefeatedEnemyCount { get => _DefeatedEnemyCount; set => _DefeatedEnemyCount = value; }
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            EnemySpawners = GameObject.FindGameObjectsWithTag(_TAG_NAME_SPAWNER).Select(g => g.transform).ToArray();

            _WaveBorder = 0f;
            _WaveCount = -1;

            _DefeatedEnemyCount = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (PauseManager.IsTimerStopped) return;

            //�^�C�}�[���Z&�E�F�[�u�J�E���g����
            _Timer += Time.deltaTime;
            if(_Timer > _WaveBorder)
            {
                _WaveBorder += _WaveCoolTime;

                //���̃E�F�[�u�ݒ肪����Ώo������G����ύX
                if (_WaveCount < _EnemyDataForWaves.Length - 1)
                {
                    _EndWaveEnemies = _Enemies;
                    _Enemies = new ObjectPool<GameObject>[_EnemyPrefs.Length];

                    _WaveCount += 1;
                    for (int i = 0; i < _EnemyDataForWaves.Length; i++)
                    {
                        uint id = _EnemyDataForWaves[_WaveCount].SummonEnemies[i].EnemyId;
                        _Enemies[id] = new ObjectPool<GameObject>(_EnemyDataForWaves[_WaveCount].SummonEnemies[i].Count);

                        for (int k = 0; k < _EnemyDataForWaves[_WaveCount].SummonEnemies[i].Count; k++)
                        {
                            GameObject obj = _Enemies[id].Create(Instantiate(_EnemyPrefs[id]));
                            obj.SetActive(false);
                        }
                    }
                }
            }

            //�폜�����Ώۂ̓G�����Ȃ���Δ�����
            if (_EndWaveEnemies == null) return;

            //�E�F�[�u�ΏۊO�̓G�������폜
            for(int i = 0; i < _EndWaveEnemies.Length; i++)
            {
                if (_EndWaveEnemies[i] == null) continue;

                bool haveActive = false;
                for(int k = 0; k < _EndWaveEnemies[i].Values.Length; k++)
                {
                    if (_EndWaveEnemies[i].Values[k])
                    {
                        haveActive = true;
                        if (!_EndWaveEnemies[i].Values[k].activeSelf)
                        {
                            Destroy(_EndWaveEnemies[i].Values[k]);
                        }
                    }
                }

                if (!haveActive) _EndWaveEnemies[i] = null;
            }
        }

        void FixedUpdate()
        {
            //�G�𐶐�
            uint[] ids = _EnemyDataForWaves[_WaveCount].SummonEnemies.Select(ed => ed.EnemyId).ToArray();
            for (uint i = 0; i < _Enemies.Length; i++)
            {
                //�E�F�[�u�Ɋ܂܂�Ă���G
                if (ids.Contains(i))
                {
                    foreach (GameObject enemy in _Enemies[i].Values)
                    {
                        if (!enemy.activeSelf)
                        {
                            enemy.transform.position = EnemySpawners[Random.Range(0, EnemySpawners.Length - 1)].position;
                            enemy.SetActive(true);
                            break;
                        }
                    }
                }
            }
        }

        void OnDestroy()
        {
            _Enemies = null;
            _EndWaveEnemies = null;
        }
    }

    /// <summary> �o������G�̎�ނƗ� </summary>
    [System.Serializable]
    public struct SummonEnemy
    {
        [SerializeField, Tooltip("�ΏۓG�̃v���n�u�z��ɂ�����v�f�ԍ�")]
        public uint EnemyId;

        [SerializeField, Tooltip("�ΏۓG���o�������鐔")]
        public uint Count;
    }

    /// <summary> 1�E�F�[�u���ɏo������G�̎�ނƗ� </summary>
    [System.Serializable]
    public struct SummonEnemyOnWave
    {
        public string name;

        public SummonEnemy[] SummonEnemies;
    }
}
