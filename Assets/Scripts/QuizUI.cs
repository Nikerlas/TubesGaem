using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    // Hapus referensi ke Console, karena Console yang akan memanggilnya
    // public Console console; <-- HAPUS INI

    public TextMeshProUGUI questionText;
    public Button[] optionButtons;

    private Console currentConsole; // Simpan referensi konsol yang aktif
    private QuestionData currentQuestion;

    // Hapus fungsi Start() yang lama

    // Fungsi baru untuk menampilkan soal berdasarkan data yang diterima
    public void DisplayQuestion(QuestionData data, Console sourceConsole)
    {
        currentQuestion = data;
        currentConsole = sourceConsole;

        questionText.text = currentQuestion.questionText;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            int index = i; 
            // Isi teks tombol dari QuestionData
            optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.answers[i];
            
            // Hapus listener lama untuk menghindari duplikasi
            optionButtons[i].onClick.RemoveAllListeners();
            // Tambahkan listener baru
            optionButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
        }
    }

    private void OnAnswerSelected(int index)
    {
        if (index == currentQuestion.correctAnswerIndex)
        {
            Debug.Log("Jawaban benar!");
            // Gunakan referensi yang disimpan untuk memberitahu konsol
            if (currentConsole != null)
            {
                currentConsole.OnQuizCompleted();
            }
        }
        else
        {
            Debug.Log("Jawaban salah. Coba lagi.");
            // Di sini Anda bisa menambahkan logika untuk menampilkan tanda silang merah
            // atau langsung menutup panel. Sesuai skenario, kita tutup saja.
            if (currentConsole != null)
            {
                // Memanggil ExitQuiz dari Console agar status game kembali normal
                currentConsole.SendMessage("ExitQuiz");
            }
        }
    }
}