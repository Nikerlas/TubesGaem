// File: Assets/Scripts/Data/QuestionData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Quiz System/Question")]
public class QuestionData : ScriptableObject
{
    [TextArea(5, 10)]
    public string questionText; // Teks pertanyaan

    public string[] answers = new string[4]; // Opsi jawaban

    [Range(0, 3)]
    public int correctAnswerIndex; // Indeks jawaban benar

    [Header("Post-Answer")]
    [TextArea(3, 7)]
    public string explanation; // <-- TAMBAHAN BARU: Penjelasan untuk ditampilkan setelah menjawab
}