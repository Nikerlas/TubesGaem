// Assets/Scripts/UI/QuizPanelUI.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizPanelUI : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;

    [Header("Feedback Colors")]
    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;
    public Color neutralColor = Color.white;
    public float feedbackDuration = 1.0f;

    private List<QuestionData> currentQuizQuestions;
    private int questionIndex;
    // private InteractableConsole sourceConsole;

    void Start()
    {
        gameObject.SetActive(false); // Start hidden
    }
    
    public void StartQuiz(string title, List<QuestionData> questions, InteractableConsole console)
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
        if (questionIndex < currentQuizQuestions.Count)
        {
            QuestionData q = currentQuizQuestions[questionIndex];
            questionText.text = q.questionText;
            for (int i = 0; i < answerButtons.Length; i++)
            {
                // Reset button appearance and functionality
                answerButtons[i].GetComponent<Image>().color = neutralColor;
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = q.answers[i];
                answerButtons[i].interactable = true;

                int buttonIndex = i; // Important for listener
                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() => SelectAnswer(buttonIndex));
            }
        }
        else
        {
            // All questions answered
            FinishQuiz(true);
        }
    }

    private void SelectAnswer(int selectedIndex)
    {
        // Disable all buttons immediately to prevent multiple clicks
        foreach (Button btn in answerButtons)
        {
            btn.interactable = false;
        }

        bool isCorrect = (selectedIndex == currentQuizQuestions[questionIndex].correctAnswerIndex);
        
        // Show feedback
        StartCoroutine(ShowFeedbackAndProceed(isCorrect));
    }

    private IEnumerator ShowFeedbackAndProceed(bool wasCorrect)
    {
        if (wasCorrect)
        {
            // Color only the correct button green
            int correctIndex = currentQuizQuestions[questionIndex].correctAnswerIndex;
            answerButtons[correctIndex].GetComponent<Image>().color = correctColor;
        }
        else
        {
            // Color the user's wrong choice red and the correct one green
            int correctIndex = currentQuizQuestions[questionIndex].correctAnswerIndex;
            // First find the button the user clicked (this assumes the listener context is still valid)
            // A better way would be to pass the button reference to SelectAnswer. For now, this logic will color all non-correct red.
            for (int i = 0; i < answerButtons.Length; i++)
            {
                if (i == correctIndex)
                {
                    answerButtons[i].GetComponent<Image>().color = correctColor;
                }
                else
                {
                     answerButtons[i].GetComponent<Image>().color = incorrectColor;
                }
            }
        }
        
        yield return new WaitForSeconds(feedbackDuration);

        if (wasCorrect)
        {
            questionIndex++;
            ShowQuestion(); // Move to next question
        }
        else
        {
            FinishQuiz(false); // Failed the quiz
        }
    }

    private void FinishQuiz(bool success)
    {
        if (success)
        {
            Debug.Log("Quiz completed successfully!");
            sourceConsole.MarkAsCompleted();
        }
        else
        {
            Debug.Log("Quiz failed. Closing panel.");
        }
        gameObject.SetActive(false); // Hide the panel
    }
}