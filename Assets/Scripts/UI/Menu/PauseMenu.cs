using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool pauseGame;
    public GameObject pauseGameMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (pauseGame) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    /**
     * Menu resume
     */
    public void Resume() {
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;
        pauseGame = false;
    }

    /**
     * Menu pause
     */
    public void Pause() {
        pauseGameMenu.SetActive(true);
        Time.timeScale = 0f;
        pauseGame = true;
    }

    /**
     * Load the game
    */
    public void LoadMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    /**
     * Exit from the game
    */
    public void ExitGame() {
        Application.Quit();
    }
}
