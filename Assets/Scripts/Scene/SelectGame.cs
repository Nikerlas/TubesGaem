using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectGame : MonoBehaviour
{
    public GameObject[] gameButtons;
    public Button prevButton, nextButton;

    public string[] sceneNames; // 👈 Add this in the Inspector
    private int currentIndex = 0;

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            NextGame();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            PrevGame();
        }
    }

    public void NextGame()
    {
        if (currentIndex < gameButtons.Length - 1)
        {
            currentIndex++;
            UpdateUI();
        }
    }

    public void PrevGame()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < gameButtons.Length; i++)
        {
            gameButtons[i].SetActive(i == currentIndex);
        }

        prevButton.interactable = currentIndex > 0;
        nextButton.interactable = currentIndex < gameButtons.Length - 1;
    }

    public void StartSelectedGame()
    {
        if (currentIndex < sceneNames.Length)
        {
            string sceneToLoad = sceneNames[currentIndex];
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("No scene mapped for index: " + currentIndex);
        }
    }
}
