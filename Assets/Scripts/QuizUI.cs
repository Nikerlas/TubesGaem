using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Button[] optionButtons;
    public Button nextButton; // assign di inspector
    public Color correctColor = Color.green;
    public Color wrongColor = Color.red;
    public Color defaultColor = Color.white;
    public bool useTimerToNext = true; // true=pakai timer, false=pakai tombol next
    public float nextDelay = 1.5f; // detik

    private Console currentConsole; // console aktif
    private QuestionData currentQuestion;

    // Tampilkan soal baru
    public void DisplayQuestion(QuestionData data, Console sourceConsole)
    {
        ResetButtonColorsAndStates();
        nextButton.gameObject.SetActive(false);

        currentQuestion = data;
        currentConsole = sourceConsole;

        questionText.text = currentQuestion.questionText;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            int index = i;
            optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.answers[i];
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
        }
    }

    private void OnAnswerSelected(int index)
    {
        // Disable tombol
        foreach (var btn in optionButtons)
            btn.interactable = false;

        if (index == currentQuestion.correctAnswerIndex)
        {
            optionButtons[index].GetComponent<Image>().color = correctColor;

            if (currentConsole != null)
                currentConsole.NotifyCorrectAnswered(useTimerToNext, nextDelay);
        }
        else
        {
            optionButtons[index].GetComponent<Image>().color = wrongColor;
            optionButtons[currentQuestion.correctAnswerIndex].GetComponent<Image>().color = correctColor;

            if (currentConsole != null)
                currentConsole.NotifyWrongAnswered(useTimerToNext, nextDelay);
        }

        if (!useTimerToNext)
        {
            nextButton.gameObject.SetActive(true);
        }
    }

    private void ResetButtonColorsAndStates()
    {
        foreach (var btn in optionButtons)
        {
            btn.GetComponent<Image>().color = defaultColor;
            btn.interactable = true;
        }
    }

    // Dipanggil tombol next di inspector
    public void OnNextButtonClicked()
    {
        ResetButtonColorsAndStates();
        nextButton.gameObject.SetActive(false);

        if (currentConsole != null)
            currentConsole.OnNextButtonClicked();
    }
}
