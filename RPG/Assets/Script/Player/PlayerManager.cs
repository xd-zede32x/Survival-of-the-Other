using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static int PlayerHealth;
    public static bool GameOver;
    public GameObject RedOverlay;


    void Start()
    {
        PlayerHealth = 100;
        GameOver = false;
    }

    void Update()
    {
        if (GameOver)
        {
            SceneManager.LoadScene("RPG");
        }
    }

    public IEnumerator  Damage (int damegaCount)
    {
        PlayerHealth -= damegaCount;
        RedOverlay.SetActive(true);
        if (PlayerHealth <= 0)
        {
            GameOver = true;
        }

        yield return new WaitForSeconds(1.5f);
        RedOverlay.SetActive(false);
    }
}