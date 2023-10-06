using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSetting : MonoBehaviour
{
    public void ListenerSetting(float intensity)
    {
        AudioListener.volume = intensity;
    }
}
