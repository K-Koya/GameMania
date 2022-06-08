using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    /// <summary>プレイヤーの攻撃や体力処理</summary>
    public class PlayerStatus : MonoBehaviour
    {
        [SerializeField, Tooltip("体力")]
        float _Life = 100f;

        [SerializeField, Tooltip("レイヤー名 : 敵")]
        string _LayerNameEnemy = "Enemy";

        /// <summary>レイヤー番号 : 敵</summary>
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
            //敵に接触し続ける間ダメージ
            if(collision.gameObject.layer == _LayerIndexEnemy)
            {
                EnemyAttack attack = collision.gameObject.GetComponent<EnemyAttack>();
                _Life -= attack.PowerOnStay;
            }
        }
    }
}
