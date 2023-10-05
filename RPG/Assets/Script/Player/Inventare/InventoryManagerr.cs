using Cinemachine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class InventoryManagerr : MonoBehaviour
{
    public GameObject UIBG;
    public GameObject crosshair;
    public Transform inventoryPanel;
    public Transform inventoryAndClothing;
    public Transform quickSlotPanel;
    public List<Slot> slots = new List<Slot>();
    public bool isOpened;
    public float reachDistance = 3f;
    private Camera mainCamera;
    public CinemachineVirtualCamera CVC;
    [SerializeField] private Transform _player;
    [SerializeField] private List<SlotArder> _slotArders = new List<SlotArder>();

    private void Awake()
    {
        UIBG.SetActive(true);
    }

    private void Start()
    {
        mainCamera = Camera.main;
        slots.AddRange(inventoryAndClothing.GetComponentsInChildren<Slot>());
        for (int i = 0; i < quickSlotPanel.childCount; i++)
        {
            if (quickSlotPanel.GetChild(i).GetComponent<Slot>() != null)
            {
                slots.Add(quickSlotPanel.GetChild(i).GetComponent<Slot>());
            }
        }

        UIBG.SetActive(false);
        inventoryAndClothing.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isOpened = !isOpened;

            if (isOpened)
            {
                UIBG.SetActive(true);
                inventoryAndClothing.gameObject.SetActive(true);
                crosshair.SetActive(false);
                CinemachinePOV pov = CVC.GetCinemachineComponent<CinemachinePOV>();
                pov.m_HorizontalAxis.m_InputAxisName = "";
                pov.m_VerticalAxis.m_InputAxisName = "";
                pov.m_HorizontalAxis.m_InputAxisValue = 0;
                pov.m_VerticalAxis.m_InputAxisValue = 0;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            else
            {
                UIBG.SetActive(false);
                inventoryAndClothing.gameObject.SetActive(false);
                crosshair.SetActive(true);
                CVC.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisName = "Mouse X";
                CVC.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisName = "Mouse Y";

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                DragAndDropItem[] dadi = FindObjectsOfType<DragAndDropItem>();

                foreach (DragAndDropItem slot in dadi)
                {
                    slot.ReturnBackToSlot();
                }
            }
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Physics.Raycast(ray, out hit, reachDistance))
            {
                if (hit.collider.gameObject.GetComponent<Item>() != null)
                {
                    AddItem(hit.collider.gameObject.GetComponent<Item>().item, hit.collider.gameObject.GetComponent<Item>().amount);
                    Destroy(hit.collider.gameObject);
                    Debug.Log("TriggerAxe");
                }
            }
        }
    }

    public void RemoveItemFromSlot(int slotId)
    {
        Slot slot = slots[slotId];

        if (slot.clothType != ClothType.None && !slot.isEmpty)
        {
            foreach (SlotArder slotArder in _slotArders)
            {
                slotArder.RemoveCloth(slot.item.clothingPrefab);
            }
        }

        slot.item = null;
        slot.isEmpty = true;
        slot.amount = 0;
        slot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        slot.iconGO.GetComponent<Image>().sprite = null;
        slot.itemAmountText.text = "";
    }

    public void AddItemSlot(ItenSpriptbleObject _item, int _amount, int slotId)
    {
        Slot slot = slots[slotId];
        slot.item = _item;
        slot.isEmpty = false;
        slot.SetIcon(_item.icon);

        if (_amount <= _item.maximumAmout)
        {
            slot.amount = _amount;

            if (slot.item.maximumAmout != 1)
            {
                slot.itemAmountText.text = slot.amount.ToString();
            }
        }

        else
        {
            slot.amount = _item.maximumAmout;
            _amount -= _item.maximumAmout;

            if (slot.item.maximumAmout != 1)
            {
                slot.itemAmountText.text = slot.amount.ToString();
            }
        }

        if (slot.clothType != ClothType.None)
        {
            foreach (SlotArder slotArder in _slotArders)
            {
                slotArder.AddClothes(slot.item.clothingPrefab);
            }
        }
    }

    public void AddItem(ItenSpriptbleObject _item, int _amount)
    {
        int amount = _amount;

        foreach (Slot slot in slots)
        {
            if (slot.item == _item)
            {
                if (slot.amount + amount <= _item.maximumAmout)
                {
                    slot.amount += amount;
                    slot.itemAmountText.text = slot.amount.ToString();
                    return;
                }

                else
                {
                    amount -= _item.maximumAmout - slot.amount;
                    slot.amount = _item.maximumAmout;
                    slot.itemAmountText.text = slot.amount.ToString();
                }

                continue;
            }
        }

        bool allFull = true;

        foreach (Slot slot in slots)
        {
            if (slot.isEmpty)
            {
                allFull = false;
                break;
            }
        }

        if (allFull)
        {
            GameObject itemObject = Instantiate(_item.itemPrefab, _player.position + Vector3.up + _player.forward, Quaternion.identity);
            itemObject.GetComponent<Item>().amount = _amount;
        }

        foreach (Slot slot in slots)
        {
            if (amount <= 0)
            {
                return;
            }

            if (slot.isEmpty == true)
            {
                slot.item = _item;
                slot.isEmpty = false;
                slot.SetIcon(_item.icon);

                if (amount <= _item.maximumAmout)
                {
                    slot.amount = amount;

                    if (slot.item.maximumAmout != 1)
                    {
                        slot.itemAmountText.text = slot.amount.ToString();
                    }

                    break;
                }

                else
                {
                    slot.amount = _item.maximumAmout;
                    amount -= _item.maximumAmout;

                    if (slot.item.maximumAmout != 1)
                    {
                        slot.itemAmountText.text = slot.amount.ToString();
                    }
                }

                allFull = true;

                foreach (Slot slots in slots)
                {
                    if (slots.isEmpty)
                    {
                        allFull = false;
                        break;
                    }
                }

                if (allFull)
                {
                    GameObject itemObject = Instantiate(_item.itemPrefab, _player.position + Vector3.up + _player.forward, Quaternion.identity);
                    itemObject.GetComponent<Item>().amount = amount;
                    Debug.Log("Throw out");
                    return;
                }
            }
        }
    }
}