using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Ending : BaseBehaviour
{
    private bool end;
    // Update is called once per frame
    void Update()
    {
        if (isPlayer && !end)
        {
            end = !end;

            mainUIScript.sectionCanvas.SetActive(true);
            if (mainUIScript.sectionCanvas.activeSelf)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                TextController.GetInstance().StartEnding();
            }
        }
    }
}
