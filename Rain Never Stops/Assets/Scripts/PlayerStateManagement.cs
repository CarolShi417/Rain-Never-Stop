using UnityEngine;

public static class PlayerStateManager
{
    public const int MaxWetness = 20;
    public const int ShelterTargetMinWetness = 15;
    public const int ShelterTargetMaxWetness = 19;

    public static PlayerState CurrentState { get; private set; } = PlayerState.Alive;
    public static int Wetness { get; private set; } = 0;
    private static float wetnessAccumulator = 0f;

    public static bool IsDead => CurrentState == PlayerState.Dead;
    public static bool IsSettled => CurrentState == PlayerState.Settled;

    public static void ResetRun()
    {
        Wetness = 0;
        wetnessAccumulator = 0f;
        ChangeState(PlayerState.Alive);
    }

    public static void AddWetness(float amount)
    {
        if (CurrentState != PlayerState.Alive || amount <= 0f)
        {
            return;
        }

        int oldWetness = Wetness;
        wetnessAccumulator = Mathf.Clamp(wetnessAccumulator + amount, 0f, MaxWetness);
        Wetness = Mathf.FloorToInt(wetnessAccumulator);

        if (Wetness != oldWetness)
        {
            Debug.Log($"Wetness: {Wetness}/{MaxWetness}, Visual: {GetVisualState()}");
        }

        if (Wetness >= MaxWetness)
        {
            ChangeState(PlayerState.Dead);
        }
    }

    public static void ReachShelter()
    {
        if (CurrentState != PlayerState.Alive)
        {
            return;
        }

        ChangeState(PlayerState.Settled);

        bool inTargetRange = Wetness >= ShelterTargetMinWetness && Wetness <= ShelterTargetMaxWetness;
        Debug.Log(inTargetRange
            ? $"Settled in target wetness range: {Wetness}."
            : $"Settled out of target wetness range: {Wetness}.");
    }

    public static PlayerVisualState GetVisualState()
    {
        if (CurrentState == PlayerState.Dead || Wetness >= MaxWetness)
        {
            return PlayerVisualState.Dead;
        }

        if (Wetness == 0)
        {
            return PlayerVisualState.Dry;
        }

        if (Wetness <= 5)
        {
            return PlayerVisualState.LightlyWet;
        }

        if (Wetness <= 10)
        {
            return PlayerVisualState.ModeratelyWet;
        }

        if (Wetness <= 15)
        {
            return PlayerVisualState.HeavilyWet;
        }

        return PlayerVisualState.Saturated;
    }

    public static string GetVisualStateCode()
    {
        switch (GetVisualState())
        {
            case PlayerVisualState.Dry:
                return "01_dry";
            case PlayerVisualState.LightlyWet:
                return "02_lightlyWet";
            case PlayerVisualState.HeavilyWet:
                return "03_heavilyWet";
            case PlayerVisualState.ModeratelyWet:
                return "03_moderatelyWet";
            case PlayerVisualState.Saturated:
                return "04_saturated";
            default:
                return "05_dead";
        }
    }

    private static void ChangeState(PlayerState newState)
    {
        if (CurrentState == newState)
        {
            return;
        }

        CurrentState = newState;
        Debug.Log($"Player state changed to: {CurrentState}");
    }
}
