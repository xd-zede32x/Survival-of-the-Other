using UnityEngine;
using UnityEngine.SceneManagement;

public class StrartMEnu : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "Survival of the Other")
        {
            SceneTransform.SwitchToScene("StartMenu");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
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