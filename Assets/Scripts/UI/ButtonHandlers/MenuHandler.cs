using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject scoreboardPanel;
    [SerializeField] private GameObject creditsPanel;

    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void MainMenu()
    {
        mainMenuPanel.SetActive(true);
        scoreboardPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void Scoreboard()
    {
        scoreboardPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void Credits()
    {
        creditsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        scoreboardPanel.SetActive(false);
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
