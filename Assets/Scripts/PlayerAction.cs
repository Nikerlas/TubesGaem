using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] private TextMeshPro UseText;
    [SerializeField] private Transform Camera;
    [SerializeField] private float MaxUseDistance = 5f;
    [SerializeField] private LayerMask UseLayer;

    public void OnUse()
    {
        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseLayer))
        {
            if (hit.collider.TryGetComponent<Door>(out Door door))
            {
                if (!door.isLocked)
                {
                    if (door.isOpen)
                        door.Close();
                    else
                        door.Open(Camera.position);
                }
            }
            else if (hit.collider.TryGetComponent<Console>(out Console console))
            {
                console.Use();
            }
        }
    }

    private void Update()
    {
        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseLayer))
        {
            if (hit.collider.TryGetComponent<Door>(out Door door))
            {
                if (door.isLocked)
                {
                    UseText.SetText("Locked");
                }
                else
                {
                    UseText.SetText(door.isOpen ? "Close \"E\"" : "Open \"E\"");
                }

                UseText.gameObject.SetActive(true);
                UseText.transform.position = hit.point + (hit.point - Camera.position).normalized * 0.01f;
                UseText.transform.rotation = Quaternion.LookRotation(hit.point - Camera.position).normalized;
            }
            else if (hit.collider.TryGetComponent<Console>(out Console console))
            {
                if (console.IsQuizCompleted)
                    UseText.SetText("Quiz Complete");
                else
                    UseText.SetText("Use Console \"E\"");

                UseText.gameObject.SetActive(true);
                UseText.transform.position = hit.point + (hit.point - Camera.position).normalized * 0.01f;
                UseText.transform.rotation = Quaternion.LookRotation(hit.point - Camera.position).normalized;
            }
            else
            {
                UseText.gameObject.SetActive(false);
            }
        }
        else
        {
            UseText.gameObject.SetActive(false);
        }
    }

}
