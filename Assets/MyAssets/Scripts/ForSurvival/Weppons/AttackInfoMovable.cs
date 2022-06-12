using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    public class AttackInfoMovable : AttackInfo
    {
        /// <summary>飛行速度</summary>
        float _Speed = 0f;

        /// <summary>飛行する方向</summary>
        Vector3 _Direction = Vector3.zero;

        /// <summary>貫通回数</summary>
        byte _PenetrateCount = 1;

        /// <summary>存在できる時間</summary>
        float _AliveTime = 100f;


        // Update is called once per frame
        void Update()
        {
            if (PauseManager.IsTimerStopped) return;

            transform.position += _Direction * _Speed * Time.deltaTime;

            //存在する時間の制御
            _AliveTime -= Time.deltaTime;
            if(_AliveTime < 0f)
            {
                gameObject.SetActive(false);
            }

            //プレイヤーから遠くに離れたら削除
            if(Vector3.SqrMagnitude(PlayerStatus.Transform.position - transform.position) > Mathf.Pow(PlayerStatus.FAR_POINT, 2))
            {
                gameObject.SetActive(false);
            }
        }

        public void DataSet(StatusBase status, float powerOnStay, float powerOnEnter, float speed, Vector3 direction, byte penetrateCount, float aliveTime)
        {
            DataSet(status, powerOnStay, powerOnEnter);
            _Speed = speed;
            _Direction = direction;
            _PenetrateCount = penetrateCount;
            _AliveTime = aliveTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer == LayerManager.Enemy)
            {
                //貫通可能回数を制御
                _PenetrateCount -= 1;
                if(_PenetrateCount < 1)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
