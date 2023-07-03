using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Events;
using System;

public class ChangePostProcessing : MonoBehaviour
{   
    [HideInInspector] public UnityEvent OnSlowDownStart,
                                         OnSlowDownEnd,
                                         OnBoosterStart,
                                         OnBoosterEnd,
                                         OnPlayerDeath;
    private Volume _v;
    private Bloom _bloom;
    private ChromaticAberration _chromaticAberration;
    private Vignette _vignette;

    private float lerpTimer;
    [SerializeField] private float chipSpeed = .5f;

    private bool _chromaticAberrationBool = false,
                _vignetteBool = false;



    void Start(){
        _v = GetComponent<Volume>();
        _v.profile.TryGet<Bloom>(out _bloom);
        _v.profile.TryGet<ChromaticAberration>(out _chromaticAberration);
        _v.profile.TryGet<Vignette>(out _vignette);

        OnSlowDownStart.AddListener(SetChromaticAberrationTrue);
        OnSlowDownEnd.AddListener(SetChromaticAberrationFalse);
        OnBoosterStart.AddListener(SetVignetteTrue);
        OnBoosterEnd.AddListener(SetVignetteFalse);
        OnPlayerDeath.AddListener(ResetValues);

        _vignette.intensity.value = Mathf.Clamp(_vignette.intensity.value,0,.2f);
        _chromaticAberration.intensity.value = Mathf.Clamp(_chromaticAberration.intensity.value,0,.2f);

    }

    private void ResetValues()
    {
        Debug.LogWarning("Reset Post Processing");
        _chromaticAberrationBool = false;
        _vignetteBool = false;
    }

    private void SetVignetteFalse()
    {
        lerpTimer = 0;
        _vignetteBool = false;
    }

    private void SetVignetteTrue()
    {
        lerpTimer = 0;
        _vignetteBool = true;
        _chromaticAberrationBool = false;
    }

    private void SetChromaticAberrationTrue()
    {
        lerpTimer = 0;
        _chromaticAberrationBool = true;
    }
    private void SetChromaticAberrationFalse()
    {
        lerpTimer = 0;
        _chromaticAberrationBool = false;
    }

    private void Update(){
        LerpChromaticAberration();
        LerpVignette();
    }

    private void LerpVignette()
    {
        if(_vignetteBool)LerpVignetteIntensityValue(0,.2f);
        else if(!_vignetteBool)LerpVignetteIntensityValue(.2f,0);
    }

    private void LerpChromaticAberration()
    {
        if(_chromaticAberrationBool)LerpChromaticAberrationIntensityValue(0,.2f);
        else if(!_chromaticAberrationBool)LerpChromaticAberrationIntensityValue(.2f,0);
    }

    private void LerpChromaticAberrationIntensityValue(float _start, float _end)
    {
        lerpTimer += Time.unscaledDeltaTime;
        float percentCompleted = lerpTimer / chipSpeed;
        _chromaticAberration.intensity.value = Mathf.Lerp(_start, _end, percentCompleted);
    }
    private void LerpVignetteIntensityValue(float _start, float _end)
    {
        lerpTimer += Time.unscaledDeltaTime;
        float percentCompleted = lerpTimer / chipSpeed;
        _vignette.intensity.value = Mathf.Lerp(_start, _end, percentCompleted);
    }
}

