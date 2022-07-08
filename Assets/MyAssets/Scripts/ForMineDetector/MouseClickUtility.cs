using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MineDetector
{
    public class MouseClickUtility : MonoBehaviour
    {
        [SerializeField, Tooltip("対象になるカメラ")]
        Camera _Camera = default;

        [SerializeField, Tooltip("クリック可能なレイヤー")]
        LayerMask _ClickableLayer = default;

        [SerializeField, Tooltip("クリック先を探索するRayの長さ")]
        float _RayLength = 100f;

        [SerializeField, Tooltip("入力名 : マウス左クリック")]
        string _InputNameMouseLeft = "Fire1";

        [SerializeField, Tooltip("入力名 : マウス右クリック")]
        string _InputNameMouseRight = "Fire3";


        // Start is called before the first frame update
        void Start()
        {
            _Camera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            MouseClickManagement();
        } 


        /// <summary>マウスクリック操作</summary>
        void MouseClickManagement()
        {
            var hitInfo = IsClickedAnyCell();
            if (hitInfo.Item1)
            {
                //マスの蓋情報を取得
                CoverController cover = hitInfo.Item2.collider.GetComponent<CoverController>();
                cover.OveredMouseCursor();

                //右クリック
                if (Input.GetButtonDown(_InputNameMouseRight))
                {
                    //旗の反転処理
                    cover.SwitchFlag();
                }
                //左クリック
                else if (Input.GetButtonDown(_InputNameMouseLeft))
                {
                    //蓋を外す処理
                    cover.Open();
                }
            }
        }

        /// <summary>
        /// マス目のクリック判定をし、結果を返す
        /// </summary>
        /// <returns>bool True : マスを見つけた  RaycastHit Rayを当てた先の情報</returns>
        (bool, RaycastHit) IsClickedAnyCell()
        {
            //クリック位置を取得してゲーム内座標に変換
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 1f;
            Ray ray = _Camera.ScreenPointToRay(mousePosition);

            //Rayをカメラ正面に向けて飛ばす
            RaycastHit hit;
            bool isHit = Physics.Raycast(ray, out hit, _RayLength, _ClickableLayer);

            return (isHit, hit);
        }
    }
}
