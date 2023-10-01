using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Indicators : MonoBehaviour
{
    public Image healthtBar, foodBar, waterBar;
    private Camera _mainCamera;

    public float healthAmount = 100;
    public float uiHealthAmount = 100;

    public float foodAmount = 100;
    private float uiFoodAmount = 100;

    public float waterAmount = 100;
    private float uiWaterAmount = 100;

    public float secondsToEmptyFood = 300f;
    public float secondsToEmptyWater = 200f;
    public float secondsToEmtHealth = 60;

    private float changeFactor = 6;
    public bool isInWater = false;

    void Start()
    {
        _mainCamera = Camera.main;
        healthtBar.fillAmount = healthAmount / 100;
        foodBar.fillAmount = foodAmount / 100;
        waterBar.fillAmount = waterAmount / 100;
    }

    void Update()
    {
        Indecsator();
    }

    public void Indecsator()
    {
        if (isInWater)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                ChangeWaterAmount(20);
            }
        }

        if (foodAmount > 0)
        {
            foodAmount -= 100 / secondsToEmptyFood * Time.deltaTime;
            uiFoodAmount = Mathf.Lerp(uiFoodAmount, foodAmount, Time.deltaTime * changeFactor);
            foodBar.fillAmount = uiFoodAmount / 100;
        }

        else
        {
            uiFoodAmount = 0;
            foodBar.fillAmount = uiFoodAmount / 100;
        }

        if (waterAmount > 0)
        {
            waterAmount -= 100 / secondsToEmptyWater * Time.deltaTime;
            uiWaterAmount = Mathf.Lerp(uiWaterAmount, waterAmount, Time.deltaTime * changeFactor);
            waterBar.fillAmount = uiWaterAmount / 100;
        }

        else
        {
            uiWaterAmount = 0;
            waterBar.fillAmount = uiWaterAmount / 100;
        }

        if (foodAmount <= 0)
        {
            healthAmount -= 100 / secondsToEmtHealth * Time.deltaTime;
        }

        if (waterAmount <= 0)
        {
            healthAmount -= 100 / secondsToEmtHealth * Time.deltaTime;
        }
        uiHealthAmount = Mathf.Lerp(uiHealthAmount, healthAmount, Time.deltaTime * changeFactor);
        healthtBar.fillAmount = uiHealthAmount / 100;
    }

    public void ChangeFoodAmount(float changeValue)
    {
        if (foodAmount + changeValue > 100)
        {
            foodAmount = 100;
        }

        else
        {
            foodAmount += changeValue;
        }
    }

    public void ChangeWaterAmount(float changeValue)
    {
        if (waterAmount + changeValue > 100)
        {
            waterAmount = 100;
        }
        else
        {
            waterAmount += changeValue;
        }
    }

    public void ChangeHealthAmount(float changeValue)
    {
        if (healthAmount + changeValue > 100)
        {
            healthAmount = 100;
        }

        else
        {
            healthAmount += changeValue;
        }
    }
}