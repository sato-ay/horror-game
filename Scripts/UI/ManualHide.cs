using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualHide : BaseBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        manual = GameObject.Find("Manual");
    }

    // Update is called once per frame
    void Update()
    {
        if (dispManual)
        {
            if (Input.GetKeyDown("return") || isPlayer)
            {
                Animator anim = manual.GetComponent<Animator>();
                anim.SetBool("Close", dispManual);
                StartCoroutine(DelayStop(0.3f, () =>
                        {
                            Destroy(manual);
                            dispManual = false;
                            Destroy(this.gameObject);
                        }
                    )
                );
            }
        }
    }
}
