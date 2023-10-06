using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;
using Const;

public class BaseBehaviour : MonoBehaviour
{
    protected GameObject player, hands, manual;
    
    protected bool dispManual = true;
    protected bool isPlayer, isHold;
    protected string holdTag;

    protected Vector3 startPosition;
    protected Quaternion startRotation, handsRotation;

    protected Animator fade;
    // protected TextMeshProUGUI task;
    protected AudioSource audioSource;

    protected MainUI mainUIScript;
    protected RayAction rayActionScript;
    protected CallTrigger callTriggerScript;
    protected PlayerMove playerMoveScript;

    protected virtual void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        startPosition = player.transform.position;
        startRotation = player.transform.rotation;

        // hands = GameObject.FindGameObjectWithTag("Hands");
        // handsRotation = hands.transform.rotation;

        GameObject rayAction = GameObject.Find("RayEvent");
        rayActionScript = rayAction.GetComponent<RayAction>();
        audioSource = rayAction.GetComponent<AudioSource>();
        // task = GameObject.Find("Task").GetComponent<TextMeshProUGUI>();

        mainUIScript = GameObject.Find("UI").GetComponent<MainUI>();
        playerMoveScript = player.GetComponent<PlayerMove>();

        holdTag = GlobalConst.HOLD_TAG;

        GameObject callTrigger = GameObject.Find("CallTrigger");
        if (callTrigger != null)
        {
            callTriggerScript = callTrigger.GetComponent<CallTrigger>();
        }
    }

    // プレイヤーの侵入判定
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player")) {
            isPlayer = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player")) {
            isPlayer = false;
        }
    }

    // コルーチン
    protected IEnumerator DelayStop(float seconds, Action action)
    {
        var cachedWait = new WaitForSeconds(seconds);
        yield return cachedWait;
        action.Invoke();
    }

    protected bool Hold
    {
        get { return isHold; }
        set { isHold = value; }
    }

    protected void StartPlayer()
    {
        player.transform.position = startPosition;
        player.transform.rotation = startRotation;
    }
}
