using UnityEngine;

public static class PlayerStateManager
{
    public static PlayerState currentState = PlayerState.Normal;

    public static void ChangeState(PlayerState newState)
    {
        if (currentState == newState)
            return;

        currentState = newState;

        Debug.Log("Player state changed to: " + newState);

        // 这里可以统一处理动画 / UI / 音效
    }
}