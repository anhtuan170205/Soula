using UnityEngine;
using System.Collections.Generic;
using Unity.Cinemachine;

public class Targeter : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup m_cineTargetGroup;
    private Camera m_mainCamera;
    private List<Target> m_targets = new List<Target>();
    public Target CurrentTarget { get; private set; }

    private void Start()
    {
        m_mainCamera = Camera.main;
    }

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

        Target closestTarget = null;
        float closestTargetDistance = float.MaxValue;

        foreach (Target target in m_targets)
        {
            Vector2 viewPosition = m_mainCamera.WorldToViewportPoint(target.transform.position);
            
            if (viewPosition.x < 0 || viewPosition.x > 1 || viewPosition.y < 0 || viewPosition.y > 1) { continue; }

            Vector2 toCenter = viewPosition - new Vector2(0.5f, 0.5f);
            if (toCenter.sqrMagnitude < closestTargetDistance)
            {
                closestTarget = target;
                closestTargetDistance = toCenter.sqrMagnitude;
            }
        }
        if (closestTarget == null) { return false; }
        CurrentTarget = closestTarget;
        m_cineTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f);
        return true;
    }

    public void Cancel()
    {
        if (CurrentTarget == null) { return; }
        m_cineTargetGroup.RemoveMember(CurrentTarget.transform);
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
