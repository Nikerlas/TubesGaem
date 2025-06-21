// Assets/Scripts/UI/MaterialPanelUI.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MaterialPanelUI : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI explanationText;
    public TextMeshProUGUI codeExampleText;
    public TextMeshProUGUI codeOutputText;
    public Button closeButton;

    void Start()
    {
        closeButton.onClick.AddListener(Close);
        gameObject.SetActive(false); // Start hidden
    }
    
    public void Show(MaterialData data)
    {
        titleText.text = data.title;
        explanationText.text = data.explanationText;
        codeExampleText.text = data.codeExample;
        codeOutputText.text = data.codeOutput;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}