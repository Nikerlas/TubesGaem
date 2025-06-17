using UnityEngine;
using UnityEngine.InputSystem;

public class Console : MonoBehaviour
{
    [Header("Quiz & Door")]
    public GameObject quizUIPanel;
    public Door doorToUnlock;
    public PlayerInput playerInput;

    [Header("Screen Visual")]
    public Renderer screenRenderer;
    public int screenMaterialIndex = 1; // Isi berdasarkan urutan material layar
    public Color lockedColor = Color.red;
    public Color unlockedColor = Color.green;

    private bool quizCompleted = false;
    private bool quizActive = false;

    private void Start()
    {
        SetScreenColor(lockedColor); // warna awal = merah (locked)
    }

    private void Update()
    {
        // Keluar quiz saat tekan ESC
        if (quizActive && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            ExitQuiz();
        }
    }

    public void Use()
    {
        if (!quizCompleted)
        {
            quizUIPanel.SetActive(true);
            Time.timeScale = 0;

            if (playerInput != null)
                playerInput.enabled = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            quizActive = true;
        }
    }

    public void OnQuizCompleted()
    {
        quizCompleted = true;
        doorToUnlock.Unlock();
        ExitQuiz();
        SetScreenColor(unlockedColor); // warna hijau = unlocked
    }

    private void ExitQuiz()
    {
        quizUIPanel.SetActive(false);
        Time.timeScale = 1;

        if (playerInput != null)
            playerInput.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        quizActive = false;
    }

    private void SetScreenColor(Color color)
    {
        if (screenRenderer != null)
        {
            Material[] materials = screenRenderer.materials;

            if (screenMaterialIndex >= 0 && screenMaterialIndex < materials.Length)
            {
                materials[screenMaterialIndex].color = color;
            }

            screenRenderer.materials = materials; // apply changes
        }
    }

    public void DebugCompleteQuiz()
    {
        Debug.Log("Quiz selesai (DEBUG MANUAL)");
        OnQuizCompleted();
    }

    public bool IsQuizCompleted => quizCompleted;
}


