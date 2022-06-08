using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    /// <summary>�v���C���[�̍U����̗͏���</summary>
    public class PlayerStatus : MonoBehaviour
    {
        [SerializeField, Tooltip("�̗�")]
        float _Life = 100f;

        [SerializeField, Tooltip("���C���[�� : �G")]
        string _LayerNameEnemy = "Enemy";

        /// <summary>���C���[�ԍ� : �G</summary>
        int _LayerIndexEnemy = -1;


        // Start is called before the first frame update
        void Start()
        {
            _LayerIndexEnemy = LayerMask.NameToLayer(_LayerNameEnemy);
        }

        // Update is called once per frame
        void Update()
        {

        }


        private void OnCollisionStay(Collision collision)
        {
            //�G�ɐڐG��������ԃ_���[�W
            if(collision.gameObject.layer == _LayerIndexEnemy)
            {
                EnemyAttack attack = collision.gameObject.GetComponent<EnemyAttack>();
                _Life -= attack.PowerOnStay;
            }
        }
    }
}
