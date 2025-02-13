using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour, 
    IPointerClickHandler, 
    IBeginDragHandler,
    IEndDragHandler,
    IDropHandler,
    IDragHandler
{
    [SerializeField] 
    private Image _itemImage;
    [SerializeField] 
    private TMP_Text _stackQuantity;
    [SerializeField] 
    private Image _borderImage;

    public event Action<UIInventoryItem> 
        OnItemClicked,
        OnItemDroppedOn, 
        OnItemBeginDrag, 
        OnItemEndDrag,
        OnRightMouseButtonClick;

    private bool _empty = true;

    private void Awake() {
        ResetData();
        Deselect();
    }

    /**
     * Reset item data
     */
    public void ResetData() {
       this._itemImage.gameObject.SetActive(false);
       _empty = true;
    }

    /**
     * Set item data
     */
    public void SetData(Sprite sprite, int quantity) {
        this._itemImage.gameObject.SetActive(true);
        this._itemImage.sprite = sprite;
        this._stackQuantity.text = quantity + "";
        _empty = false;
    }

    /**
     * Select item
     */
    public void Select() {
        _borderImage.enabled = true;
    }

    /**
     * Deselect item
     */
    public void Deselect() {
        _borderImage.enabled = false;
    }

    /**
     * Event: on right or left click item
     */
    public void OnPointerClick(PointerEventData pointerData) {

        if (pointerData.button == PointerEventData.InputButton.Right) {
            OnRightMouseButtonClick?.Invoke(this);
        } else {
            OnItemClicked?.Invoke(this);
        }
    }

    /**
     * Event: On begin item drag
     */
    public void OnBeginDrag(PointerEventData eventData) {
        if (_empty) return;
        OnItemBeginDrag?.Invoke(this);
    }

    /**
     *  Event: On item drag end
     */
    public void OnEndDrag(PointerEventData eventData) {
        OnItemEndDrag?.Invoke(this);
    }

    /**
     *  Event: On drop item
     */
    public void OnDrop(PointerEventData eventData) {
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData) {}
}
