using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ColorSettings : MonoBehaviour
{
    Slider brightnessSlider;
    Slider contrastSlider;
    Slider saturationSlider;

    private Volume v;
    private ColorAdjustments setting;

    UIManager colourSettings;
    private void Awake()
    {
        colourSettings = FindObjectOfType<UIManager>();
        brightnessSlider = colourSettings.GetBrightness();
        contrastSlider = colourSettings.GetContrast();
        saturationSlider = colourSettings.GetSaturation();
    }
    private void Start()
    {
        v = GetComponent<Volume>();
        v.profile.TryGet(out setting);

        brightnessSlider.maxValue = 1f;
        brightnessSlider.minValue = -2f;

        contrastSlider.maxValue = 100f;
        contrastSlider.minValue = -50f;

        saturationSlider.maxValue = 100f;
        saturationSlider.minValue = -100f;

        if (PlayerPrefs.GetInt("ONFIRSTTIMEOPENINGVIDEO",1) == 1)
        {
            //on first time opening game reset post processing
            PlayerPrefs.SetInt("ONFIRSTTIMEOPENINGVIDEO",0);
            resetColour();
        }
        else
        {
            setting.postExposure.value = brightnessSlider.value = PlayerPrefs.GetFloat("Brightness");
            setting.contrast.value = contrastSlider.value = PlayerPrefs.GetFloat("Contrast");
            setting.saturation.value = saturationSlider.value = PlayerPrefs.GetFloat("Saturation");
        }

        brightnessSlider.onValueChanged.AddListener(delegate{SetBrightness();});
        contrastSlider.onValueChanged.AddListener(delegate{SetContrast();});
        saturationSlider.onValueChanged.AddListener(delegate{SetSaturation();});

    }

    private void SetSaturation(){
        setting.saturation.value = saturationSlider.value;
        PlayerPrefs.SetFloat("Saturation",saturationSlider.value);
    }

    private void SetContrast(){
        setting.contrast.value = contrastSlider.value;
        PlayerPrefs.SetFloat("Contrast",contrastSlider.value);
    }

    private void SetBrightness(){
        setting.postExposure.value = brightnessSlider.value;
        PlayerPrefs.SetFloat("Brightness",brightnessSlider.value);
    }

    public void resetColour()
    {
        setting.postExposure.value = brightnessSlider.value = 0.01194698f;
        setting.contrast.value = contrastSlider.value = 0.83559f;
        setting.saturation.value = saturationSlider.value = 35.4568f;
    }
}
