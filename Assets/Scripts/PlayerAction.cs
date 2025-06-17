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
                if (door.isOpen)
                {
                    door.Close();
                }
                else
                {
                    door.Open(Camera.position);
                }
            }
        }
    }

    private void Update()
    {
        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseLayer)
        && hit.collider.TryGetComponent<Door>(out Door door))
        {
            if (door.isOpen)
            {
                UseText.SetText("Close \"E\"");
            }
            else
            {
                UseText.SetText("Open \"E\"");
            }
            UseText.gameObject.SetActive(true);
            UseText.transform.position = hit.point + (hit.point - Camera.position).normalized * 0.01f;
            UseText.transform.rotation = Quaternion.LookRotation(hit.point - Camera.position).normalized;
        }
        else
        {
            UseText.gameObject.SetActive(false);
        }
    }
}
