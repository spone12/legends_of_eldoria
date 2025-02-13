using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] 
    private UIInventoryPage _inventoryUI;

    public int inventorySize = 10;

    private void Start() {
        _inventoryUI.InitializeInventoryUI(inventorySize);
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.I)) {
            if (_inventoryUI.isActiveAndEnabled == false) {
                _inventoryUI.Show();
            } else {
                _inventoryUI.Hide();
            }
        }

        if (_inventoryUI.isActiveAndEnabled) {

            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                _inventoryUI.SelectionItemSwitching("Up", inventorySize);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                _inventoryUI.SelectionItemSwitching("Down", inventorySize);
            }
        }
    }
}
