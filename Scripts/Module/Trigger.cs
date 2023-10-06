using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField]
    public string keyItemName;
    Inventory inventory;

    private void Start() {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    private void OnTriggerEnter(Collider other) {
        inventory.OnTriggerObj = this.gameObject;
    }

    private void OnTriggerExit(Collider other) {
        inventory.OnTriggerObj = null;
    }
}
