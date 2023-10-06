using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu, subMenu;
    [SerializeField] Animator armAnim, clipAnim;
    [SerializeField] Light clipLight;
    static SettingMenu instance;

    private void Start() {
        instance = this;
        this.gameObject.GetComponent<Canvas>().enabled = false;
        clipLight.enabled = false;
    }

    public static SettingMenu GetInstance()
    {
        return instance;
    }

    public void Main()
    {
        mainMenu.SetActive(true);
        subMenu.SetActive(false);
    }

    public void Sub()
    {
        mainMenu.SetActive(false);
        subMenu.SetActive(true);
    }

    public void Open()
    {
        armAnim.SetBool("Menu", true);
        ClipAnim(1f);

        StartCoroutine(DelayStop(1.0f, () =>
                {
                    MainUI.GetInstance().SettingMenu();
                    Main();
                }
            )
        );
    }

    public void Close()
    {
        armAnim.SetBool("Menu", false);
        ClipAnim(-1.2f);

        MainUI.GetInstance().SettingMenu();
    }

    public bool GetMenuBool()
    {
        bool b = clipAnim.GetBool("MenuOpen");
        return b;
    }

    private void ClipAnim(float speed)
    {
        bool setBool = speed > 0f ? true : false;
        clipAnim.SetFloat(Animator.StringToHash("Speed"), speed);
        clipAnim.SetBool("MenuOpen", setBool);
        StartCoroutine(DelayStop(speed, () =>
                {
                    clipLight.enabled = setBool;
                }
            )
        );
    }

    protected IEnumerator DelayStop(float seconds, Action action)
    {
        var cachedWait = new WaitForSeconds(seconds);
        yield return cachedWait;
        action.Invoke();
    }
}
