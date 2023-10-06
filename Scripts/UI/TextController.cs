using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Const;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TextController : BaseBehaviour {
    [SerializeField][Range(0.001f, 0.3f)]
    float intervalForCharDisplay = 0.05f;   // 1文字の表示にかける時間
    [SerializeField] private PlayableDirector openingDirector;
    [SerializeField] private PlayableDirector endingDirector;
    [SerializeField] private GameObject endRollObj;
    [SerializeField] private PlayableDirector endRollDirector;

    private TextMeshProUGUI sectionText;   // uiTextへの参照
    private int currentSentenceNum = 1; //現在表示している文章番号
    private string section = "p";
    private bool complete;
    static TextController textInstance;

    void Start () {
        textInstance = this;
        mainUIScript.DisplayChange();

        endRollObj.SetActive(false);

        sectionText = this.gameObject.GetComponent<TextMeshProUGUI>();
        StartText();
    }

    public static TextController GetInstance() {
        return textInstance;
    }

    public void StartText () {
        if (section == "e")
        {
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Ending);
        }
        StartCoroutine(SetDisplaySection(GlobalConst.Story(section + currentSentenceNum.ToString())));
    }

    public void StartEnding() {
        endingDirector.Play();

        section = "e";
        currentSentenceNum = 1;
        sectionText.text = "";
    }

    private IEnumerator SetDisplaySection(string text)
    {
        int textCount = 0;

        while (text.Length > textCount)
        {
            sectionText.text += text[textCount];
            textCount++;
            yield return new WaitForSeconds(intervalForCharDisplay);
        }
        if (text.Length == textCount)
        {
            complete = !complete;
            mainUIScript.space.SetActive(complete);
        }
    }

    private void Update() {
        if (complete && Input.GetKeyDown("space"))
        {
            SetNextSentence();
        }
    }

    // 次の文章をセットする
    void SetNextSentence() {
        complete = !complete;
        mainUIScript.space.SetActive(complete);
        currentSentenceNum++;

        if (section == "p" && currentSentenceNum == 6)
        {
            bool flg = mainUIScript.DisplayChange();
            mainUIScript.space.SetActive(flg);
            openingDirector.Play();
        }
        else if (section == "e" && currentSentenceNum == 9)
        {
            sectionText.text = "";
            StartCoroutine(SetDisplaySection(GlobalConst.Story(section + currentSentenceNum.ToString())));
        }
        else if (section == "e" && currentSentenceNum == 10)
        {
            SoundManager.Instance.StopBGM();
            endRollObj.SetActive(true);
            endRollDirector.Play();
        }
        else
        {
            StartCoroutine(SetDisplaySection(GlobalConst.Story(section + currentSentenceNum.ToString())));
        }
    }
}
