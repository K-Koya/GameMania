using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>音量オプションを施行</summary>
public class SoundOption : MonoBehaviour
{
    [SerializeField, Tooltip("AudioMixerのパラメータ名 : BGM用ボリューム")]
    string _ParamNameBGMVolume = "BGMVolume";

    [SerializeField, Tooltip("AudioMixerのパラメータ名 : SE用ボリューム")]
    string _ParamNameSEVolume = "SEVolume";

    [SerializeField, Tooltip("音量調整をかける対象のAudioMixer")]
    AudioMixer _AudioMixer = null;

    [SerializeField, Tooltip("BGMの音量調整をするスライドバー")]
    Slider _BGMSlider = null;

    [SerializeField, Tooltip("SEの音量調整をするスライドバー")]
    Slider _SESlider = null;

    private void Start()
    {
        //現在のAudioMixer中のBGM音量を取得し、スライドバーに反映
        _AudioMixer.GetFloat(_ParamNameBGMVolume, out float bgmVolume);
        _BGMSlider.value = bgmVolume;

        //現在のAudioMixer中のSE音量を取得し、スライドバーに反映
        _AudioMixer.GetFloat(_ParamNameSEVolume, out float seVolume);
        _SESlider.value = seVolume;
    }

    /// <summary>BGMスライドバーの数値を実際にAudioMixerに反映させるためのメソッド</summary>
    /// <param name="volume">AudioMixerに反映するボリューム値</param>
    public void SetBGM(float volume)
    {
        _AudioMixer.SetFloat(_ParamNameBGMVolume, volume);
    }

    /// <summary>SEスライドバーの数値を実際にAudioMixerに反映させるためのメソッド</summary>
    /// <param name="volume">AudioMixerに反映するボリューム値</param>
    public void SetSE(float volume)
    {
        _AudioMixer.SetFloat(_ParamNameSEVolume, volume);
    }
}