using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StrartMEnu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "Survival of the Other")
        {
            SceneTransform.SwitchToScene("StartMenu");
        }

    }

    public void GoToGame()
    {
        SceneTransform.SwitchToScene("Survival of the Other");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
