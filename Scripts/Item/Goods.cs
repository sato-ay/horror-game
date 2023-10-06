using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Goods : MonoBehaviour
{
    GameObject itemObject;
    string itemName, information;
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetUp(Obtainable item)
    {
        image = GetComponent<Image>(); // Imageコンポーネント

        this.itemName = item.GetItemName(); // アイテム名を取得
        this.information = ItemManager.GetInstance().GetItem(this.itemName).GetInformation(); // アイテム情報を取得

        // 画像を取得してImageコンポーネントに入れる
        image.sprite = ItemManager.GetInstance().GetItem(this.itemName).GetIcon();
        this.itemObject = item.GetGameObject(); // オブジェクトを取得
    }

	public string GetItemName() {
		return itemName;
	}

	public string GetInformation() {
		return information;
	}

	public GameObject GetItemObject() {
		return itemObject;
	}
}
