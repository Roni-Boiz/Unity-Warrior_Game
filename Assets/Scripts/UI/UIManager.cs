using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    private void Awake()
    {
        PauseGame(false);
        gameOverScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (pauseUI.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        //Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void PauseGame(bool _status)
    {
        pauseUI.SetActive(_status);
        Time.timeScale = System.Convert.ToInt32(!_status);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("level", 1));
    }





}
