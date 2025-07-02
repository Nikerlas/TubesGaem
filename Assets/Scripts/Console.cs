using UnityEngine;
using UnityEngine.InputSystem;

public class Console : MonoBehaviour
{
    [Header("Quiz & Door")]
    public GameObject quizUIPanel;
    public Door doorToUnlock;
    public PlayerInput playerInput;
    public QuizUI quizUI; // assign di inspector
    public QuestionData[] questions;

    [Header("Screen Visual")]
    public Renderer screenRenderer;
    public int screenMaterialIndex = 1;
    public Color lockedColor = Color.red;
    public Color unlockedColor = Color.green;

    private bool quizCompleted = false;
    private bool quizActive = false;
    private int currentQuestionIndex = 0;

    private void Start()
    {
        SetScreenColor(lockedColor);
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
            currentQuestionIndex = 0;
            ShowCurrentQuestion();

            quizUIPanel.SetActive(true);
            Time.timeScale = 0;

            if (playerInput != null)
                playerInput.enabled = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            quizActive = true;
        }
    }

    private void ShowCurrentQuestion()
    {
        if (currentQuestionIndex < questions.Length)
        {
            quizUI.DisplayQuestion(questions[currentQuestionIndex], this);
        }
        else
        {
            OnQuizCompleted();
        }
    }
    private System.Collections.IEnumerator DelayNextQuestion(float delay)
{
    yield return new WaitForSecondsRealtime(delay);
    CheckOrShowNextQuestion();
}


    // Dipanggil QuizUI saat jawab benar
    public void NotifyCorrectAnswered(bool autoNext, float delay)
    {
        currentQuestionIndex++;
        if (autoNext)
        {
            StartCoroutine(DelayNextQuestion(delay));
        }
    }

public void NotifyWrongAnswered(bool autoNext, float delay)
{
    currentQuestionIndex++;
    if (autoNext)
    {
        StartCoroutine(DelayNextQuestion(delay));
    }
}


    // Dipanggil tombol next
    public void OnNextButtonClicked()
    {
        CheckOrShowNextQuestion();
    }

    private void CheckOrShowNextQuestion()
    {
        if (currentQuestionIndex >= questions.Length)
        {
            OnQuizCompleted();
        }
        else
        {
            ShowCurrentQuestion();
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
