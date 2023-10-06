using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;
using Const;

public class MainUI : BaseBehaviour
{
    [SerializeField] string startRoom;
    [SerializeField] float textSpeed;
    [SerializeField] public GameObject selectCanvas, sectionCanvas, space, splash, fireDestroy, armsDownTrigger;
    [SerializeField] Canvas menuCanvas;
    [SerializeField] FireAnim lightAnim;

    private TextMeshProUGUI sanityText, patientInfo, dialogue, section, selectItemName;

    private int sanity, startTaskCount;
    private string currentRoom;

    private bool check, isDisplay, isSpeak;

    // private Image taskCheck;
    // private Animator taskAnim;
    // private float delaySec = 1f;
    // private PlayableDirector typingDirector;
    static MainUI instance;

    private void Start()
    {
        instance = this;

        patientInfo = transform.Find("Canvas/PatientInfo/Info").gameObject.GetComponent<TextMeshProUGUI>();
        dialogue = transform.Find("Canvas/Dialogue").gameObject.GetComponent<TextMeshProUGUI>();

        section = transform.Find("SectionCanvas/SectionText").gameObject.GetComponent<TextMeshProUGUI>();

        selectItemName = selectCanvas.transform.Find("Select/SelectItem").gameObject.GetComponent<TextMeshProUGUI>();
        selectCanvas.SetActive(false);

        fade = transform.Find("FadeCanvas").gameObject.GetComponent<Animator>();

        CurrentRoom = startRoom == "" ? GlobalConst.START_ROOM : startRoom;

        // sanityText = transform.Find("Canvas/Sanity").gameObject.GetComponent<TextMeshProUGUI>();
        // sanity = GlobalConst.START_SANITY;
        // sanityText.text = sanity.ToString();

        // taskCheck = task.transform.Find("TaskCheck").gameObject.GetComponent<Image>();
        // taskCheck.enabled = check;
        // taskAnim = task.GetComponent<Animator>();
        // task.text = CurrentRoom + GlobalConst.START_TASK;
        // startTaskCount = GlobalConst.TASK_COUNT;

        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.MainFloor);

        space.SetActive(isSpeak);
        splash.SetActive(false);
        fireDestroy.SetActive(false);
        armsDownTrigger.SetActive(false);
        lightAnim.enabled = false;

