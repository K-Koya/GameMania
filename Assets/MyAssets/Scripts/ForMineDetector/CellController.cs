using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MineDetector
{
    public class CellController : MonoBehaviour
    {
        /// <summary>空白を表す数値</summary>
        public const byte EMPTY_CONTENT = 0;

        /// <summary>地雷を表す数値</summary>
        public const byte MINE_CONTENT = 9;

        [SerializeField, Tooltip("地雷用のイラスト")]
        Image _MineImage = null;

        [SerializeField, Tooltip("中身の数値")]
        TextMeshProUGUI _ContentText = null;

        [SerializeField, Tooltip("マス目中身（0は空）")]
        byte _Content = 0;

        /// <summary>このセルの座標</summary>
        Vector2Int _Index = default;

        /// <summary>自分で蓋を開けさせるようにするメソッド</summary>
        public System.Action OpenMyself = null;

        /// <summary>蓋が外されているかを確認するメソッド</summary>
        public System.Func<bool> IsOpenned = null;


        /// <summary>マス目中身（9は地雷 0とその他は空）</summary>
        public byte Contant { get => _Content; }

        /// <summary>このセルの座標</summary>
        public Vector2Int Index { get => _Index; }



        // Start is called before the first frame update
        void Start()
        {
            _MineImage.gameObject.SetActive(false);
            _ContentText.gameObject.SetActive(false);
        }

        void OnValidate()
        {
            ConvertAppearContent();
        }

        /// <summary>セルの中身の数値を設定し描画</summary>
        /// <param name="content">中身の数値</param>
        public void SetContent(byte content)
        {
            _Content = content;
            ConvertAppearContent();
        }

        /// <summary>セルの位置情報を保管し、セルの中身の数値を設定し描画</summary>
        /// <param name="index">セルの位置</param>
        /// <param name="content">中身の数値</param>
        public void SetContent(Vector2Int index, byte content)
        {
            _Index = index;
            SetContent(content);
        }

        /// <summary>セルの中身を数値から決定</summary>
        void ConvertAppearContent()
        {
            if (EMPTY_CONTENT < _Content && _Content < MINE_CONTENT)
            {
                _MineImage.gameObject.SetActive(false);
                _ContentText.gameObject.SetActive(true);
                _ContentText.text = _Content.ToString();
            }
            else if (_Content == MINE_CONTENT)
            {
                _ContentText.gameObject.SetActive(false);
                _MineImage.gameObject.SetActive(true);
            }
            else
            {
                _MineImage.gameObject.SetActive(false);
                _ContentText.gameObject.SetActive(false);
            }
        }
    }
}
