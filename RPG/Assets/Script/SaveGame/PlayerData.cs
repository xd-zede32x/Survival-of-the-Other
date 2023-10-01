using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float health;
    public float food;
    public float water;

    public float[] position;

    public string[] itemNames;
    public int[] itemAmounts;


    public PlayerData(Indicators indicators, CustomController player, InventoryManagerr inventoryManager)
    {
        health = indicators.healthAmount; 
        food = indicators.foodAmount; 
        water = indicators.waterAmount;

        position = new float[3];
        var playerPosition = player.transform.position;
        position[0] = playerPosition.x;
        position[1] = playerPosition.y;
        position[2] = playerPosition.z;

        itemNames = new string[inventoryManager.slots.Count];
        itemAmounts = new int[inventoryManager.slots.Count];

        for (int index = 0; index < inventoryManager.slots.Count; index++)
        {
            if (inventoryManager.slots[index].item != null)
            {
                itemNames[index] = inventoryManager.slots[index].item.name;
            }
        }

        for (int index = 0; index < inventoryManager.slots.Count; index++)
        {
            if (inventoryManager.slots[index].item != null)
            {
                itemAmounts[index] = inventoryManager.slots[index].amount;
            }
        }
    }
}