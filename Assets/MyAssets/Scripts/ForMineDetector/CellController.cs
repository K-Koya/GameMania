using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MineDetector
{
    public class CellController : MonoBehaviour
    {
        /// <summary>�󔒂�\�����l</summary>
        public const byte EMPTY_CONTENT = 0;

        /// <summary>�n����\�����l</summary>
        public const byte MINE_CONTENT = 9;

        [SerializeField, Tooltip("�n���p�̃C���X�g")]
        Image _MineImage = null;

        [SerializeField, Tooltip("���g�̐��l")]
        TextMeshProUGUI _ContentText = null;

        [SerializeField, Tooltip("�}�X�ڒ��g�i0�͋�j")]
        byte _Content = 0;

        /// <summary>���̃Z���̍��W</summary>
        Vector2Int _Index = default;

        /// <summary>�����ŊW���J��������悤�ɂ��郁�\�b�h</summary>
        public System.Action OpenMyself = null;

        /// <summary>�W���O����Ă��邩���m�F���郁�\�b�h</summary>
        public System.Func<bool> IsOpenned = null;


        /// <summary>�}�X�ڒ��g�i9�͒n�� 0�Ƃ��̑��͋�j</summary>
        public byte Contant { get => _Content; }

        /// <summary>���̃Z���̍��W</summary>
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

        /// <summary>�Z���̒��g�̐��l��ݒ肵�`��</summary>
        /// <param name="content">���g�̐��l</param>
        public void SetContent(byte content)
        {
            _Content = content;
            ConvertAppearContent();
        }

        /// <summary>�Z���̈ʒu����ۊǂ��A�Z���̒��g�̐��l��ݒ肵�`��</summary>
        /// <param name="index">�Z���̈ʒu</param>
        /// <param name="content">���g�̐��l</param>
        public void SetContent(Vector2Int index, byte content)
        {
            _Index = index;
            SetContent(content);
        }

        /// <summary>�Z���̒��g�𐔒l���猈��</summary>
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
