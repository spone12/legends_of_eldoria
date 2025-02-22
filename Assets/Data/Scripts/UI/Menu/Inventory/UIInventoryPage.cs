using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.UI {
    public class UIInventoryPage : MonoBehaviour {
        [SerializeField]
        private UIInventoryItem _itemPrefab;
        [SerializeField]
        private RectTransform _contentPanel;
        [SerializeField]
        private UIInventoryDescription _itemDescription;
        [SerializeField]
        private MouseFollower _mouseFollower;

        List<UIInventoryItem> _listOfUIItems = new List<UIInventoryItem>();

        private int _currentlyDraggedItemIndex = -1;

        private int _currentItemIndex = 0;

        public event Action<int>
            OnDescriptionRequested,
            OnItemActionRequested,
            OnStartDragging;

        public event Action<int, int> OnSwapItems;

        //[SerializeField]
        //private ItemActionPanel actionPanel;

        private void Awake() {
            Hide();
            _mouseFollower.Toggle(false);
            _itemDescription.ResetDescription();
        }

        /**
         * Initialize inventory UI Items cells
         */
        public void InitializeInventoryUI(int inventorySize) {
            for (int i = 0; i < inventorySize; i++) {
                UIInventoryItem uiItem =
                    Instantiate(_itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(_contentPanel, false);
                _listOfUIItems.Add(uiItem);

                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnItemBeginDrag += HandleBeginDrag;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemEndDrag += HandleEndDrag;
                uiItem.OnRightMouseButtonClick += HandleShowItemActions;
            }
        }

        internal void ResetAllItems() {
            foreach (var item in _listOfUIItems) {
                item.ResetData();
                item.Deselect();
            }
        }

        internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description) {
            _itemDescription.SetDescription(itemImage, name, description);
            DeselectAllItems();
            _listOfUIItems[itemIndex].Select();
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity) {
            if (_listOfUIItems.Count > itemIndex) {
                _listOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
            }
        }

        /**
         * Show inventory
         */
        public void Show() {
            gameObject.SetActive(true);
            _itemDescription.ResetDescription();
            ResetSelection();
        }

        /**
         *  Hide inventory
         */
        public void Hide() {
            gameObject.SetActive(false);
            ResetDraggedItem();
        }

        /**
         * Selection item switching
         */
        public void SelectionItemSwitching(string type, int inventorySize) {

            _listOfUIItems[_currentItemIndex].Deselect();
            if (type == "Down") {
                if (++_currentItemIndex >= inventorySize) {
                    _currentItemIndex = inventorySize - 1;
                }

            } else {
                if (0 > --_currentItemIndex) {
                    _currentItemIndex = 0;
                }
            }

            _listOfUIItems[_currentItemIndex].Select();
        }

        /**
         * 
         */
        public void ResetSelection() {
            _itemDescription.ResetDescription();
            DeselectAllItems();
        }

        private void DeselectAllItems() {
            foreach (UIInventoryItem item in _listOfUIItems) {
                item.Deselect();
            }

            //actionPanel.Toggle(false);
        }

        private void CreateDraggedItem(Sprite sprite, int quantity) {
            _mouseFollower.Toggle(true);
            _mouseFollower.SetData(sprite, quantity);
        }

        /**
         * 
         */
        private void HandleItemSelection(UIInventoryItem inventoryItemUI) {

            int index = _listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;

            OnDescriptionRequested?.Invoke(index);
        }

        /**
         * 
         */
        private void HandleBeginDrag(UIInventoryItem inventoryItemUI) {

            int index = _listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;

            _currentlyDraggedItemIndex = index;
            HandleItemSelection(inventoryItemUI);
            OnStartDragging?.Invoke(index);
        }

        /**
         * 
         */
        private void HandleShowItemActions(UIInventoryItem inventoryItemUI) {
            int index = _listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1) {
                return;
            }

            OnItemActionRequested?.Invoke(index);
        }

        /**
         * 
         */
        private void HandleSwap(UIInventoryItem inventoryItemUI) {
            int index = _listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1) {
                return;
            }

            OnSwapItems?.Invoke(_currentlyDraggedItemIndex, index);
            //HandleItemSelection(inventoryItemUI);
        }

        /**
         * 
         */
        private void HandleEndDrag(UIInventoryItem inventoryItemUI) {
            ResetDraggedItem();
        }

        /**
         * 
         */
        private void ResetDraggedItem() {
            _mouseFollower.Toggle(false);
            _currentlyDraggedItemIndex = -1;
        }
    }
}
