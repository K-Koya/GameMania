using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reversi
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
            OnMouseManagement();
        }


        /// <summary>マウスクリック操作</summary>
        void OnMouseManagement()
        {
            if (GameManager.Instance.Phase == GamePhase.Playing)
            {
                var hitInfo = IsClickedAnyCell();
                if (hitInfo.Item1)
                {
                    //マス情報を取得
                    FloorController floor = hitInfo.Item2.collider.GetComponent<FloorController>();
                    floor.OveredMouseCursor();

                    //右クリック
                    if (Input.GetButtonDown(_InputNameMouseRight))
                    {

                    }
                    //左クリック
                    else if (Input.GetButtonDown(_InputNameMouseLeft))
                    {
                        if (floor.IsAbleToDrop)
                        {
                            bool isPass = floor.DropStone(GameManager.Instance.Turn, true);
                            GameManager.Instance.SwitchTurn();
                            if (isPass)
                            {
                                if (ReversiCellMap.Instance.DetectTurnOverCell(GameManager.Instance.Turn))
                                {
                                    GameManager.Instance.Phase = GamePhase.Ending;
                                }
                            }
                        }
                    }
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