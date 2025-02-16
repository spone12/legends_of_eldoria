using System;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] 
    private UIInventoryPage _inventoryUI;

    [SerializeField]
    private InventorySO _inventoryData;

    private void Start() {
        PrepareUI();
        //_inventoryData.Initialize();
    }

    private void PrepareUI() {
        _inventoryUI.InitializeInventoryUI(_inventoryData.Size);
        _inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
        _inventoryUI.OnSwapItems += HandleSwapItems;
        _inventoryUI.OnStartDragging += HandleDragging;
        _inventoryUI.OnItemActionRequested += HandleItemActionRequest;
    }

    private void HandleItemActionRequest(int itemIndex) {
        InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;

       /* IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null) {

            inventoryUI.ShowItemAction(itemIndex);
            inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
        }

        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null) {
            _inventoryData.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.quantity));
        }*/

    }

    private void HandleDragging(int itemIndex) {
        InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;
       // _inventoryData.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
    }

    private void HandleSwapItems(int itemIndex_1, int itemIndex_2) {
        //_inventoryData.SwapItems(itemIndex_1, itemIndex_2);
    }

    private void HandleDescriptionRequest(int itemIndex) {
        InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty) {
            _inventoryUI.ResetSelection();
            return;
        }
        ItemSO item = inventoryItem.item;
        //string description = PrepareDescription(inventoryItem);
        _inventoryUI.UpdateDescription(
            itemIndex, 
            item.ItemImage,
            item.name, 
            item.Description
        );
    }

    public void Update() {

        if (Input.GetKeyDown(KeyCode.I)) {
            if (_inventoryUI.isActiveAndEnabled == false) {

                _inventoryUI.Show();
                foreach (var item in _inventoryData.GetCurrentInventoryState()) {
                    _inventoryUI.UpdateData(
                        item.Key,
                        item.Value.item.ItemImage,
                        item.Value.quantity
                    );
                }
            } else {
                _inventoryUI.Hide();
            }
        }

        if (_inventoryUI.isActiveAndEnabled) {

            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                _inventoryUI.SelectionItemSwitching("Up", _inventoryData.Size);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                _inventoryUI.SelectionItemSwitching("Down", _inventoryData.Size);
            }
        }
    }
}
