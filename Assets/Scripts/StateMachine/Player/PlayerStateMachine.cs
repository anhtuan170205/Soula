using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    private float m_duration = 5f;
    private void Start()
    {
        SwitchState(new PlayerTestState(this));
    }

    private void Update()
    {
        m_duration -= Time.deltaTime;
        Debug.Log(m_duration);
        if (m_duration <= 0f)
        {
            SwitchState(new PlayerTestState(this));
        }
    }
}
