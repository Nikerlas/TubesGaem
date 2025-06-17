using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    public bool isLocked = true;
    [SerializeField] private bool isRotationDoor = true;
    [SerializeField] private float speed = 1f;

    [Header("Rotation Config")]
    [SerializeField] private float RotationAmount = 90f;
    [SerializeField] private float ForwardDirection = 0;

    private Vector3 StartRotation;
    private Vector3 Forward;

    private Coroutine AnimationCoroutine;

    private void Start()
    {
        StartRotation = transform.rotation.eulerAngles;
        Forward = transform.right;
    }

    public void Open(Vector3 userPosition)
    {
        if (isLocked || isOpen)
        {
            return; // Do not open if locked or already open
        }

        if (AnimationCoroutine != null)
        {
            StopCoroutine(AnimationCoroutine);
        }

        if (isRotationDoor)
        {
            float dot = Vector3.Dot(Forward, (userPosition - transform.position).normalized);
            Debug.Log($"Dot: {dot.ToString("N3")}");
            AnimationCoroutine = StartCoroutine(DoRotationOpen(dot));
        }
    }

    private IEnumerator DoRotationOpen(float ForwardAmount)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        if (ForwardAmount >= ForwardDirection)
        {
            endRotation = startRotation * Quaternion.Euler(0, -RotationAmount, 0);
        }
        else
        {
            endRotation = startRotation * Quaternion.Euler(0, RotationAmount, 0);
        }

        isOpen = true;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }

    public void Close()
    {
        if (isOpen)
        {
            StopCoroutine(AnimationCoroutine);
        }

        if (isRotationDoor)
        {
            AnimationCoroutine = StartCoroutine(DoRotationClose());
        }
    }

    private IEnumerator DoRotationClose()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(StartRotation);

        isOpen = false;
        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }

    public void Unlock()
    {
        isLocked = false;
        Debug.Log("Door Unlocked");
    }
}
