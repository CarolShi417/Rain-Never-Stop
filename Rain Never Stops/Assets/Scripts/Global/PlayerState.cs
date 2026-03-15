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
    Dead
}

//RainZoneController
//        ↓
//PlayerStateManagement
//        ↓
//PlayerState
//        ↓
//PlayerController / Animator