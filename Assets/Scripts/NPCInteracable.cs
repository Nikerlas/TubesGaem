using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    public DialogData dialogData;

    public void Use()
    {
        DialogUI.Instance.StartDialog(dialogData);
    }
}