        // section.transform.parent.gameObject.SetActive(false);
    }

    public static MainUI GetInstance()
    {
        return instance;
    }

    public void CheckInfo()
    {
        patientInfo.text = GlobalConst.RoomChart(CurrentRoom);
        // if (task.text == CurrentRoom + GlobalConst.START_TASK)
        // {
        //     CheckTask(CurrentRoom + GlobalConst.NEXT_TASK);
        // }
    }

    public string CurrentRoom
    {
        get { return currentRoom; }
        set { currentRoom = value; }
    }

    // public bool ChangeRoomTask()
    // {
    //     if (task.text == CurrentRoom + GlobalConst.NEXT_TASK)
    //     {
    //         CheckTask(GlobalConst.RoomTask(CurrentRoom) + "(" + startTaskCount.ToString() + "/" + GlobalConst.RoomTaskNum(CurrentRoom) + ")");
    //         return true;
    //     }
    //     else
    //     {
    //         return false;
    //     }
    // }

    // public int SetSanity
    // {
    //     get { return sanity; }
    //     set
    //     {
    //         sanity = value;
    //         sanityText.text = sanity.ToString();
    //     }
    // }

    public void Speak(string text)
    {
        if (!IsSpeak && text != "")
        {
            playerMoveScript.CursorLock = false;
            StartCoroutine(SetDisplayDialogue(text));
        }
    }

    public IEnumerator SetDisplayDialogue(string text)
    {
        int textCount = 0;
        dialogue.text = "";

        while (text.Length > textCount)
        {
            dialogue.text += text[textCount];
            textCount++;
            yield return new WaitForSeconds(textSpeed);
        }
        if (text.Length == textCount)
        {
            IsSpeak = true;
            space.SetActive(IsSpeak);
        }
    }

    public void ResetDialogue()
    {
        dialogue.text = "";
        IsSpeak = false;
        space.SetActive(IsSpeak);
    }

    // public void CheckTask(string taskText)
    // {
    //     taskCheck.enabled = !check;
    //     taskAnim.SetFloat("Speed", 1);
    //     StartCoroutine(DelayStop(delaySec, () =>
    //             {
    //                 task.text = taskText;
    //                 taskCheck.enabled = check;
    //             }
    //         )
    //     );
    //     StartCoroutine(DelayStop(delaySec + 1f, () =>
    //             {
    //                 taskAnim.SetFloat("Speed", 0);
    //             }
    //         )
    //     );
    // }

    // public void PlayTimeLine(string text)
    // {
    //     if (typingDirector == null)
    //     {
    //         return;
    //     }
    //     if (text == "prologue")
    //     {
    //         typingDirector.initialTime = 0.5f;
    //     }
    //     section.transform.parent.gameObject.SetActive(true);
    //     section.text = GlobalConst.Story(text);
    //     typingDirector.Play();
    // }

    // public void ClearRoom()
    // {
    //     section.transform.parent.gameObject.SetActive(true);
    //     section.text = GlobalConst.RoomEnding(CurrentRoom);
    //     typingDirector.Play();

    //     hospitalRoom.SetActive(true);

    //     int room = int.Parse(CurrentRoom);
    //     room += 1;
    //     CurrentRoom = room.ToString();
        
    //     ResetDialogue();

    //     StartCoroutine(DelayStop(10.0f, () =>
    //             {
    //                 task.text = CurrentRoom + GlobalConst.START_TASK;
    //                 StartPlayer();
    //                 SoundManager.Instance.PlayBGM(BGMSoundData.BGM.MainFloor);
    //                 hospitalRoom = GameObject.Find("hospital_room_" + CurrentRoom);
    //                 if (hospitalRoom != null)
    //                 {
    //                     hospitalRoom.SetActive(false);
    //                 }
    //             }
    //         )
    //     );
    // }

    public void CountUpRoom()
    {
        int room = int.Parse(CurrentRoom);
        room += 1;
        CurrentRoom = room.ToString();

        callTriggerScript.IsCallable = true;
        callTriggerScript.IsCaught = false;
    }

    public bool IsDisplay
    {
        get { return isDisplay; }
        set { isDisplay = value; }
    }

    public bool IsSpeak
    {
        get { return isSpeak; }
        set { isSpeak = value; }
    }

    public void FadeIn(bool flg)
    {
        fade.SetBool("Fade", flg);
    }

    public void SelectMenu()
    {
        selectCanvas.SetActive(DisplayChange());
    }

    public void SettingMenu()
    {
        menuCanvas.enabled = DisplayChange();
    }

    public bool DisplayChange()
    {
        IsDisplay = !isDisplay;
        playerMoveScript.CursorLock = !IsDisplay;
        playerMoveScript.CursorVisible = !IsDisplay;
        return IsDisplay;
    }

    public void SetItemName(string name)
    {
        selectItemName.text = name + "をどうする？";
    }

    public void RoomEvent()
    {
        switch(CurrentRoom){
            case "701":
                Speak(GlobalConst.RoomDialogue(CurrentRoom));
                CountUpRoom();
                StartCoroutine(DelayStop(4.0f, () =>
                        {
                            SoundManager.Instance.PlaySE(SESoundData.SE.WallLight);
                            lightAnim.enabled = true;
                        }
                    )
                );
                break;
            case "703":
                fireDestroy.SetActive(true);
                Speak(GlobalConst.RoomDialogue(CurrentRoom));
                CountUpRoom();
                break;
            case "705":
                armsDownTrigger.SetActive(true);
                Speak(GlobalConst.RoomDialogue(CurrentRoom));
                CountUpRoom();
                break;
            case "706":
                Speak(GlobalConst.RoomDialogue(CurrentRoom));
                Destroy(GameObject.Find("CallTrigger"));
                splash.SetActive(true);
                break;
            default:
                Speak(GlobalConst.RoomDialogue(CurrentRoom));
                CountUpRoom();
                break;
        }
    }

    // public void CountUpTask()
    // {
    //     audioSource.PlayOneShot(rayActionScript.getSound);
    //     startTaskCount++;
    //     task.text = GlobalConst.RoomTask(CurrentRoom) + "(" + startTaskCount.ToString() + "/" + GlobalConst.RoomTaskNum(CurrentRoom) + ")";
    //     // if (startTaskCount == GlobalConst.RoomTaskNum(CurrentRoom))
    //     // {
    //     //     ClearRoom();
    //     // }
    // }
}
