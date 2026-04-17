using System;
using UnityEngine;

//全局脚本，用于记录触发结局的事件
public static class Events 
{
    // Ending1：湿度死亡
    public static Action OnPlayerStateDead;

    // Ending2：被怪物杀
    public static Action OnPlayerEatedByMonster;

    public static Action TriggerBadEnding3;

    public static Action TriggerHappyEnding1;
}
