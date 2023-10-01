using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Items/New Item")]
public class ItemCreator : ItenSpriptbleObject
{
    private void Start()
    { 
        itemTape = ItemTape.Food;
    }
}