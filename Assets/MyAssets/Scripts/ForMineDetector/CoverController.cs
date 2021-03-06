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
            _Cell.IsOpenned = () => IsOpenned;
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
            //ゲーム開始メソッドを未実行なら実行し、ゲーム開始
            if (MineDetectorCellMap.GameStart != null)
            {
                MineDetectorCellMap.GameStart(Index.x, Index.y);
                GameManager.GameStartCall();
            }

            //既にこのマスが開いていれば離脱
            if (_IsOpenned) return;

            //旗が立っていれば外さない
            if (_IsBuiltFlag) return;

            _IsOpenned = true;
            gameObject.SetActive(!_IsOpenned);

            //マスの中身によって処理を変える
            switch (_Cell.Contant)
            {
                //このマスが空白なら周囲のマスを開ける
                case CellController.EMPTY_CONTENT:
                    MineDetectorCellMap.CheckAround(Index.y, Index.x);
                    break;

                //このマスが爆弾なら敗北し爆弾マスをすべて開いた上でゲーム終了
                case CellController.MINE_CONTENT:
                    GameManager.GameFaultCall();
                    MineDetectorCellMap.CheckTheAnswer();
                    break;
            }

            //ゲームをクリアしていたら、それを報告する
            if (MineDetectorCellMap.CheckCleared())
            {
                GameManager.GameClearCall();
            }
        }

        /// <summary>旗を建てているときは降ろし、旗を降ろしているときは建てる</summary>
        /// <returns>変更後の旗の状況</returns>
        public bool SwitchFlag()
        {
            _IsBuiltFlag = !_IsBuiltFlag;
            return _IsBuiltFlag;
        }
    }
}
