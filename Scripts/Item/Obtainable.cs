using UnityEngine;
using System;

[Serializable]
public class Obtainable
{
    [SerializeField] string itemName;
    GameObject gameObject;

    internal void Obtain(GameObject item)
    {
        gameObject = item;
        Inventory.GetInstance().Obtain(this);
    }

    internal string GetItemName()
    {
        return itemName;
    }

    internal GameObject GetGameObject()
    {
        return gameObject;
    }
}
