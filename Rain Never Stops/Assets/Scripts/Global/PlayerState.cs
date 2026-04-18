public enum PlayerState
{
    Alive, //活着
    Dead,  //死了
    Settled// 进入shelter
}

public enum PlayerBehaviorState
{
    Dry,
    LightlyWet,
    ModeratelyWet,
    HeavilyWet,
    Saturated,
    Dying
}

// 玩家移动锁
public static class PlayerLockState
{
    public static bool isMovementLocked = false;
}
//RainZoneController
//        ↓
//PlayerStateManagement
//        ↓
//PlayerState
//        ↓
//PlayerController / Animator