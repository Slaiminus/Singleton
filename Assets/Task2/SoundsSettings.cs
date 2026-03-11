using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundsSettings : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgVolumeSlider;
    [SerializeField] private AudioMixer mixer;

    private const string MasterVolumeName = "Master";
    private const string BgVolumeName = "BGMusic";

    public static SoundsSettings Instance { get; private set; }
    private void Awake()
    {
        masterSlider.onValueChanged.AddListener(OnMasterSliderVolumeChanged);
        bgVolumeSlider.onValueChanged.AddListener(OnBgSliderVolumeChanged);
    }

    #region load

    private void Start()
    {
        TryLoad(MasterVolumeName, masterSlider);
        TryLoad(BgVolumeName, bgVolumeSlider);
    }

    private void TryLoad(string key, Slider slider)
    {
        if (PlayerPrefs.HasKey(key))
        {
            float volume = PlayerPrefs.GetFloat(key);
            slider.value = volume;
        }
    }
    #endregion

    private void OnMasterSliderVolumeChanged(float value) =>
        SetMixerVolume(value, MasterVolumeName);
    private void OnBgSliderVolumeChanged(float value) =>
        SetMixerVolume(value, BgVolumeName);

    private void SetMixerVolume(float value, string paramName)
    {
        float volume = GetVolumeDb(value);
        mixer.SetFloat(paramName, value);
        PlayerPrefs.SetFloat(paramName, value);
    }

    private float GetVolumeDb(float value) =>
        Mathf.Log10(value) * 20;
}
