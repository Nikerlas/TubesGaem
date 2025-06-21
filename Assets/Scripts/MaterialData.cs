using UnityEngine;

[CreateAssetMenu(fileName = "New Material", menuName = "Quiz System/Material")]
public class MaterialData : ScriptableObject
{
    [Header("Material Content")]
    public string title;

    [TextArea(5, 10)]
    public string explanationText;

    [Header("Code Example")]
    [TextArea(5, 10)]
    public string codeExample;

    [TextArea(3, 5)]
    public string codeOutput;
}