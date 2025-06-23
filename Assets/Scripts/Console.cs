// File: Console.cs (Versi Final yang Sudah Disesuaikan)
using UnityEngine;
using System.Collections.Generic;

public class Console : MonoBehaviour
{
    // Enum untuk membuat pilihan dropdown di Inspector
    public enum ConsoleType { Quiz, Material }

    [Header("Console General Setup")]
    public ConsoleType type = ConsoleType.Quiz;
    public string consoleTitle;
    
    [Header("Quiz-Specific Setup")]
    [Tooltip("Pintu spesifik yang akan dibuka oleh konsol ini. Biarkan kosong jika tidak ada.")]
    public Door doorToUnlock;
    [Tooltip("Daftar semua soal untuk konsol ini.")]
    public List<QuestionData> questions;

    [Header("Material-Specific Setup")]
    [Tooltip("Data materi untuk konsol ini.")]
    public MaterialData material;

    // Properti untuk dibaca oleh PlayerAction
    public bool IsQuizCompleted { get; private set; } = false;

    // Referensi ke UI Panel
    private QuizPanelUI quizPanel;
    private MaterialPanelUI materialPanel;

    void Start()
    {
        // Cari panel UI secara otomatis saat game dimulai
        quizPanel = FindObjectOfType<QuizPanelUI>(true);
        materialPanel = FindObjectOfType<MaterialPanelUI>(true);
    }

    // Fungsi ini dipanggil oleh PlayerAction.cs saat tombol interaksi ditekan
    public void Use()
    {
        // Jika kuis sudah selesai, tidak melakukan apa-apa
        if (IsQuizCompleted && type == ConsoleType.Quiz)
        {
            Debug.Log("Quiz for " + consoleTitle + " is already complete.");
            return;
        }

        // Cek tipe konsol untuk menentukan aksi
        if (type == ConsoleType.Quiz)
        {
            if (quizPanel != null && questions.Count > 0)
            {
                // Memulai kuis dengan mengirim judul, daftar soal, dan referensi ke konsol ini sendiri
                quizPanel.StartQuiz(consoleTitle, questions, this);
            }
        }
        else if (type == ConsoleType.Material)
        {
            if (materialPanel != null && material != null)
            {
                // Menampilkan panel materi
                materialPanel.Show(material);
            }
        }
    }

    // Fungsi ini akan dipanggil oleh QuizPanelUI saat kuis berhasil diselesaikan
    public void OnQuizCompleted()
    {
        if (!IsQuizCompleted)
        {
            IsQuizCompleted = true;
            Debug.Log($"Console {consoleTitle} completed! Unlocking its door.");
            
            // Jika ada pintu yang terhubung, panggil fungsi Unlock()
            if (doorToUnlock != null)
            {
                doorToUnlock.Unlock();
            }
        }
    }
}