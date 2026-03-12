using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Homework_7");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}