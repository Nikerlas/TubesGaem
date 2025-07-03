using UnityEngine;

[System.Serializable]
public class Choice
{
    public string choiceText;
    public DialogData nextDialogData;     // teks pilihan

}

[CreateAssetMenu(fileName = "NewDialog", menuName = "Dialog/DialogData")]
public class DialogData : ScriptableObject
{
    public int[] choiceTriggerIndexes;
    public string npcName;
    [TextArea(2, 5)]
    public string[] sentences;
    public Choice[] choices;
    public bool isBadEnding = false;
    public bool isGoodEnding = false;
}
