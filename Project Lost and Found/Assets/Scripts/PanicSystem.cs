using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Volume))]
public class PanicSystem : MonoBehaviour
{
    private StressData playerStress;

    private Volume stack;
    private Vignette pp_vignette;
    private ColorAdjustments pp_color;
    private Bloom pp_bloom;

    private float vignette_intensity_max;
    [SerializeField] private float vignette_activation_speed;

    private Color color_max;
    [SerializeField] private float color_activation_speed;

    private float bloom_intensity_max;
    [SerializeField] private float bloom_activation_speed;

    [SerializeField] [Range(0, 100)] private int vignetteActivation; 
    [SerializeField] [Range(0, 100)] private int colorActivation; 
    [SerializeField] [Range(0, 100)] private int bloomActivation;

    [SerializeField] [Range(0, 100)] private int heartBeatActivation;

    void Start()
    {
        playerStress = FindObjectOfType<StressData>();
        stack = GetComponent<Volume>();
        stack.profile.TryGet(out pp_vignette);
        stack.profile.TryGet(out pp_color);
        stack.profile.TryGet(out pp_bloom);

        vignette_intensity_max = pp_vignette.intensity.value;
        color_max = pp_color.colorFilter.value;
        bloom_intensity_max = pp_bloom.intensity.value;

        pp_vignette.intensity.value = 0.0f;
        pp_color.colorFilter.value = Color.white;
        pp_bloom.intensity.value = 0.0f;
    }

    void Update()
    {
        if(playerStress != null)
        {
            float stress = playerStress.GetStress();
            if (stress >= vignetteActivation)
                HandleVignette(false);
            else
                HandleVignette(true);

            if (stress >= colorActivation)
                HandleColor(false);
            else
                HandleColor(true);

            if (stress >= bloomActivation)
                HandleBloom(false);
            else
                HandleBloom(true);

            if (stress >= heartBeatActivation)
                HandleBeat();

        }
    }

    private void HandleVignette(bool down)
    {
        if (pp_color.colorFilter.value != color_max && !down)
            pp_color.colorFilter.value = Color.Lerp(pp_color.colorFilter.value, color_max, color_activation_speed);
        else if(pp_color.colorFilter.value != Color.white)
            pp_color.colorFilter.value = Color.Lerp(pp_color.colorFilter.value, Color.white, color_activation_speed);
    }
    private void HandleColor(bool down)
    {
        if (pp_vignette.intensity.value != vignette_intensity_max && !down)
            pp_vignette.intensity.value = Mathf.Lerp(pp_vignette.intensity.value, vignette_intensity_max, vignette_activation_speed);
        else if (pp_vignette.intensity.value > 0.01f)
            pp_vignette.intensity.value = Mathf.Lerp(pp_vignette.intensity.value, 0.0f, vignette_activation_speed);
    }
    private void HandleBloom(bool down)
    {
        if (pp_bloom.intensity.value != bloom_intensity_max && !down)
            pp_bloom.intensity.value = Mathf.Lerp(pp_bloom.intensity.value, bloom_intensity_max, bloom_activation_speed);
        else if (pp_bloom.intensity.value > 0.01f)
            pp_bloom.intensity.value = Mathf.Lerp(pp_bloom.intensity.value, 0.0f, bloom_activation_speed);
    }

    private void HandleBeat()
    {
        GameObject player = playerStress.gameObject;
        AudioSource aus;
        if (player.TryGetComponent<AudioSource>(out aus) && !aus.isPlaying)
        {
            aus.loop = true;
            aus.Play();
        }
        aus.pitch = 1.3f+playerStress.GetStress() / 100f;
    }
}
