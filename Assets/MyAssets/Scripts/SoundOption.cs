using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>���ʃI�v�V�������{�s</summary>
public class SoundOption : MonoBehaviour
{
    [SerializeField, Tooltip("AudioMixer�̃p�����[�^�� : BGM�p�{�����[��")]
    string _ParamNameBGMVolume = "BGMVolume";

    [SerializeField, Tooltip("AudioMixer�̃p�����[�^�� : SE�p�{�����[��")]
    string _ParamNameSEVolume = "SEVolume";

    [SerializeField, Tooltip("���ʒ�����������Ώۂ�AudioMixer")]
    AudioMixer _AudioMixer = null;

    [SerializeField, Tooltip("BGM�̉��ʒ���������X���C�h�o�[")]
    Slider _BGMSlider = null;

    [SerializeField, Tooltip("SE�̉��ʒ���������X���C�h�o�[")]
    Slider _SESlider = null;

    private void Start()
    {
        //���݂�AudioMixer����BGM���ʂ��擾���A�X���C�h�o�[�ɔ��f
        _AudioMixer.GetFloat(_ParamNameBGMVolume, out float bgmVolume);
        _BGMSlider.value = bgmVolume;

        //���݂�AudioMixer����SE���ʂ��擾���A�X���C�h�o�[�ɔ��f
        _AudioMixer.GetFloat(_ParamNameSEVolume, out float seVolume);
        _SESlider.value = seVolume;
    }

    /// <summary>BGM�X���C�h�o�[�̐��l�����ۂ�AudioMixer�ɔ��f�����邽�߂̃��\�b�h</summary>
    /// <param name="volume">AudioMixer�ɔ��f����{�����[���l</param>
    public void SetBGM(float volume)
    {
        _AudioMixer.SetFloat(_ParamNameBGMVolume, volume);
    }

    /// <summary>SE�X���C�h�o�[�̐��l�����ۂ�AudioMixer�ɔ��f�����邽�߂̃��\�b�h</summary>
    /// <param name="volume">AudioMixer�ɔ��f����{�����[���l</param>
    public void SetSE(float volume)
    {
        _AudioMixer.SetFloat(_ParamNameSEVolume, volume);
    }
}