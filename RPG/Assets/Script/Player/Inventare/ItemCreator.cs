using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Items/New Item")]
public class ItemCreator : ItenSpriptbleObject
{
    public float healAmount;

    private void Start()
    {
        itemTape = ItemTape.Food;
    }
}