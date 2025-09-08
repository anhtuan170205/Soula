using UnityEngine;
using System.Collections.Generic;
using Unity.Cinemachine;

public class Targeter : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup m_targetGroup;
    private List<Target> m_targets = new List<Target>();
    public Target CurrentTarget { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) { return; }
        m_targets.Add(target);
        target.OnDestroyed += RemoveTarget;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) { return; }
        RemoveTarget(target);
    }

    public bool SelectTarget()
    {
        if (m_targets.Count == 0) { return false; }
        CurrentTarget = m_targets[0];
        m_targetGroup.AddMember(CurrentTarget.transform, 1f, 2f);
        return true;
    }

    public void Cancel()
    {
        if (CurrentTarget == null) { return; }
        m_targetGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget = null;
    }

    private void RemoveTarget(Target target)
    {
        if (CurrentTarget == target)
        {
            Cancel();
        }
        target.OnDestroyed -= RemoveTarget;
        m_targets.Remove(target);
    }
}
