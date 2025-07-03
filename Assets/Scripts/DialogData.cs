using UnityEngine;

[CreateAssetMenu(fileName = "NewDialog", menuName = "Dialog/DialogData")]
public class DialogData : ScriptableObject
{
    public string npcName;
    public Sprite portrait;
    [TextArea(2, 5)]
    public string[] sentences;
}
