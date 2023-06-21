using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static int playerHP;
    public static bool isGameOver;
    public Image playerHPImage;
    public GameObject bloodOverlay;
    [SerializeField] private Indicators _indicators;

    void Start()
    {
        isGameOver = false;
        playerHP = 100;
    }

    void Update()
    {
        if (isGameOver)
        {
            SceneManager.LoadScene("Survival of the Other");
        }
    }


    public IEnumerator Damage(float damageAmount)
    {
        bloodOverlay.SetActive(true);
        _indicators.healthAmount -= damageAmount;
        if (_indicators.healthAmount <= 0)
            isGameOver = true;

        yield return new WaitForSeconds(1f);
        bloodOverlay.SetActive(false);
    }
}