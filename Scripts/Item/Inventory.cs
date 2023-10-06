using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : BaseBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject imagePrefab, itemContents, selectMenu, hand;

    List<Goods> inventory = new List<Goods>();
    private string[] itemNum = {"1", "2", "3", "4"};
    private GameObject itemContent, selectedItem, onTriggerObj;
    private int keyNum = 0;
    private string useItemName, selectedItemInfo;

    static Inventory instance;
    private void Start()
    {
        instance = this;
    }

    public static Inventory GetInstance()
    {
        return instance;
    }

    private void Update()
    {
        if (itemNum.Contains(Input.inputString))
        {
            keyNum = int.Parse(Input.inputString);
            SelectItem(keyNum);
        }
    }

    // アイテムを取得するメソッド
    public void Obtain(Obtainable item)
    {
        // 4個以上はもてないよ
        if (inventory.Count < itemNum.Count())
        {
            // アイテムの存在を確認
            if (ItemManager.GetInstance().HasItem(item.GetItemName()))
            {
                GameObject goodsObj = Instantiate(imagePrefab, content); // Imageインスタンスを作る
                Goods goods = goodsObj.GetComponent<Goods>(); // スクリプトを取得
                goods.SetUp(item); // 画像などを設定
                item.GetGameObject().SetActive(false); // アイテムを非アクティブにする
                inventory.Add(goods); // リストに入れる

                string itemInfo = ItemManager.GetInstance().GetItem(item.GetItemName()).GetInformation();
                mainUIScript.Speak(itemInfo + "を拾った。");
            }
            else
            {
                // Debug.Log("アイテム名が無効");
            }
        }
        else
        {
            mainUIScript.Speak("これ以上は持てない。");
        }
    }

    // アイテムを確認し、どうするか選択するメソッド
    public void SelectItem(int num)
    {
        if (inventory.Count >= num)
        {
            mainUIScript.SelectMenu();
            Goods goods = inventory[keyNum - 1].GetComponent<Goods>();
            mainUIScript.SetItemName(goods.GetInformation());
            selectedItem = goods.GetItemObject();
            selectedItemInfo = goods.GetInformation();
        }
        else
        {
            return;
        }
    }

    // 使用する
    public void Use()
    {
        if (OnTriggerObj != null)
        {
            Trigger trigger = OnTriggerObj.GetComponent<Trigger>();
            Goods goods = selectedItem.GetComponent<Goods>();
            UseItemName = this.gameObject.name;
            if (trigger.keyItemName == selectedItem.name)
            {
                mainUIScript.Speak(selectedItemInfo + "を使用した。");
                DelItem();
                // 使う対象によって動作変えなくては、、
                Destroy(OnTriggerObj);
            }
            else
            {
                mainUIScript.Speak("これは使えないようだ。");
            }
        }
        else
        {
            mainUIScript.Speak("使う対象がない。");
        }
    }

    // 破棄する
    public void Discard()
    {
        if (keyNum > 0)
        {
            selectedItem.transform.position = hand.transform.position;
            selectedItem.SetActive(true);
            DelItem();
        }
    }

    // 使用・破棄時共通処理
    public void DelItem()
    {
        inventory.RemoveAt(keyNum - 1);
        Destroy(itemContents.transform.GetChild(keyNum - 1).gameObject);
        Cancel();
    }

    // 画面キャンセル
    public void Cancel()
    {
        mainUIScript.SelectMenu();
    }

    public GameObject OnTriggerObj
    {
        get { return onTriggerObj; }
        set { onTriggerObj = value; }
    }

    public string UseItemName
    {
        get { return useItemName; }
        set { useItemName = value; }
    }
}
