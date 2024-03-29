using UnityEngine;
using TMPro;

namespace Reversi
{
    [RequireComponent(typeof(Renderer))]
    public class FloorController : MonoBehaviour
    {
        #region メンバ
        [SerializeField, Tooltip("石の実体プレハブ")]
        GameObject _StonePrefab = null;

        /// <summary>床のレンダーラー</summary>
        Renderer _Renderer = default;

        /// <summary>元の床のマテリアル</summary>
        Material _Original = default;

        /// <summary>true : 石を落とせる</summary>
        bool _IsAbleToDrop = false;

        [SerializeField, Tooltip("セルの場所")]
        Vector2Int _FloorIndex = Vector2Int.zero;

        [SerializeField, Tooltip("石をおける床のマテリアル")]
        Material _AbleToDropStone = default;

        [SerializeField, Tooltip("マウスカーソルを合わせたときに使う床のマテリアル")]
        Material _OriginalOveredMouse = default;

        [SerializeField, Tooltip("個数計測用GUI")]
        TextMeshProUGUI _ResultCountText = null;

        [SerializeField, Tooltip("True : マウスカーソルが合っている")]
        bool _IsOveredMouse = false;

        /// <summary>乗っている石の色</summary>
        StoneColor _StoneColor = StoneColor.None;

        /// <summary>石のデータ</summary>
        StoneController _StoneController = null;
        #endregion

        #region プロパティ
        /// <summary>この床がどの位置にあるかの情報</summary>
        public Vector2Int FloorIndex { get => _FloorIndex; set => _FloorIndex = value; }
        /// <summary>乗っている石の色</summary>
        public StoneColor StoneColor { get => _StoneColor; }
        /// <summary>石のデータ</summary>
        public StoneController StoneController { get => _StoneController; }
        /// <summary>true : 石を落とせる</summary>
        public bool IsAbleToDrop { get => _IsAbleToDrop; set => _IsAbleToDrop = value; }
        #endregion


        // Start is called before the first frame update
        void Start()
        {
            //レンダーラーと元のマテリアルを取得
            _Renderer = GetComponent<Renderer>();
            _Original = _Renderer.material;

            //石を生成してデータをリンク
            GameObject obj = Instantiate(_StonePrefab);
            obj.SetActive(false);
            obj.transform.parent = transform;
            _StoneController = obj.GetComponent<StoneController>();
            _StoneController.transform.localPosition = transform.up;

            _StoneColor = StoneColor.None;

            _ResultCountText?.gameObject.SetActive(false);

            GetComponentInChildren<Canvas>().worldCamera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            if (_IsOveredMouse)
            {
                _Renderer.material = _OriginalOveredMouse;
                _IsOveredMouse = false;
            }
            else
            {
                if (_IsAbleToDrop)
                {
                    _Renderer.material = _AbleToDropStone;
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
            if (_StoneColor == StoneColor.None && _IsAbleToDrop)
            {
                _IsOveredMouse = true;
            }
        }

        /// <summary>石を投下する</summary>
        /// <param name="stoneColor">色</param>
        /// <returns>true : パスが発生</returns>
        public bool DropStone(StoneColor stoneColor, bool useAnim)
        {
            //既に石が乗っていたなら離脱
            if (_StoneColor != StoneColor.None) return false;

            //無色を渡して来たら離脱
            if (stoneColor == StoneColor.None) return false;

            //石を見えるようにして投下する
            if(useAnim) _StoneController.transform.position = transform.position + transform.up * 3f;
            else _StoneController.transform.position = transform.position + transform.up * 0.5f;
            _StoneController.transform.up = Vector3.up * (int)stoneColor;
            _StoneController.gameObject.SetActive(true);

            //色を更新
            _StoneColor = stoneColor;
            //他の石を裏返す
            ReversiCellMap.Instance.TurnOverStones(stoneColor, _FloorIndex.x, _FloorIndex.y);
            //盤面を更新
            if (ReversiCellMap.Instance.DetectTurnOverCell(stoneColor))
            {
                return false;
            }

            return true;
        }

        /// <summary>石を指定した色にする</summary>
        /// <param name="stoneColor">色</param>
        public void TurnOverStone(StoneColor stoneColor)
        {
            if (_StoneColor == stoneColor) return;

            _StoneColor = stoneColor;
            _StoneController.TurnOver();
        }

        /// <summary>マスに何も置かれていない状態に戻す</summary>
        public void ResetCell()
        {
            _StoneColor = StoneColor.None;
            _StoneController.gameObject.SetActive(false);
        }

        /// <summary>石の個数を表示するGUIの表示・非表示設定</summary>
        /// <param name="number">石のカウント(0以下なら非表示)</param>
        public void ActiveCountText(int number, Color color)
        {
            bool isActive = number > 0;

            if (isActive && _ResultCountText)
            {
                _ResultCountText.color = color;
                _ResultCountText.text = number.ToString();
            }
            _ResultCountText?.gameObject.SetActive(isActive);
        }
    }
}