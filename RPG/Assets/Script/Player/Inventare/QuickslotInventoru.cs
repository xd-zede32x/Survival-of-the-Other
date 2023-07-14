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
                    }
                    else
                    {
                        quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
                    }
                }
                else
                {
                    quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
                    currentQuickslotID = i;
                    quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
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

    private void ChangeCharacteristics()
    {
        if (int.Parse(healthText.text) + quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>().item.changeHealth <= 100)
        {
            float Health = int.Parse(healthText.text) + quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>().item.changeHealth;
            healthText.text = Health.ToString();
        }
        else
        {
            healthText.text = "100";
        }
    }
}