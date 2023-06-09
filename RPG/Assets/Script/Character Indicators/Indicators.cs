using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Indicators : MonoBehaviour
{
    public Image hethtBar, foodBar, waterBar;
    public float healthAmount = 100;
    public float foodAmount = 100;
    public float waterAmount = 100;

    public float secondsToEmptyFood = 300f;
    public float secondsToEmptyWater = 200f;
    public float secondsToEmtHealth = 60;

    void Start()
    {
        hethtBar.fillAmount = healthAmount / 100;
        foodBar.fillAmount = foodAmount / 100;
        waterBar.fillAmount = waterAmount / 100;
    }

    void Update()
    {
        Indecsator();
    }

    public void Indecsator()
    {
        if (foodAmount > 0)
        {
            foodAmount -= 100 / secondsToEmptyFood * Time.deltaTime;
            foodBar.fillAmount = foodAmount / 100;
        }

        if (waterAmount > 0)
        {
            waterAmount -= 100 / secondsToEmptyWater * Time.deltaTime;
            waterBar.fillAmount = waterAmount / 100;
        }

        if (foodAmount <= 0)
        {
            healthAmount -= 100 / secondsToEmtHealth * Time.deltaTime;
        }

        if (waterAmount <= 0)
        {
            healthAmount -= 100 / secondsToEmtHealth * Time.deltaTime;
        }

        hethtBar.fillAmount = healthAmount / 100;
    }
}
