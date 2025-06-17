using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    public Console console; // referensi ke Console.cs
    public TextMeshProUGUI questionText;
    public Button[] optionButtons;

    private int correctAnswerIndex = 2; // misal jawaban benar di index 2

    private void Start()
    {
        questionText.text = "Apa ibu kota Indonesia?";
        string[] options = { "Bandung", "Surabaya", "Jakarta", "Medan" };

        for (int i = 0; i < optionButtons.Length; i++)
        {
            int index = i; // capture index untuk listener
            optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = options[i];
            optionButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
        }
    }

    private void OnAnswerSelected(int index)
    {
        if (index == correctAnswerIndex)
        {
            Debug.Log("Jawaban benar!");
            console.OnQuizCompleted(); // Unlock pintu dari sini
        }
        else
        {
            Debug.Log("Jawaban salah. Coba lagi.");
        }
    }
}
