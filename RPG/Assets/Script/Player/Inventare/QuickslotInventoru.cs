using UnityEngine;
using UnityEngine.UI;

public class QuickslotInventoru : MonoBehaviour
{
    public Transform quickslotParent;
    public InventoryManagerr inventoryManager;
    public int currentQuickslotID = 0;
    public Sprite selectedSprite;
    public Sprite notSelectedSprite;
    public Slot activaSlot = null;
    public Transform allWepons;
    public Indicators indicators;

    private void Update()
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

            SelectSlot();
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

            SelectSlot();

        }

        for (int i = 0; i < quickslotParent.childCount; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                if (currentQuickslotID == i)
                {
                    if (quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite == notSelectedSprite)
                    {
                        SelectSlot();
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
                    SelectSlot();
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
                }
            }
        }
    }

    private void SelectSlot()
    {
        quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
        activaSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>();
        ShowItemInHand();
    }

    private void RemoveConsumableItem()
    {
        if (quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>().amount <= 1)
        {
            quickslotParent.GetChild(currentQuickslotID).GetComponentInChildren<DragAndDropItem>().NullifySlotData();
        }

        else
        {
            quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>().amount--;
            quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>().itemAmountText
                .text = quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>().amount.ToString(); 
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
        Slot itemSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>();
       
        if (itemSlot.item.changeHunger == 0 && itemSlot.item.changeThirst == 0 && itemSlot.item.changeHealth == 0)
        {
            return;
        }

        indicators.ChangeFoodAmount(itemSlot.item.changeHunger);
        indicators.ChangeWaterAmount(itemSlot.item.changeThirst);
        indicators.ChangeHealthAmount(itemSlot.item.changeHealth);

        RemoveConsumableItem();
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