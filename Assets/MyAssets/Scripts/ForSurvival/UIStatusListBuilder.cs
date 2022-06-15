using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Survival
{
    /// <summary>�|�[�Y���j���[�⃌�x���A�b�v���ɕ\�����郊�X�g���\�z����</summary>
    public class UIStatusListBuilder : MonoBehaviour
    {
        [SerializeField, Tooltip("UI�e�L�X�g�̓��A�v���C���[�̃X�e�[�^�X�@�\�̏��")]
        TextMeshProUGUI[] _PlayerStatusInfoTexts = default;

        [SerializeField, Tooltip("�v���C���[�̃X�e�[�^�X�X�L���̃��x���A�b�v�{�^��UI")]
        Button[] _PlayerStatusLevelUpButtons = default;

        [SerializeField, Tooltip("UI�e�L�X�g�̓��A����1�̕\������X�L����")]
        TextMeshProUGUI[] _Weapon1SubTitleTexts = default;

        [SerializeField, Tooltip("UI�e�L�X�g�̓��A����1�̃X�L���̃��x���l")]
        TextMeshProUGUI[] _Weapon1LevelTexts = default;

        [SerializeField, Tooltip("����1�̃X�L���̃��x���A�b�v�{�^��UI")]
        Button[] _Weapon1LevelUpButtons = default;

        [SerializeField, Tooltip("UI�e�L�X�g�̓��A����2�̕\������X�L����")]
        TextMeshProUGUI[] _Weapon2SubTitleTexts = default;

        [SerializeField, Tooltip("UI�e�L�X�g�̓��A����2�̃X�L���̃��x���l")]
        TextMeshProUGUI[] _Weapon2LevelTexts = default;

        [SerializeField, Tooltip("����2�̃X�L���̃��x���A�b�v�{�^��UI")]
        Button[] _Weapon2LevelUpButtons = default;

        [SerializeField, Tooltip("����2�̃X�L���̉���{�^��UI")]
        Button _Weapon2UnlockButton = default;

        [SerializeField, Tooltip("ture : ���x���A�b�v���ł���A���x���A�b�v�{�^���������郊�X�g�����")]
        bool _IsUseButton = false;

        [SerializeField, Tooltip("����1���")]
        PlayerWeaponBase _Weapon1Info = null;

        [SerializeField, Tooltip("����2���")]
        PlayerWeaponBase _Weapon2Info = null;

        [SerializeField, Tooltip("�v���C���[�̈ړ��p�R���|�[�l���g")]
        PlayerMove _MoveInfo = null;

        [SerializeField, Tooltip("�v���C���[���")]
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
            //�v���C���[�̃X�e�[�^�X�X�L���̕\�����\�z
            if (_IsUseButton)
            {
                int healed = (int)(_StatusInfo.Life + (_StatusInfo.MaxLife * 0.25f));
                if (healed > _StatusInfo.MaxLife) healed = (int)_StatusInfo.MaxLife;
                _PlayerStatusInfoTexts[0].text = $"{(int)_StatusInfo.Life} �� {healed}";

                byte nowLevel = _MoveInfo.MoveSpeedLevel;
                byte nextLevel = (byte)(nowLevel + 1);
                if (nextLevel > _MoveInfo.MoveSpeedMaxLevel)
                {
                    _PlayerStatusLevelUpButtons[1].interactable = false;
                    _PlayerStatusInfoTexts[1].text = $"Lv.{nowLevel} MAX!";
                }
                else _PlayerStatusInfoTexts[1].text = $"Lv.{nowLevel} �� Lv.{nextLevel}";
            }
            else
            {
                _PlayerStatusInfoTexts[0].text = $"{(int)_StatusInfo.Life} / {(int)_StatusInfo.MaxLife}";
                _PlayerStatusInfoTexts[1].text = $"Lv.{_MoveInfo.MoveSpeedLevel}";
            }


            //����1�̕\�����\�z
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
                    else _Weapon1LevelTexts[i].text = $"Lv.{nowLevel} �� Lv.{nextLevel}";
                }
                else _Weapon1LevelTexts[i].text = $"Lv.{_Weapon1Info.WeaponInfomations[i].Level}";
            }

            //����2��Lock�\��
            if(_IsUseButton && _StatusInfo.Level >= 5) _Weapon2UnlockButton.interactable = true;

            //����2�̕\�����\�z
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
                    else _Weapon2LevelTexts[i].text = $"Lv.{nowLevel} �� Lv.{nextLevel}";
                }
                else _Weapon2LevelTexts[i].text = $"Lv.{_Weapon2Info.WeaponInfomations[i].Level}";
            }
        }

        /// <summary>�Ώۂ��w�肵�ă��x���A�b�v</summary>
        /// <param name="index">���x���A�b�v�Ώہi�z��̗v�f�ԍ��j</param>
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
