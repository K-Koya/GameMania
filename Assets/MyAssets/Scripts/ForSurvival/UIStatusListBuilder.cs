using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Survival
{
    /// <summary>ポーズメニューやレベルアップ時に表示するリストを構築する</summary>
    public class UIStatusListBuilder : MonoBehaviour
    {
        [SerializeField, Tooltip("UIテキストの内、プレイヤーのステータス機能の情報")]
        TextMeshProUGUI[] _PlayerStatusInfoTexts = default;

        [SerializeField, Tooltip("プレイヤーのステータススキルのレベルアップボタンUI")]
        Button[] _PlayerStatusLevelUpButtons = default;

        [SerializeField, Tooltip("UIテキストの内、武器1の表示するスキル名")]
        TextMeshProUGUI[] _Weapon1SubTitleTexts = default;

        [SerializeField, Tooltip("UIテキストの内、武器1のスキルのレベル値")]
        TextMeshProUGUI[] _Weapon1LevelTexts = default;

        [SerializeField, Tooltip("武器1のスキルのレベルアップボタンUI")]
        Button[] _Weapon1LevelUpButtons = default;

        [SerializeField, Tooltip("UIテキストの内、武器2の表示するスキル名")]
        TextMeshProUGUI[] _Weapon2SubTitleTexts = default;

        [SerializeField, Tooltip("UIテキストの内、武器2のスキルのレベル値")]
        TextMeshProUGUI[] _Weapon2LevelTexts = default;

        [SerializeField, Tooltip("武器2のスキルのレベルアップボタンUI")]
        Button[] _Weapon2LevelUpButtons = default;

        [SerializeField, Tooltip("武器2のスキルの解放ボタンUI")]
        Button _Weapon2UnlockButton = default;

        [SerializeField, Tooltip("ture : レベルアップ時であり、レベルアップボタンが押せるリストを作る")]
        bool _IsUseButton = false;

        [SerializeField, Tooltip("武器1情報")]
        PlayerWeaponBase _Weapon1Info = null;

        [SerializeField, Tooltip("武器2情報")]
        PlayerWeaponBase _Weapon2Info = null;

        [SerializeField, Tooltip("プレイヤーの移動用コンポーネント")]
        PlayerMove _MoveInfo = null;

        [SerializeField, Tooltip("プレイヤー情報")]
        PlayerStatus _StatusInfo = null;

        // Start is called before the first frame update
        void Start()
        {
            if(_Weapon2UnlockButton) _Weapon2UnlockButton.interactable = false;
            foreach(var button in _Weapon2LevelUpButtons) button.interactable = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnEnable()
        {
            //プレイヤーのステータススキルの表示を構築
            if (_IsUseButton)
            {
                int healed = (int)(_StatusInfo.Life + (_StatusInfo.MaxLife * 0.25f));
                if (healed > _StatusInfo.MaxLife) healed = (int)_StatusInfo.MaxLife;
                _PlayerStatusInfoTexts[0].text = $"{(int)_StatusInfo.Life} → {healed}";

                byte nowLevel = _MoveInfo.MoveSpeedLevel;
                byte nextLevel = (byte)(nowLevel + 1);
                if (nextLevel > _MoveInfo.MoveSpeedMaxLevel)
                {
                    _PlayerStatusLevelUpButtons[1].interactable = false;
                    _PlayerStatusInfoTexts[1].text = $"Lv.{nowLevel} MAX!";
                }
                else _PlayerStatusInfoTexts[1].text = $"Lv.{nowLevel} → Lv.{nextLevel}";
            }
            else
            {
                _PlayerStatusInfoTexts[0].text = $"{(int)_StatusInfo.Life} / {(int)_StatusInfo.MaxLife}";
                _PlayerStatusInfoTexts[1].text = $"Lv.{_MoveInfo.MoveSpeedLevel}";
            }


            //武器1の表示を構築
            for (int i = 0; i < _Weapon1SubTitleTexts.Length; i++)
            {
                _Weapon1SubTitleTexts[i].text = _Weapon1Info.WeaponInfomations[i].Name;
                if (_IsUseButton)
                {
                    byte nowLevel = _Weapon1Info.WeaponInfomations[i].Level;
                    byte nextLevel = (byte)(nowLevel + 1);
                    if (nextLevel > _Weapon1Info.WeaponInfomations[i].MaxLevel)
                    {
                        _Weapon1LevelUpButtons[i].interactable = false;
                        _Weapon1LevelTexts[i].text = $"Lv.{nowLevel} MAX!";
                    }
                    else _Weapon1LevelTexts[i].text = $"Lv.{nowLevel} → Lv.{nextLevel}";
                }
                else _Weapon1LevelTexts[i].text = $"Lv.{_Weapon1Info.WeaponInfomations[i].Level}";
            }

            //武器2のLock表示
            if(_IsUseButton && _StatusInfo.Level >= 5) _Weapon2UnlockButton.interactable = true;

            //武器2の表示を構築
            for (int i = 0; i < _Weapon2SubTitleTexts.Length; i++)
            {
                _Weapon2SubTitleTexts[i].text = _Weapon2Info.WeaponInfomations[i].Name;
                if (_IsUseButton)
                {
                    byte nowLevel = _Weapon2Info.WeaponInfomations[i].Level;
                    byte nextLevel = (byte)(nowLevel + 1);
                    if (nextLevel > _Weapon2Info.WeaponInfomations[i].MaxLevel)
                    {
                        _Weapon2LevelUpButtons[i].interactable = false;
                        _Weapon2LevelTexts[i].text = $"Lv.{nowLevel} MAX!";
                    }
                    else _Weapon2LevelTexts[i].text = $"Lv.{nowLevel} → Lv.{nextLevel}";
                }
                else _Weapon2LevelTexts[i].text = $"Lv.{_Weapon2Info.WeaponInfomations[i].Level}";
            }
        }

        /// <summary>対象を指定してレベルアップ</summary>
        /// <param name="index">レベルアップ対象（配列の要素番号）</param>
        public void DoLevelUp(int weaponNum, int index)
        {
            switch (weaponNum)
            {
                case 1:
                    _Weapon1Info.WeaponInfomations[index].DoLevelUp();
                    break;
                case 2:
                    _Weapon2Info.WeaponInfomations[index].DoLevelUp();
                    break;
                default: break;
            }
        }
    }
}
