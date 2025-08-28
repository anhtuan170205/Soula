using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State m_currentState;

    private void Update()
    {
        m_currentState?.Tick(Time.deltaTime);
    }

    public void SwitchState(State newState)
    {
        m_currentState?.Exit();
        m_currentState = newState;
        m_currentState?.Enter();
    }
}
