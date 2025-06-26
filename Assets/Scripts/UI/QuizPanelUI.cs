// File: Assets/Scripts/UI/QuizPanelUI.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizPanelUI : MonoBehaviour
{
    [Header("Main UI References")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;

    [Header("Explanation Panel UI")]
    public GameObject explanationPanel;
    public TextMeshProUGUI explanationText;
    public Button continueButton;

    [Header("Feedback Colors")]
    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;
    public Color neutralColor = Color.white;
    
    // --- Variabel Internal ---
    private List<QuestionData> currentQuizQuestions;
    private int questionIndex;
    private Console sourceConsole; 
    private bool lastAnswerWasCorrect;

    void Start()
    {
        gameObject.SetActive(false);
        if (explanationPanel != null)
        {
            explanationPanel.SetActive(false);
        }
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(OnContinueClicked);
        }
    }
    
    public void StartQuiz(string title, List<QuestionData> questions, Console console)
    {
        titleText.text = title;
        currentQuizQuestions = questions;
        sourceConsole = console;
        questionIndex = 0;
        
        gameObject.SetActive(true);
        ShowQuestion();
    }

    private void ShowQuestion()
    {
        if (explanationPanel != null)
        {
            explanationPanel.SetActive(false); // Sembunyikan panel penjelasan saat soal baru muncul
        }
        
        QuestionData q = currentQuizQuestions[questionIndex];
        questionText.text = q.questionText;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponent<Image>().color = neutralColor;
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = q.answers[i];
            answerButtons[i].interactable = true;

            int buttonIndex = i;
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => SelectAnswer(buttonIndex));
        }
    }

    private void SelectAnswer(int selectedIndex)
    {
        foreach (Button btn in answerButtons)
        {
            btn.interactable = false;
        }

        lastAnswerWasCorrect = (selectedIndex == currentQuizQuestions[questionIndex].correctAnswerIndex);
        
        ShowFeedbackAndExplanation(selectedIndex);
    }

    private void ShowFeedbackAndExplanation(int selectedIndex)
    {
        int correctIndex = currentQuizQuestions[questionIndex].correctAnswerIndex;
        answerButtons[correctIndex].GetComponent<Image>().color = correctColor;

        if (!lastAnswerWasCorrect)
        {
            answerButtons[selectedIndex].GetComponent<Image>().color = incorrectColor;
        }

        // Tampilkan panel penjelasan jika ada dan penjelasannya tidak kosong
        if (explanationPanel != null && !string.IsNullOrEmpty(currentQuizQuestions[questionIndex].explanation))
        {
            explanationText.text = currentQuizQuestions[questionIndex].explanation;
            explanationPanel.SetActive(true);
        }
        else
        {
            // Jika tidak ada panel penjelasan, langsung lanjutkan setelah jeda singkat
            StartCoroutine(ContinueAfterDelay());
        }
    }
    
    private IEnumerator ContinueAfterDelay()
    {
        yield return new WaitForSeconds(1.5f); // Jeda 1.5 detik
        OnContinueClicked();
    }

    // Fungsi ini dipanggil oleh continueButton atau otomatis setelah jeda
    public void OnContinueClicked()
    {
        if (explanationPanel != null)
        {
            explanationPanel.SetActive(false); // Sembunyikan panel lagi
        }

        if (lastAnswerWasCorrect)
        {
            questionIndex++;
            if (questionIndex < currentQuizQuestions.Count)
            {
                ShowQuestion(); // Lanjut ke soal berikutnya
            }
            else
            {
                FinishQuiz(true); // Semua soal terjawab dengan benar
            }
        }
        else
        {
            FinishQuiz(false); // Kuis gagal karena ada jawaban salah
        }
    }

    private void FinishQuiz(bool success)
    {
        if (success)
        {
            // Panggil OnQuizCompleted() yang ada di skrip Console.cs
            sourceConsole.OnQuizCompleted();
        }
        gameObject.SetActive(false);
    }
}