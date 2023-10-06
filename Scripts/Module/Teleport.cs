using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Const;

public class Teleport : BaseBehaviour
{
    // private bool teleport = true;

    // private void Update() {
    //     if (isPlayer)
    //     {
    //         if (transform.parent.gameObject.name != (GlobalConst.ROOM_NAME + mainUIScript.CurrentRoom))
    //         {
    //             string parentName = transform.parent.gameObject.name;
    //             string room = parentName.Replace(GlobalConst.ROOM_NAME, "");
    //             mainUIScript.Speak(room + GlobalConst.ENT_TEXT);
    //         }
    //         else if (teleport && mainUIScript.ChangeRoomTask())
    //         {
    //             mainUIScript.ResetDialogue();
    //             mainUIScript.FadeIn(true);
    //             SoundManager.Instance.StopBGM();
    //             StartCoroutine(DelayStop(1f, () =>
    //                     {
    //                         player.transform.position = RoomPos;
    //                         mainUIScript.FadeIn(false);
    //                         SoundManager.Instance.PlayBGM(BGMSoundData.BGM.RoomAmbient);
    //                         teleport = false;
    //                     }
    //                 )
    //             );
    //         }
    //         else if (teleport && !mainUIScript.ChangeRoomTask())
    //         {
    //             mainUIScript.Speak(GlobalConst.SetDialogue("NoInfo"));
    //         }
    //     }
    // }
}
