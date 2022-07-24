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

        if (PlayerPrefs.HasKey("Brightness"))
        {
            setting.postExposure.value = brightnessSlider.value = PlayerPrefs.GetFloat("Brightness");
            setting.contrast.value = contrastSlider.value = PlayerPrefs.GetFloat("Contrast");
            setting.saturation.value = saturationSlider.value = PlayerPrefs.GetFloat("Saturation");
        }
        else
        {
            setting.postExposure.value = brightnessSlider.value = 0f;
            setting.contrast.value = contrastSlider.value = 0f;
            setting.saturation.value = saturationSlider.value = 0f;
        }
    }

    public void resetColour()
    {
        setting.postExposure.value = brightnessSlider.value = 0.01194698f;
        setting.contrast.value = contrastSlider.value = 0.83559f;
        setting.saturation.value = saturationSlider.value = 35.4568f;
    }

    public void Update()
    {
        setting.postExposure.value = brightnessSlider.value;
        setting.contrast.value = contrastSlider.value;
        setting.saturation.value = saturationSlider.value;
    }


    private void Save()
    {
        PlayerPrefs.SetFloat("Brightness", setting.postExposure.value);
        PlayerPrefs.SetFloat("Saturation", setting.saturation.value);
        PlayerPrefs.SetFloat("Contrast", setting.contrast.value);
    }
}
