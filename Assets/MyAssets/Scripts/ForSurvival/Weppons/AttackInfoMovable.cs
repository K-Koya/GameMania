using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    public class AttackInfoMovable : AttackInfo
    {
        /// <summary>��s���x</summary>
        float _Speed = 0f;

        /// <summary>��s�������</summary>
        Vector3 _Direction = Vector3.zero;

        /// <summary>�ђʉ�</summary>
        byte _PenetrateCount = 1;

        /// <summary>���݂ł��鎞��</summary>
        float _AliveTime = 100f;


        // Update is called once per frame
        void Update()
        {
            if (PauseManager.IsTimerStopped) return;

            transform.position += _Direction * _Speed * Time.deltaTime;

            //���݂��鎞�Ԃ̐���
            _AliveTime -= Time.deltaTime;
            if(_AliveTime < 0f)
            {
                gameObject.SetActive(false);
            }

            //�v���C���[���牓���ɗ��ꂽ��폜
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
                //�ђʉ\�񐔂𐧌�
                _PenetrateCount -= 1;
                if(_PenetrateCount < 1)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
