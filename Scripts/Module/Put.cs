using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Const;

public class Put : BaseBehaviour
{
    private PlayableDirector stageDirector;
    private void Start() {
        stageDirector = GameObject.Find("ZombieScream").GetComponent<PlayableDirector>();
    }

    // public void ItemPut(GameObject putTarget)
    // {
    //     if (putTarget.name.StartsWith("Trolley") || putTarget.name.StartsWith("FakeTrolley"))
    //     {
    //         bool fake = putTarget.name.StartsWith("FakeTrolley") ? true : false;
    //         if (Input.GetKeyDown("e"))
    //         {
    //             putTarget.transform.GetChild(0).gameObject.SetActive(true);
    //             PutHoldItem(putTarget);

    //             float delaySec = fake ? 0.3f : 0.1f;
    //             StartCoroutine(DelayStop(delaySec, () =>
    //                     {
    //                         if (fake)
    //                         {
    //                             stageDirector.Play();
    //                             putTarget.transform.GetChild(0).gameObject.SetActive(false);
    //                             mainUIScript.SetSanity -= 10;

    //                             int num = Random.Range(1, 4);
    //                             string diaNum = "Fear" + num.ToString();
    //                             mainUIScript.Speak(GlobalConst.SetDialogue(diaNum));
    //                         }
    //                         else
    //                         {
    //                             mainUIScript.CountUpTask();
    //                         }
    //                     }
    //                 )
    //             );
    //         }
    //     }

    //     if (putTarget.name.StartsWith("Chair"))
    //     {
    //         if (Input.GetKeyDown("e"))
    //         {
    //             PutHoldItem(putTarget);

    //             GameObject sitPos = putTarget.transform.Find("SitPos").gameObject;
    //             GameObject.FindGameObjectWithTag("HoldItem").transform.position = sitPos.transform.position;
    //         }
    //     }
    // }

    private void PutHoldItem(GameObject putTarget)
    {
        Destroy(GameObject.FindGameObjectWithTag("HoldItem"));
        putTarget.tag = "Untagged";
        isHold = false;
    }
}
