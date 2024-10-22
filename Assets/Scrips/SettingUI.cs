using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private float mixerMultiplayer = 25;


    [Header("SFX setting")]
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TextMeshProUGUI sfxSliderText;
    [SerializeField] private string sfxParamater;

    [Header("BGM setting")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private TextMeshProUGUI bgmSliderText;
    [SerializeField] private string bgmParamater;




    public void SFXSliderValue(float value)
    {
        float newValue = Mathf.Log10(value) * mixerMultiplayer;
        audioMixer.SetFloat(sfxParamater, newValue);
    }
    public void BGMSliderValue(float value)
    {
        float newValue = Mathf.Log10(value) * mixerMultiplayer;
        audioMixer.SetFloat(bgmParamater, newValue);
    }

    // Start is called before the first frame update
    private void OnDisable()
    {
        PlayerPrefs.SetFloat(sfxParamater, sfxSlider.value);
        PlayerPrefs.SetFloat(bgmParamater, bgmSlider.value);
    }
    private void OnEnable()
    {
        sfxSlider.value = PlayerPrefs.GetFloat(sfxParamater, .7f);
        bgmSlider.value = PlayerPrefs.GetFloat(bgmParamater, .7f);
    }
   
}
