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
    }
}
