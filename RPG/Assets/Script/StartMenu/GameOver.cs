using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void GoToTheGame()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToTheExit()
    {
        Application.Quit();
    }
}