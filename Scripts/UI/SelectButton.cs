using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectButton : MonoBehaviour
{
    private TextMeshProUGUI selectText;
    Color col;
    private Image check;
    private bool isCheck;
    private string scene = "Mainfloor";
    private string title = "Title";
    private string htmlValue = "#A20000";

    private void Start() {
        selectText = this.gameObject.GetComponent<TextMeshProUGUI>();
        check = transform.Find("Check").gameObject.GetComponent<Image>();
        check.enabled = isCheck;
    }

    public void Check() {
        isCheck = !isCheck;
        check.enabled = isCheck;
    }

    public void GameStart() {
        FadeManager.Instance.LoadScene (scene);
    }

    public void GameQuit() {
        Application.Quit();
    }

    private void OnEnable()
    {
        if (isCheck)
        {
            isCheck = false;
            check.enabled = isCheck;
        }
    }

    public void Title() {
        FadeManager.Instance.LoadScene(title);
    }

    private void ColorRedChange() {
        if(ColorUtility.TryParseHtmlString(htmlValue, out col))
            selectText.color = col;
    }
}
