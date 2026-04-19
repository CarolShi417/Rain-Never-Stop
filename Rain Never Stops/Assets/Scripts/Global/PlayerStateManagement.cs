using System;
using UnityEngine;

public static class PlayerStateManagement
{
    public static PlayerState currentState = PlayerState.Alive;

    public static PlayerBehaviorState currentBehaviorState = PlayerBehaviorState.Dry;

    public static bool playerInLight = false;//玩家是否在灯光下

    public static bool isTeleTrigger = false;//是否触发接电话，触发的话player动作暂停……？

    public static float humidity = 0f;//湿度值
    
    

    public static float deadTimer = 0f;//用于判定humidity=20是否超过2s
    public static void ChangeState()
    {
        PlayerState previousState = currentState;

        if (humidity <= 0f)
        {
            currentState = PlayerState.Alive;
        }
        else if (humidity >= 20f)
        {
            deadTimer += Time.deltaTime;

            if(deadTimer >= 2f)
            {
                currentState = PlayerState.Dead;
                deadTimer = 0f;

                //触发结局1
                Events.OnPlayerStateDead?.Invoke();
            }            
        }
        //if(玩家进入shelter1 2 或3)
        //{
        //    currentState = PlayerState.Settled;
        //}

        if (previousState != currentState)
        {
            //Debug.Log($"PlayerState changed to: {currentState}");
        }
    }

    //如果玩家在Alive状态下，有以下几个状态
    public static void ChangeBehaviorState()
    {
        PlayerBehaviorState previousBehaviorState = currentBehaviorState;

        if (humidity <= 2f)
        {
            currentBehaviorState = PlayerBehaviorState.Dry;
        }
        else if (humidity <= 5f)
        {
            currentBehaviorState = PlayerBehaviorState.LightlyWet;
        }
        else if (humidity <= 10f)
        {
            currentBehaviorState = PlayerBehaviorState.ModeratelyWet;
        }
        else if (humidity <= 13f)
        {
            currentBehaviorState = PlayerBehaviorState.HeavilyWet;
        }
        else if (humidity <= 18f)
        {
            currentBehaviorState = PlayerBehaviorState.Saturated;
        }
        else if(humidity < 20f)
        {
            currentBehaviorState = PlayerBehaviorState.Dying;
        }

        if (previousBehaviorState != currentBehaviorState)
        {
            //Debug.Log($"玩家动画状态 changed: {currentBehaviorState}");
        }
        //Debug.Log("StateManagement Humidity为" + humidity);
        //Debug.Log("currentBehaviorState为" + currentBehaviorState);
    }


}