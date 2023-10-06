using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBGM : MonoBehaviour
{
    AudioSource audioSource;
    private bool IsFade;
    private double FadeInSeconds = 3.0;
    private bool IsFadeIn = true;
    private double FadeDeltaTime = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (IsFadeIn && audioSource.volume <= 0.35)
        {
            FadeDeltaTime += Time.deltaTime;
            if (FadeDeltaTime >= FadeInSeconds)
            {
                FadeDeltaTime = FadeInSeconds;
                IsFadeIn = false;
            }
            audioSource.volume = (float)(FadeDeltaTime / FadeInSeconds);
        }
    }
}
