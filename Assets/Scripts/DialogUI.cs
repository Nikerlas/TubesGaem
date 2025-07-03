using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DialogUI : MonoBehaviour, IPointerClickHandler
{
    public static DialogUI Instance;

    [Header("UI Elements")]
    public GameObject dialogPanel;
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI dialogText;

    [Header("Player Control")]
    public PlayerInput playerInput;               // Drag PlayerInput di inspector
    public MonoBehaviour[] scriptsToDisable;      // Drag script: PlayerAction, PlayerMovement, PlayerLook, dsb

    private DialogData currentDialog;
    private int currentIndex = 0;

    private void Awake()
    {
        Instance = this;
        dialogPanel.SetActive(false);
    }

    private void Update()
{
    if (dialogPanel.activeSelf)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}


    public void StartDialog(DialogData dialog)
    {
        currentDialog = dialog;
        currentIndex = 0;

        npcNameText.text = currentDialog.npcName;

        dialogPanel.SetActive(true);

        // 🌸 Disable player input & other scripts
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
        else
        {
            ShowSentence();
        }
    }

    private void EndDialog()
    {
        dialogPanel.SetActive(false);

        // 🌸 Enable player input & other scripts again
        if (playerInput != null)
            playerInput.enabled = true;

        foreach (var script in scriptsToDisable)
            script.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // 🌸 Detect klik di panel untuk next kalimat
    public void OnPointerClick(PointerEventData eventData)
    {
        NextSentence();
    }
}
