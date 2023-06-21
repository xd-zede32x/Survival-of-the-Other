using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDropItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Slot oldSlot;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        oldSlot = transform.GetComponentInParent<Slot>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
            return;
        GetComponent<RectTransform>().position += new Vector3(eventData.delta.x, eventData.delta.y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
            return;
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.75f);
        GetComponentInChildren<Image>().raycastTarget = false;
        transform.SetParent(transform.parent.parent.parent);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
            return;
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
        GetComponentInChildren<Image>().raycastTarget = true;

        transform.SetParent(oldSlot.transform);
        transform.position = oldSlot.transform.position;
        if (eventData.pointerCurrentRaycast.gameObject.name == "UIBG") 
        {
            GameObject itemObject = Instantiate(oldSlot.item.itemPrefab, player.position + Vector3.up + player.forward, Quaternion.identity);
            itemObject.GetComponent<Item>().amount = oldSlot.amount;
            NullifySlotData();
        }
        else if (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent == null)
        {
            return;
        }
        else if (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<Slot>() != null)
        {
            ExchangeSlotData(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<Slot>());
        }

    }
    public void NullifySlotData() 
    {
        oldSlot.item = null;
        oldSlot.amount = 0;
        oldSlot.isEmpty = true;
        oldSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        oldSlot.iconGO.GetComponent<Image>().sprite = null;
        oldSlot.itemAmountText.text = "";
    }
    void ExchangeSlotData(Slot newSlot)
    {
        ItenSpriptbleObject item = newSlot.item;
        int amount = newSlot.amount;
        bool isEmpty = newSlot.isEmpty;
        GameObject iconGO = newSlot.iconGO;
        TMP_Text itemAmountText = newSlot.itemAmountText;

        newSlot.item = oldSlot.item;
        newSlot.amount = oldSlot.amount;
        if (oldSlot.isEmpty == false)
        {
            newSlot.SetIcon(oldSlot.iconGO.GetComponent<Image>().sprite);
            newSlot.itemAmountText.text = oldSlot.amount.ToString();
        }
        else
        {
            newSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            newSlot.iconGO.GetComponent<Image>().sprite = null;
            newSlot.itemAmountText.text = "";
        }

        newSlot.isEmpty = oldSlot.isEmpty;

        oldSlot.item = item;
        oldSlot.amount = amount;
        if (isEmpty == false)
        {
            oldSlot.SetIcon(iconGO.GetComponent<Image>().sprite);
            oldSlot.itemAmountText.text = amount.ToString();
        }
        else
        {
            oldSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            oldSlot.iconGO.GetComponent<Image>().sprite = null;
            oldSlot.itemAmountText.text = "";
        }

        oldSlot.isEmpty = isEmpty;
    }
}