using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

public class RayAction : BaseBehaviour
{
    [SerializeField] Animator playerHandAnim;
    private ActionText actionScript;
    private Put putScript;
    private Light flashLight;

    public AudioClip getSound, doorOpen, doorClose, eat, itemGet, lightSwitch, file, getBody;

    private bool disp, sp;
    private int dialogueCount = 0;

    private void Start()
    {
        actionScript = transform.parent.gameObject.GetComponent<ActionText>();
        putScript = GetComponent<Put>();

        flashLight = GameObject.Find("WhiteLight").GetComponent<Light>();
    }

    private void Update() {
        if (Input.GetKeyDown("q"))
        {
            audioSource.PlayOneShot(lightSwitch);
            flashLight.enabled = !flashLight.enabled;
        }

        if (Input.GetKeyDown("space"))
        {
            if (mainUIScript.IsSpeak)
            {
                playerMoveScript.CursorLock = true;
                mainUIScript.ResetDialogue();
            }
            else
            {
                return;
            }
        }

        if (!mainUIScript.space.activeSelf && !mainUIScript.sectionCanvas.activeSelf && Input.GetKeyDown("m"))
        {
            if (!mainUIScript.IsDisplay)
            {
                SettingMenu.GetInstance().Open();
            }
            else
            {
                SettingMenu.GetInstance().Close();
            }
        }

        if (mainUIScript.IsSpeak && SettingMenu.GetInstance().GetMenuBool())
        {
            SettingMenu.GetInstance().Close();
        }

        if (!mainUIScript.IsDisplay)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit, 2.0f))
            {
                GameObject targetObj = hit.collider.gameObject;

                actionScript.ActionDisplay(TargetObjAction(targetObj));
            }
            else
            {
                actionScript.ActionDisplay(null);
            }
        }
    }

    private string TargetObjAction(GameObject targetObj) {
        string actionName = null;
        if (targetObj.tag == "Door")
        {
            // 部屋のドアを開けられる条件は受電済
            if (targetObj.name == "Door" || (targetObj.name == "Door_" + mainUIScript.CurrentRoom && callTriggerScript.IsCaught))
            {
                actionName = GlobalConst.ActionName(targetObj.tag);
                if (Input.GetKeyDown("e"))
                {
                    DoorOpener(targetObj);
                }
            }
        }

        if (targetObj.tag == "Food")
        {
            actionName = GlobalConst.ActionName(targetObj.tag);
            if (Input.GetKeyDown("e"))
            {
                Destroy(targetObj);
                audioSource.PlayOneShot(eat);
            }
        }

        if (targetObj.name == "Chart")
        {
            actionName = GlobalConst.ActionName(targetObj.name);
            if (Input.GetKeyDown("e"))
            {
                Animator infoAnim = GameObject.Find("PatientInfo").GetComponent<Animator>();

                disp = !disp;
                infoAnim.SetBool("Open", disp);
                playerMoveScript.CursorLock = !disp;
                audioSource.PlayOneShot(file);
                mainUIScript.CheckInfo();
            }
        }

        if (targetObj.tag == "Check")
        {
            actionName = GlobalConst.ActionName(targetObj.tag);
            string t = "";

            if (targetObj.name == "Bear")
            {
                t = GlobalConst.SetDialogue(targetObj.name + dialogueCount);
                if (t != "")
                {
                    if (dialogueCount == 0 && Input.GetKeyDown("e"))
                    {
                        mainUIScript.Speak(t);
                        dialogueCount++;
                    }
                    if (dialogueCount > 0 && Input.GetKeyDown("space"))
                    {
                        mainUIScript.Speak(t);
                        dialogueCount++;
                    }
                }
                else
                {
                    if (!sp)
                    {
                        sp = !sp;
                        StartCoroutine(DelayStop(2.0f, () =>
                                {
                                    SoundManager.Instance.PlaySE(SESoundData.SE.Asobo);
                                }
                            )
                        );
                        StartCoroutine(DelayStop(3.5f, () =>
                                {
                                    SoundManager.Instance.PlaySE(SESoundData.SE.Asoboyo);
                                }
                            )
                        );
                        StartCoroutine(DelayStop(5.0f, () =>
                                {
                                    mainUIScript.Speak(GlobalConst.SetDialogue("Surprised1"));
                                }
                            )
                        );
                    }
                    StartCoroutine(DelayStop(5.0f, () =>
                            {
                                dialogueCount = 0;
                            }
                        )
                    );
                }
            }
            if (targetObj.name == "Statue" || targetObj.name == "Face")
            {
                t = GlobalConst.SetDialogue(targetObj.name);
                if (t != "" && playerMoveScript.CursorLock)
                {
                    if (Input.GetKeyDown("e"))
                    {
                        mainUIScript.Speak(t);
                    }
                }
            }
            if (targetObj.name.StartsWith("TV"))
            {
                int num = Random.Range(0, 3);

                t = GlobalConst.SetDialogue("TV" + num);
                if (t != "" && playerMoveScript.CursorLock)
                {
                    if (Input.GetKeyDown("e"))
                    {
                        MaterialController mc = MaterialController.Instance;
                        mc.MonitorChange();
                        mainUIScript.Speak(t);
                    }
                }
            }
        }

        if (targetObj.tag == "Sharp")
        {
            actionName = GlobalConst.ActionName("Check");
            int num = Random.Range(0, 3);

            string t = GlobalConst.SetDialogue("Fear" + num);
            if (t != "" && playerMoveScript.CursorLock)
            {
                if (Input.GetKeyDown("e"))
                {
                    mainUIScript.Speak(t);
                }
            }
        }

        if (targetObj.tag == "Phone" && callTriggerScript.IsCalling)
        {
            actionName = GlobalConst.ActionName(targetObj.tag);
            SoundManager sm = SoundManager.Instance;
            if (Input.GetKeyDown("e"))
            {
                if (playerMoveScript.CursorLock)
                {
                    playerHandAnim.SetBool("Catch", callTriggerScript.IsCalling);
                    callTriggerScript.telephoneLightSwitch(false);

                    if (sm.StopSE()) sm.PlaySE(SESoundData.SE.Catch);

                    string room = mainUIScript.CurrentRoom.ToString();
                    mainUIScript.Speak(GlobalConst.CheckRoom(room));
                }
            }
            if (Input.GetKeyDown("space"))
            {
                callTriggerScript.IsCaught = true;
                callTriggerScript.IsCalling = false;
                playerHandAnim.SetBool("Catch", callTriggerScript.IsCalling);
                sm.PlaySE(SESoundData.SE.Put);
                if (mainUIScript.CurrentRoom == "706")
                {
                    sm.PlaySE(SESoundData.SE.Help);
                    StartCoroutine(DelayStop(2.0f, () =>
                            {
                                mainUIScript.Speak(GlobalConst.SetDialogue("Surprised2"));
                            }
                        )
                    );
                }
            }
        }

        if (targetObj.tag == "Item")
        {
            actionName = GlobalConst.ActionName(targetObj.tag);
            if (Input.GetKeyDown("e"))
            {
                ObtainableItem obtItem = targetObj.GetComponent<ObtainableItem>();
                obtItem.Touch(targetObj);
            }
        }

        return actionName;
    }

    private void DoorOpener(GameObject obj)
    {
        GameObject parentObj = obj.transform.parent.gameObject;
        Animator doorAnim = parentObj.GetComponent<Animator>();
        doorAnim.SetBool("Open", !doorAnim.GetBool("Open"));
        AudioClip doorSound = doorAnim.GetBool("Open") ? doorOpen : doorClose;
        playerHandAnim.SetBool("Push", true);

        float delaySec = doorAnim.GetBool("Open") ? 0.1f : 0.7f;
        StartCoroutine(DelayStop(delaySec, () =>
                {
                    audioSource.PlayOneShot(doorSound);
                    playerHandAnim.SetBool("Push", false);
                }
            )
        );
    }
}
