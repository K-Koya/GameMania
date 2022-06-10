using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    public class WaveEnemyManager : MonoBehaviour
    {
        /// <summary>タグ名 : スポナー</summary>
        const string _TAG_NAME_SPAWNER = "Spawner";

        [SerializeField, Tooltip("経過時間を記憶(s)")]
        double _Timer = 0f;

        /// <summary>次のウェーブに行くための必要時間</summary>
        double _WaveBorder = 0f;

        [SerializeField, Tooltip("1Waveの期間(s)")]
        float _WaveCoolTime = 60f;

        [SerializeField, Tooltip("現在のウェーブカウント")]
        short _WaveCount = 0;

        [SerializeField, Tooltip("実際に出現予定の敵のプレハブ")]
        GameObject[] _EnemyPrefs = default;

        [SerializeField, Tooltip("Waveごとに出現予定の敵の種類と数を格納")]
        SummonEnemyOnWave[] _EnemyDataForWaves = default;

        /// <summary>出現している敵をまとめて管理するインスタンス</summary>
        ObjectPool<GameObject>[] Enemies = default;

        /// <summary>出現するウェーブを終えた敵をまとめて管理するインスタンス</summary>
        ObjectPool<GameObject>[] EndWaveEnemies = default;

        /// <summary>敵を出現させる場所</summary>
        Transform[] EnemySpawners = default;

        /// <summary>経過時間(s)</summary>
        public double Timer { get => _Timer; }


        // Start is called before the first frame update
        void Start()
        {
            EnemySpawners = GameObject.FindGameObjectsWithTag(_TAG_NAME_SPAWNER).Select(g => g.transform).ToArray();

            _WaveBorder = 0f;
            _WaveCount = -1;
        }

        // Update is called once per frame
        void Update()
        {
            if (PauseManager.IsTimerStopped) return;

            //タイマー加算&ウェーブカウント処理
            _Timer += Time.deltaTime;
            if(_Timer > _WaveBorder)
            {
                _WaveBorder += _WaveCoolTime;

                //次のウェーブ設定があれば出現する敵情報を変更
                if (_WaveCount < _EnemyDataForWaves.Length - 1)
                {
                    EndWaveEnemies = Enemies;
                    Enemies = new ObjectPool<GameObject>[_EnemyPrefs.Length];

                    _WaveCount += 1;
                    for (int i = 0; i < _EnemyDataForWaves.Length; i++)
                    {
                        uint id = _EnemyDataForWaves[_WaveCount].SummonEnemies[i].EnemyId;
                        Enemies[id] = new ObjectPool<GameObject>(_EnemyDataForWaves[_WaveCount].SummonEnemies[i].Count);

                        for (int k = 0; k < _EnemyDataForWaves[_WaveCount].SummonEnemies[i].Count; k++)
                        {
                            GameObject obj = Enemies[id].Create(Instantiate(_EnemyPrefs[id]));
                            obj.SetActive(false);
                        }
                    }
                }
            }

            //削除処理対象の敵がいなければ抜ける
            if (EndWaveEnemies == null) return;

            //ウェーブ対象外の敵を順次削除
            for(int i = 0; i < EndWaveEnemies.Length; i++)
            {
                if (EndWaveEnemies[i] == null) continue;

                bool haveActive = false;
                for(int k = 0; k < EndWaveEnemies[i].Values.Length; k++)
                {
                    if (EndWaveEnemies[i].Values[k])
                    {
                        haveActive = true;
                        if (!EndWaveEnemies[i].Values[k].activeSelf)
                        {
                            Destroy(EndWaveEnemies[i].Values[k]);
                        }
                    }
                }

                if (!haveActive) EndWaveEnemies[i] = null;
            }
        }

        void FixedUpdate()
        {
            //敵を生成
            uint[] ids = _EnemyDataForWaves[_WaveCount].SummonEnemies.Select(ed => ed.EnemyId).ToArray();
            for (uint i = 0; i < Enemies.Length; i++)
            {
                //ウェーブに含まれている敵
                if (ids.Contains(i))
                {
                    foreach (GameObject enemy in Enemies[i].Values)
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
    }

    /// <summary> 出現する敵の種類と量 </summary>
    [System.Serializable]
    public struct SummonEnemy
    {
        [SerializeField, Tooltip("対象敵のプレハブ配列における要素番号")]
        public uint EnemyId;

        [SerializeField, Tooltip("対象敵を出現させる数")]
        public uint Count;
    }

    /// <summary> 1ウェーブ中に出現する敵の種類と量 </summary>
    [System.Serializable]
    public struct SummonEnemyOnWave
    {
        public string name;

        public SummonEnemy[] SummonEnemies;
    }
}
