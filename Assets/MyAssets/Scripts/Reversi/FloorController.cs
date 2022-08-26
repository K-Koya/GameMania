using UnityEngine;

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

        [SerializeField, Tooltip("セルの場所")]
        Vector2Int _FloorIndex = Vector2Int.zero;

        [SerializeField, Tooltip("マウスカーソルを合わせたときに使う床のマテリアル")]
        Material _OriginalOveredMouse = default;

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
                _Renderer.material = _Original;
            }
        }

        /// <summary>カーソルを合わせている</summary>
        public void OveredMouseCursor()
        {
            if (_StoneColor == StoneColor.None)
            {
                _IsOveredMouse = true;
            }
        }

        /// <summary>石を投下する</summary>
        /// <param name="stoneColor">色</param>
        public void DropStone(StoneColor stoneColor)
        {
            //既に石が乗っていたなら離脱
            if (_StoneColor != StoneColor.None) return;

            //無色を渡して来たら離脱
            if (stoneColor == StoneColor.None) return;

            //石を見えるようにして投下する
            _StoneController.transform.position = transform.position + transform.up * 9f;
            _StoneController.transform.up = Vector3.up * (int)stoneColor;
            _StoneController.gameObject.SetActive(true);

            //色を更新
            _StoneColor = stoneColor;
        }

        /// <summary>石を落下させることなく即盤面上に置く</summary>
        /// <param name="stoneColor">色</param>
        public void SetStone(StoneColor stoneColor)
        {

        }
    }
}