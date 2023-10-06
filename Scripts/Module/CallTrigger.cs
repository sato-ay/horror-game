using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

public class CallTrigger : BaseBehaviour
{
    [SerializeField] float randStartZ, randEndZ;
    [SerializeField] Light telephoneLight;

    private bool  isCallable, isCalling, isCaught;
    public static CallTrigger Instance { get; private set; }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        isCallable = true;
        telephoneLightSwitch(false);
    }

    private void Update()
    {
        if (isPlayer)
        {
            if (!IsCalling && IsCallable && !IsCaught)
            {
                IsCalling = true;
                IsCallable = false;

                telephoneLightSwitch(true);

                string room = MainUI.GetInstance().CurrentRoom;
                SoundManager.Instance.PlaySE(SelectSE(room));

                if (room == "701")
                {
                    StartCoroutine(DelayStop(1.0f, () =>
                            {
                                MainUI.GetInstance().Speak(GlobalConst.SetDialogue("Call"));
                            }
                        )
                    );
                }

                RandPosZ();
            }
        }
    }

    public void telephoneLightSwitch(bool flg)
    {
        telephoneLight.enabled = flg;
    }

    private SESoundData.SE SelectSE(string room)
    {
        SESoundData.SE se;
        switch(room){
            case "701":
                se = SESoundData.SE.Call1;
                break;
            case "702":
                se = SESoundData.SE.Call2;
                break;
            case "703":
                se = SESoundData.SE.Call3;
                break;
            case "704":
                se = SESoundData.SE.Call4;
                break;
            case "705":
                se = SESoundData.SE.Call5;
                break;
            case "706":
                se = SESoundData.SE.Call6;
                break;
            default:
                se = SESoundData.SE.Call1;
                break;
        }

        return se;
    }

    private void RandPosZ() {
        Vector3 pos = transform.position;
        pos.z = Random.Range(randStartZ, randEndZ);
        transform.position = pos;
    }

    // コール鳴らしていいかどうか判定 trueなら鳴らしてヨシ
    public bool IsCallable
    {
        get { return isCallable; }
        set { isCallable = value; }
    }

    // コール鳴ってるかどうか
    public bool IsCalling
    {
        get { return isCalling; }
        set { isCalling = value; }
    }

    // コールが鳴って受電したかどうか
    public bool IsCaught
    {
        get { return isCaught; }
        set { isCaught = value; }
    }
}
