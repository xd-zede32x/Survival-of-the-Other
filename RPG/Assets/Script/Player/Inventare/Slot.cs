using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public ItenSpriptbleObject item;
    public int amount;
    public bool isEmpty = true;
    public GameObject iconGO;
    public TMP_Text itemAmountText;
    public ClothType clothType = ClothType.None;

    private void Awake()
    {
        iconGO = transform.GetChild(0).GetChild(0).gameObject;
        itemAmountText = transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
    }

    public void SetIcon(Sprite icon)
    {
        iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        iconGO.GetComponent<Image>().sprite = icon;
    }
}