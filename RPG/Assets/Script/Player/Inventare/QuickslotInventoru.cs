using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuickslotInventoru : MonoBehaviour
{

    public Transform quickslotParent;
    public InventoryManagerr inventoryManager;
    public int currentQuickslotID = 0;
    public Sprite selectedSprite;
    public Sprite notSelectedSprite;
    public TextMeshProUGUI healthText;
    public Slot activaSlot = null;
    public Transform allWepons;
    public Indicators indicators;

    void Update()
    {
        float mw = Input.GetAxis("Mouse ScrollWheel");

        if (mw > 0.1)
        {
            quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
            if (currentQuickslotID >= quickslotParent.childCount - 1)
            {
                currentQuickslotID = 0;
            }
            else
            {
                currentQuickslotID++;
            }
            quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
            activaSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>();
            ShowItemInHand();

        }
        if (mw < -0.1)
        {
            quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
            if (currentQuickslotID <= 0)
            {
                currentQuickslotID = quickslotParent.childCount - 1;
            }
            else
            {
                currentQuickslotID--;
            }
            quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
            activaSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>();
            ShowItemInHand();

        }
        for (int i = 0; i < quickslotParent.childCount; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                if (currentQuickslotID == i)
                {
                    if (quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite == notSelectedSprite)
                    {
                        quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
                        activaSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>();
                        ShowItemInHand();
                    }
                    else
                    {
                        quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
                        activaSlot = null;
                        HideItemInHand();
                    }
                }
                else
                {
                    quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
                    currentQuickslotID = i;
                    quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
                    activaSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>();
                    ShowItemInHand();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>().item != null)
            {
                if (quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>().item.isConsumeable && !inventoryManager.isOpened && quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite == selectedSprite)
                {
                    ChangeCharacteristics();

                    if (quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>().amount <= 1)
                    {
                        quickslotParent.GetChild(currentQuickslotID).GetComponentInChildren<DragAndDropItem>().NullifySlotData();
                    }
                    else
                    {
                        quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>().amount--;
                        quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>().itemAmountText.text = quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>().amount.ToString();
                    }
                }
            }
        }
    }
    public void CheckItemmInHand()
    {
        if (activaSlot != null)
        {
            ShowItemInHand();
        }
        else
        {
            HideItemInHand();
        }
    }

    private void ChangeCharacteristics()
    {
        indicators.ChangeFoodAmount(quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>().item.changeHunger); ;
        indicators.ChangeWaterAmount(quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>().item.changeHunger); ;
        indicators.ChangeHealthAmount(quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>().item.changeHunger); ;
    }

    private void ShowItemInHand()
    {
        HideItemInHand();
        if (activaSlot.item == null)
        {
            return;
        }

        for (int index = 0; index < allWepons.childCount; index++)
        {
            if (activaSlot.item.inHeandName == allWepons.GetChild(index).name)
            {
                allWepons.GetChild(index).gameObject.SetActive(true);
            }
        }
    }

    private void HideItemInHand()
    {
        for (int index = 0; index < allWepons.childCount; index++)
        {
            allWepons.GetChild(index).gameObject.SetActive(false);
        }
    }
}