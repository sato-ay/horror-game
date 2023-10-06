using UnityEngine;
using Const;

public class ArmsDown : BaseBehaviour
{
    [SerializeField] GameObject targetParent;
    private bool enter;

    private void Update()
    {
        if (isPlayer && !enter)
        {
            enter = true;

            Rigidbody[] rigidbodies = targetParent.GetComponentsInChildren<Rigidbody>();
            foreach (var rb in rigidbodies)
            {
                rb.isKinematic = false;
            }
            SoundManager.Instance.PlaySE(SESoundData.SE.Stamp);

            StartCoroutine(DelayStop(2.0f, () =>
                    {
                        mainUIScript.Speak(GlobalConst.SetDialogue("Surprised3"));
                    }
                )
            );
        }
    }
}
