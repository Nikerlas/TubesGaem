using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement : MonoBehaviour
{
    [Header("Scene Settings")]
    public string EscapeScene;
    public bool isEscapeForExit = false;

    [Header("Exit Popup UI")]
    public GameObject exitPopup;

    [Header("Pause Menu UI")]
    public GameObject pausePanel;
    public Button pauseButton; // 👉 Reference to your Pause button

    [Header("Finish Game UI")]
    public GameObject finishPopup;
    private bool isPaused = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (exitPopup != null)
            exitPopup.SetActive(false);

        if (pausePanel != null)
            pausePanel.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;

        // 👉 Hook up button click
        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(PauseGame);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel != null && pausePanel.activeSelf)
            {
                ResumeGame();
            }
            else if (exitPopup != null && exitPopup.activeSelf)
            {
                CancelExit();
            }
            else if (isEscapeForExit)
            {
                if (exitPopup != null)
                    ShowExitPopup();
                else
                {
                    Application.Quit();
                    Debug.Log("Game is exiting");
                }
            }
            else if (!string.IsNullOrEmpty(EscapeScene))
            {
                SceneManager.LoadScene(EscapeScene);
            }
            else
            {
                PauseGame(); // fallback
            }
        }
    }
    public void PauseGame()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
    }

    public void ResumeGame()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
    }

    public void ShowExitPopup()
    {
        if (exitPopup != null)
            exitPopup.SetActive(true);
    }

    public void ConfirmExit()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }

    public void CancelExit()
    {
        if (exitPopup != null)
            exitPopup.SetActive(false);
    }

    public void SelectGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void CreditScene()
    {
        SceneManager.LoadScene("Credit");
    }

    public void ExitGame()
    {
        if (exitPopup != null)
            ShowExitPopup();
        else
        {
            Application.Quit();
            Debug.Log("Game is exiting");
        }
    }
    // 🔥 Tambahan: Menampilkan popup saat menyelesaikan game
    public void FinishGame()
    {
        if (finishPopup != null)
        {
            finishPopup.SetActive(true);
            Time.timeScale = 0f;
        }
    }


}

