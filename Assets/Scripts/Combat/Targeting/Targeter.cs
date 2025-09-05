using UnityEngine;
using System.Collections.Generic;

public class Targeter : MonoBehaviour
{
    private List<Target> m_targets = new List<Target>();

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) { return; }
        m_targets.Add(target);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) { return; }
        m_targets.Remove(target);
    }
}
