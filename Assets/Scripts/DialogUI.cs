using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogUI : MonoBehaviour, IPointerClickHandler
{
    public static DialogUI Instance;

    [Header("UI Elements")]
    public GameObject dialogPanel;
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI dialogText;
    public Transform choicesContainer;               // drag ChoicesContainer
    public Button choiceButtonPrefab;                // drag ChoiceButtonPrefab

    [Header("Player Control")]
    public PlayerInput playerInput;                   // drag PlayerInput
    public MonoBehaviour[] scriptsToDisable;          // drag PlayerAction, PlayerMovement, dsb

    private DialogData currentDialog;
    private int currentIndex = 0;
    private List<Button> activeChoices = new List<Button>();

    private void Awake()
    {
        Instance = this;
        dialogPanel.SetActive(false);
    }

    public void StartDialog(DialogData dialog)
    {
        currentDialog = dialog;
        currentIndex = 0;

        npcNameText.text = currentDialog.npcName;

        dialogPanel.SetActive(true);

        if (playerInput != null)
            playerInput.enabled = false;

        foreach (var script in scriptsToDisable)
            script.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        ShowSentence();
    }

    private void ShowSentence()
    {
        dialogText.text = currentDialog.sentences[currentIndex];
    }

    private void NextSentence()
    {
        currentIndex++;
        if (currentIndex >= currentDialog.sentences.Length)
        {
            EndDialog();
        }
        else if (ShouldShowChoicesHere(currentIndex))
        {
            ShowChoices();
        }
        else
        {
            ShowSentence();
        }
    }

    private bool ShouldShowChoicesHere(int index)
    {
        if (currentDialog.choices == null || currentDialog.choices.Length == 0)
            return false;

        foreach (var choiceIndex in currentDialog.choiceTriggerIndexes)
        {
            if (choiceIndex == index)
                return true;
        }
        return false;
    }

    private void ShowChoices()
    {
        ClearChoices();

        foreach (var choice in currentDialog.choices)
        {
            var btn = Instantiate(choiceButtonPrefab, choicesContainer);
            btn.gameObject.SetActive(true);
            btn.GetComponentInChildren<TextMeshProUGUI>().text = choice.choiceText;

            btn.onClick.AddListener(() =>
            {
                ClearChoices();

                if (choice.nextDialogData != null)
                {
                    StartDialog(choice.nextDialogData);
                }
                else
                {
                    ShowSentence();
                }
            });

            activeChoices.Add(btn);
        }
    }

    private void ClearChoices()
    {
        foreach (var btn in activeChoices)
        {
            Destroy(btn.gameObject);
        }
        activeChoices.Clear();
    }

    private void EndDialog()
    {
        dialogPanel.SetActive(false);

        if (playerInput != null)
            playerInput.enabled = true;

        foreach (var script in scriptsToDisable)
            script.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 🌸 Cek ending
        if (currentDialog.isBadEnding)
        {
            Debug.Log("BAD END triggered!");
            // Load scene BadEnd, tampilkan panel, dsb
        }
        else if (currentDialog.isGoodEnding)
        {
            Debug.Log("GOOD END triggered!");
            // Load scene GoodEnd atau panel good end
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Hanya next kalau tidak sedang ada choices
        if (activeChoices.Count == 0)
        {
            NextSentence();
        }
    }

    private void Update()
    {
        if (dialogPanel.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
