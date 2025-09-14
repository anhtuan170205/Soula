using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public float ChaseRange { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; }

    public GameObject Player { get; private set; }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Agent.updatePosition = false;
        Agent.updateRotation = false;
        SwitchState(new EnemyIdleState(this));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ChaseRange);
    }
}
