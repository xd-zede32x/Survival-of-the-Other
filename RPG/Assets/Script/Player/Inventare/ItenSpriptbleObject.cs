using UnityEngine;

public enum ItemTape { Default,Food,Weapon,Instrument }

public class ItenSpriptbleObject : ScriptableObject
{
    public string itemName;
    public int maximumAmout;
    public GameObject itemPrefab;
    public Sprite icon; 
    public ItemTape itemTape;
    public string itemDescription;
    public bool isConsumeable;

    [Header("Consumable Characteristics")]
    public float changeHealth;
    public float changeHunger;
    public float changeThirst;
}
