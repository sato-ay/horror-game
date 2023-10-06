using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class ExposureSetting : MonoBehaviour
{
    [SerializeField] Volume postFX;
    private Exposure exposure;

    private void Awake()
    {
        postFX.profile.TryGet(out exposure);
    }

    public void LightSetting(float intensity)
    {
        if (exposure == null)
        {
            return;
        }
        exposure.fixedExposure.value = intensity;
    }
}
