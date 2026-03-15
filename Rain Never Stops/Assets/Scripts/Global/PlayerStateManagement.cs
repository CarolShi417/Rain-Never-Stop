using UnityEngine;

public static class PlayerStateManagement
{
    public static PlayerState currentState = PlayerState.Alive;

    public static PlayerBehaviorState currentBehaviorState = PlayerBehaviorState.Dry;

    public static bool playerInLight = false;

    public static float humidity = 0f;
    public static void ChangeState()
    {
        PlayerState previousState = currentState;

        if (humidity <= 0f)
        {
            currentState = PlayerState.Alive;
        }
        else if (humidity >= 20f)
        {
            currentState = PlayerState.Dead;
        }
        //if(玩家进入shelter1 2 或3)
        //{
        //    currentState = PlayerState.Settled;
        //}

        if (previousState != currentState)
        {
            Debug.Log($"PlayerState changed to: {currentState}");
        }
    }

    //如果玩家在Alive状态下，有以下几个状态
    public static void ChangeBehaviorState()
    {
        PlayerBehaviorState previousBehaviorState = currentBehaviorState;

        if (humidity <= 0f)
        {
            currentBehaviorState = PlayerBehaviorState.Dry;
        }
        else if (humidity <= 5f && humidity >= 1f)
        {
            currentBehaviorState = PlayerBehaviorState.LightlyWet;
        }
        else if (humidity <= 10f && humidity > 5f)
        {
            currentBehaviorState = PlayerBehaviorState.ModeratelyWet;
        }
        else if (humidity <= 15f && humidity > 10f)
        {
            currentBehaviorState = PlayerBehaviorState.HeavilyWet;
        }
        else if (humidity < 20f && humidity > 15f)
        {
            currentBehaviorState = PlayerBehaviorState.Saturated;
        }

        if (previousBehaviorState != currentBehaviorState)
        {
            Debug.Log($"玩家动画状态 changed: {currentBehaviorState}");
        }
        //Debug.Log("StateManagement Humidity为" + humidity);
    }
}