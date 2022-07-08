using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MineDetector
{
    public class CoverController : MonoBehaviour
    {
        /// <summary>蓋オブジェクトのレンダーラー</summary>
        Renderer _Renderer = default;

        /// <summary>当該セルの情報</summary>
        CellController _Cell = null;

        /// <summary>元の空白マテリアル</summary>
        Material _Original = default;

        [SerializeField, Tooltip("マウスカーソルを合わせたときに使う空白マテリアル")]
        Material _OriginalOveredMouse = default;

        [SerializeField, Tooltip("元の旗マテリアル")]
        Material _BuiltFlag = default;

        [SerializeField, Tooltip("マウスカーソルを合わせたときに使う旗マテリアル")]
        Material _BuiltFlagOveredMouse = default;

        [SerializeField, Tooltip("元のはてなマテリアル")]
        Material _Question = default;

        [SerializeField, Tooltip("マウスカーソルを合わせたときに使うはてなマテリアル")]
        Material _QuestionOveredMouse = default;

        [SerializeField, Tooltip("True : マウスカーソルが合っている")]
        bool _IsOveredMouse = false;

        [SerializeField, Tooltip("True : 公開済み")]
        bool _IsOpenned = false;

        [SerializeField, Tooltip("True : 旗を建てている")]
        bool _IsBuiltFlag = false;


        /// <summary>True : 公開済み</summary>
        public bool IsOpenned { get => _IsOpenned; }

        /// <summary>セルの位置</summary>
        public Vector2Int Index { get => _Cell.Index; }



        // Start is called before the first frame update
        void Start()
        {
            _Renderer = GetComponent<Renderer>();
            _Original = _Renderer.materials[0];

            _Cell = GetComponentInParent<CellController>();
            _Cell.OpenMyself = Open;
        }

        // Update is called once per frame
        void Update()
        {
            if (_IsOveredMouse)
            {
                if (_IsBuiltFlag)
                {
                    _Renderer.material = _BuiltFlagOveredMouse;
                }
                else
                {
                    _Renderer.material = _OriginalOveredMouse;
                }
                
                _IsOveredMouse = false;
            }
            else
            {
                if (_IsBuiltFlag)
                {
                    _Renderer.material = _BuiltFlag;
                }
                else
                {
                    _Renderer.material = _Original;
                }
            }
        }

        /// <summary>カーソルを合わせている</summary>
        public void OveredMouseCursor()
        {
            _IsOveredMouse = true;
        }

        /// <summary>蓋を外す</summary>
        public void Open()
        {
            //ゲーム開始メソッドを未実行なら実行する
            MineDetectorCellMap.GameStart?.Invoke(Index.x, Index.y);

            //既にこのマスが開いていれば離脱
            if (_IsOpenned) return;

            //旗が立っていれば外さない
            if (_IsBuiltFlag) return;

            _IsOpenned = true;
            gameObject.SetActive(!_IsOpenned);

            //このマスが空白なら周囲のマスを開ける
            if (_Cell.Contant == CellController.EMPTY_CONTENT) MineDetectorCellMap.CheckAround(Index.y, Index.x);
        }

        /// <summary>旗を建てているときは降ろし、旗を降ろしているときは建てる</summary>
        public void SwitchFlag()
        {
            _IsBuiltFlag = !_IsBuiltFlag;
        }
    }
}
