using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void GoToTheGame()
    {
        SceneManager.LoadScene(1);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void GoToTheExit()
    {
        Application.Quit();
    } 
}