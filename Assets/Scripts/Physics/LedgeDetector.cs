using System;
using UnityEngine;

public class LedgeDetector : MonoBehaviour
{
    public event Action<Vector3> OnLedgeDetected;

    private void OnTriggerEnter(Collider other)
    {
        OnLedgeDetected?.Invoke(other.transform.forward);
    }
}
