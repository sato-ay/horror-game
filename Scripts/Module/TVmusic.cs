using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVmusic : MonoBehaviour
{
    private bool playing;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player")) {
            playing = true;
            PlayingMusic(SESoundData.SE.TV);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player")) {
            playing = false;
            PlayingMusic(SESoundData.SE.TV);
        }
    }

    public void PlayingMusic(SESoundData.SE se)
    {
        if (playing)
        {
            SoundManager.Instance.PlaySE(se);
        }
        if (!playing)
        {
            playing = !SoundManager.Instance.StopSE();
        }
    }
}
