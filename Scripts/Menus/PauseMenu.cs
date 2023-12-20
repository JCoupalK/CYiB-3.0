using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Steamworks;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject SettingMenuUI;
    public GameObject DefaultSelectedElementSettings;
    public GameObject DefaultSelectedElementPause;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        SettingMenuUI.SetActive(false);
        Cursor.visible = false;
    }
    private void Update()
    {
        if (Time.timeScale == 0f)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }

        if (Input.GetButtonDown("Cancel"))
        {
            if (GameIsPaused)
            {
                Resume();
            }

            else
            {
                Pause();
            }
        }


        // Function to pause when unplugged (doesnt work)
        // if (Input.GetJoystickNames().Length > 0)
        // {
        //     if (!GameIsPaused)
        //     {
        //         Pause();
        //     }
        // }

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        SettingMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
        if (DefaultSelectedElementPause != null)
        {
            EventSystem.current.SetSelectedGameObject(DefaultSelectedElementPause);
        }

    }

    public void SettingLeButton()
    {
        SettingMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        if (DefaultSelectedElementSettings != null)
        {
            EventSystem.current.SetSelectedGameObject(DefaultSelectedElementSettings);
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        SteamClient.Shutdown();
        Application.Quit();
        Debug.Log("Quitting game...");
    }

}