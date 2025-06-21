// Assets/Scripts/Data/QuestionData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Quiz System/Question")]
public class QuestionData : ScriptableObject
{
    [Header("Question Info")]
    [Tooltip("The question text. Can contain code snippets.")]
    [TextArea(5, 10)]
    public string questionText;

    [Tooltip("Provide 4 answer options.")]
    public string[] answers = new string[4];

    [Tooltip("The index of the correct answer (0-3).")]
    [Range(0, 3)]
    public int correctAnswerIndex;
}