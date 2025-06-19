using UnityEngine;

// Menambahkan opsi untuk membuat aset ini dari menu Create di Unity
[CreateAssetMenu(fileName = "Soal Baru", menuName = "Sistem Kuis/Data Soal")]
public class QuestionData : ScriptableObject
{
    [Header("Informasi Soal")]
    [Tooltip("Teks pertanyaan, bisa berisi potongan kode Python")]
    [TextArea(5, 10)] // Membuat field teks lebih besar di Inspector
    public string questionText;

    [Tooltip("Sediakan 4 pilihan jawaban")]
    public string[] answers = new string[4];

    [Tooltip("Indeks jawaban yang benar (0 = A, 1 = B, 2 = C, 3 = D)")]
    [Range(0, 3)] // Membatasi input antara 0 dan 3
    public int correctAnswerIndex;
}