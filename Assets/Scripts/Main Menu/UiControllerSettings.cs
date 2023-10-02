using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiControllerSettings : MonoBehaviour
{
    private const float maxVolume = 80;

    public event Action onSettingsCloseButtonClicked;
    public event Action onLanguageChanged;

    [SerializeField] private UnityEngine.Audio.AudioMixer audioMixer;
    [SerializeField] private AnimationCurve logarithm;

    public CanvasGroup canvasGroup;
    [SerializeField] private Slider sliderVolumeGeneral;
    [SerializeField] private Slider sliderVolumeMusic;
    [SerializeField] private Slider sliderVolumeEffect;
    [SerializeField] private TMPro.TMP_Dropdown dropdownLanguage;
    [SerializeField] private Button closeButton;

    private void Awake ()
    {
        sliderVolumeGeneral.onValueChanged.AddListener(OnSliderVolumeGeneralChanged);
        sliderVolumeMusic.onValueChanged.AddListener(OnSliderVolumeMusicChanged);
        sliderVolumeEffect.onValueChanged.AddListener(OnSliderVolumeEffectChanged);
        closeButton.onClick.AddListener(OnSettingsCloseButtonClicked);

        SetupDropdown();
    }

    private void OnDestroy ()
    {
        sliderVolumeGeneral.onValueChanged.RemoveAllListeners();
        sliderVolumeMusic.onValueChanged.RemoveAllListeners();
        sliderVolumeEffect.onValueChanged.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();
    }

    private void OnSliderVolumeGeneralChanged(float volume)
    {
        float newValue = logarithm.Evaluate(volume) * maxVolume - maxVolume;
        audioMixer.SetFloat("VolumeGeneral", newValue);
    }

    private void OnSliderVolumeMusicChanged(float volume)
    {
        float newValue = logarithm.Evaluate(volume) * maxVolume - maxVolume;
        audioMixer.SetFloat("VolumeMusic", newValue);
    }

    private void OnSliderVolumeEffectChanged(float volume)
    {
        float newValue = logarithm.Evaluate(volume) * maxVolume - maxVolume;
        audioMixer.SetFloat("VolumeEffect", newValue);
    }

    private void SetupDropdown()
    {
        List<TMP_Dropdown.OptionData> languageList = new List<TMP_Dropdown.OptionData>();

        dropdownLanguage.ClearOptions();

        for (short i = 0; i < Enum.GetNames(typeof(Loc.Language)).Length; i++)
        {
            TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData();

            Loc.Language language = (Loc.Language)i;
            data.text = language.ToString();

            languageList.Add(data);
        }

        dropdownLanguage.AddOptions(languageList);
        dropdownLanguage.onValueChanged.AddListener(delegate
        {
            ChangeLanguage(dropdownLanguage);
            onLanguageChanged?.Invoke();
        });

        dropdownLanguage.value = (int)Loc.currentLanguage;
    }

    void ChangeLanguage(TMP_Dropdown change)
    {
        Loc.currentLanguage = (Loc.Language)change.value;
    }

    private void OnSettingsCloseButtonClicked () => onSettingsCloseButtonClicked?.Invoke();
}