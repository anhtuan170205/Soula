using UnityEngine;

public class Targeter : MonoBehaviour
{
    private List<Targe> m_targets = new List<Targe>();

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
