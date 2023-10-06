using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionText : BaseBehaviour
{
    private TextMeshProUGUI actionText;
    private Animator actionAnim;
    private Image centerPoint;

    private void Start()
    {
        GameObject action = GameObject.Find("ActionText");
        actionText = action.GetComponent<TextMeshProUGUI>();
        actionAnim = action.GetComponent<Animator>();
        centerPoint = GameObject.Find("CenterPoint").GetComponent<Image>();
    }

    public void ActionDisplay(string actionName)
    {
        Color red = new Color(255.0f, 0.0f, 0.0f);
        Color white = new Color(255.0f, 255.0f, 255.0f);

        // コマンド表示
        if (actionName != null)
        {
            actionText.text = actionName;
            actionAnim.SetBool("Open", true);
            centerPoint.color = red;
        }
        else
        {
            actionAnim.SetBool("Open", false);
            centerPoint.color = white;
        }
    }
}
