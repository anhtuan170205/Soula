using UnityEngine;
using System.Collections.Generic;

public class Targeter : MonoBehaviour
{
    public List<Target> m_targets = new List<Target>();
    public Target CurrentTarget { get; private set; }

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

    public bool SelectTarget()
    {
        if (m_targets.Count == 0) { return false; }
        CurrentTarget = m_targets[0];
        return true;
    }

    public void Cancel()
    {
        CurrentTarget = null;
    }
}
