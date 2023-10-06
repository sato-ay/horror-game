using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSplash : BaseBehaviour
{
    private bool sound;
    // Update is called once per frame
    void Update()
    {
        if (isPlayer && !sound)
        {
            sound = !sound;
            SoundManager.Instance.PlaySE(SESoundData.SE.Dressing);
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.RoomAmbient);

            transform.GetChild(0).gameObject.SetActive(true);
            this.gameObject.GetComponent<BoxCollider>().enabled = false;

            StartCoroutine(DelayStop(2.0f, () =>
                    {
                        transform.GetChild(1).gameObject.SetActive(true);
                    }
                )
            );
        }
    }
}
