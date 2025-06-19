using UnityEngine;
using UnityEngine.InputSystem;

public class Console : MonoBehaviour
{
    [Header("Data Kuis")]
    public QuestionData questionData; // <-- TAMBAHKAN INI: Untuk menampung data soal

    [Header("Quiz & Door")]
    public GameObject quizUIPanel;
    public QuizUI quizUIScript; // <-- TAMBAHKAN INI: Referensi langsung ke skrip UI
    public Door doorToUnlock;
    public PlayerInput playerInput;

    [Header("Screen Visual")]
    public Renderer screenRenderer;
    public int screenMaterialIndex = 1;
    public Color lockedColor = Color.red;
    public Color unlockedColor = Color.green;

    private bool quizCompleted = false;
    private bool quizActive = false;

    private void Start()
    {
        SetScreenColor(lockedColor);
        if (quizUIScript == null && quizUIPanel != null)
        {
            quizUIScript = quizUIPanel.GetComponent<QuizUI>();
        }
    }

    private void Update()
    {
        if (quizActive && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            ExitQuiz();
        }
    }

    public void Use()
    {
        if (!quizCompleted)
        {
            // Tampilkan pertanyaan SEBELUM panel aktif
            if (quizUIScript != null && questionData != null)
            {
                quizUIScript.DisplayQuestion(questionData, this); // <-- MODIFIKASI: Kirim data soal ke UI
            }

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
        SetScreenColor(unlockedColor);
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
        // ... (Fungsi ini tidak perlu diubah)
        if (screenRenderer != null)
        {
            Material[] materials = screenRenderer.materials;

            if (screenMaterialIndex >= 0 && screenMaterialIndex < materials.Length)
            {
                materials[screenMaterialIndex].color = color;
            }

            screenRenderer.materials = materials;
        }
    }

    public bool IsQuizCompleted => quizCompleted;
}