public class TimerInfo
{
    public float CooldownDuration { get; private set; }
    public float CooldownTimer { get; private set; }
    public bool Started { get; private set; }

    public TimerInfo(float cooldownDuration)
    {
        CooldownDuration = cooldownDuration;
        CooldownTimer = CooldownDuration;
    }

    public void UpdateTimer(float deltaTime)
    {
        Started = true;
        CooldownTimer -= deltaTime;
        if (CooldownTimer < 0)
        {
            CooldownTimer = CooldownDuration;
            Started = false;
        }
    }
}