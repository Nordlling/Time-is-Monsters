using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButtonHandler : MonoBehaviour
{
    [SerializeField] private string menuSceneName = "Menu";

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
