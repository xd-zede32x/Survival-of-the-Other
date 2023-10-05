using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static int playerHP;
    public static bool isGameOver;
    [SerializeField] private Image _playerHPImage;
    [SerializeField] private GameObject _bloodOverlay; 
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
            SceneManager.LoadScene(2);
        }
    }

    public IEnumerator Damage(float damageAmount)
    {
        _indicators.healthAmount -= damageAmount;
        _bloodOverlay.SetActive(true);
        if (_indicators.healthAmount <= 0)
            isGameOver = true;

        yield return new WaitForSeconds(1f);
        _bloodOverlay.SetActive(false);
    }
}