using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDestroy : BaseBehaviour
{
    [SerializeField]
    string destroyTagName;

    private GameObject[] items;
    private bool enter;

    private void Start() {
        items = GameObject.FindGameObjectsWithTag(destroyTagName);
    }

    private void Update()
    {
        if (isPlayer && !enter)
        {
            AllDestroy();
            enter = true;
        }
    }

    private void AllDestroy()
    {
        if (destroyTagName == "Fire")
        {
            SoundManager.Instance.PlaySE(SESoundData.SE.Drop);
        }

        int i = 0;
        foreach (GameObject item in items)
        {
            Destroy(item);
            i++;
        }
        if (i == items.Length) Destroy(this.gameObject);
    }
}
