using UnityEngine;
public class PlayerDataSaveLoad : MonoBehaviour
{
    [SerializeField] private Indicators _indicators;
    [SerializeField] private CustomController _customCharacterController;
    [SerializeField] private InventoryManagerr _inventoryManager;


    public void SavePlayer()
    {
        BinarySevingSystem.SavePlayer(_indicators, _customCharacterController, _inventoryManager);
    }

    public void LoadPlayer()
    {
        PlayerData data = BinarySevingSystem.LoadPlayer();

        _indicators.healthAmount = data.health;
        _indicators.waterAmount = data.water;
        _indicators.foodAmount = data.food;

        _customCharacterController.transform.position =
            new Vector3(data.position[0], data.position[1], data.position[2]);

        for (int i = 0; i < _inventoryManager.slots.Count; i++)
        {
            if (data.itemNames[i] != null)
            {
                _inventoryManager.RemoveItemFromSlot(i);
                ItenSpriptbleObject item = Resources.Load<ItenSpriptbleObject>($"ScriptableObjects/{data.itemNames[i]}");
                int itemAmount = data.itemAmounts[i];
                _inventoryManager.AddItemSlot(item, itemAmount, i);
            }

            else
            {
                _inventoryManager.RemoveItemFromSlot(i);
            }
        }
    }
}