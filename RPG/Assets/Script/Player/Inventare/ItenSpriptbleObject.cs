using UnityEngine;

public enum ItemTape { Default,Food,Weapon,Instrument, Clothing}
public enum ClothType {None,Head,Body,BodyArmor,Legs,Feet}

public class ItenSpriptbleObject : ScriptableObject
{
    public string itemName;
    public int maximumAmout;
    public GameObject itemPrefab;
    public GameObject clothingPrefab;
    public Sprite icon; 
    public ItemTape itemTape;
    public ClothType clothType = ClothType.None;
    public string itemDescription;
    public bool isConsumeable;
    public string inHeandName;

    [Header("Consumable Characteristics")]
    public float changeHealth;
    public float changeHunger;
    public float changeThirst;
}