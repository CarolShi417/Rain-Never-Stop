using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //玩家移动模式，是否只能往右走
    public enum MoveMode
    {
        RightOnly,
        LeftRight
    }
    [Header("moveMode")]
    [SerializeField] private MoveMode moveMode = MoveMode.LeftRight;

    [Header("speed")]
    [SerializeField] private float currentSpeed;//在inspector显示当前速度，方便调试
    
    [SerializeField] private float drySpeed = 20f;
    [SerializeField] private float lightlyWetSpeed = 15f;
    [SerializeField] private float moderatelyWetSpeed = 10f;
    [SerializeField] private float heavilyWetSpeed = 5f;
    [SerializeField] private float saturatedSpeed = 3f;

    [Header("当前移动向量")]
    private float horizontalMove = 0f;

    [Header("animation")]
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // 获取Animator
    }

    // Update is called once per frame
    void Update()
    {
        //如果接到电话，停止一切行动
        if (PlayerStateManagement.isTeleTrigger) return;

        //获取最新的behavior
        PlayerStateManagement.ChangeBehaviorState();

        //处理移动
        HandleMovement();

        //处理动画状态
        UpdateAnimation();
        
    }

    void HandleMovement()
    {
        // 获取输入
        float horizontal = Input.GetAxis("Horizontal");

        //进入rain scene，只能往右走
        if (moveMode == MoveMode.RightOnly)
        {
            horizontal = Mathf.Max(0f, horizontal);
        }


        // 根据状态获取速度
        currentSpeed = GetMoveSpeed();
        horizontalMove = horizontal * currentSpeed;

        // 移动
        transform.Translate(Vector2.right * horizontalMove * Time.deltaTime);
        //Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //rb.MovePosition(rb.position + new Vector2(horizontalMove * Time.deltaTime, 0));

        // 改变x轴方向
        if (horizontal != 0)
        {
            transform.localScale = new Vector3(horizontal > 0 ? 1 : -1, transform.localScale.y, transform.localScale.z);
        }

        // 玩家速度随humidity变化
        float GetMoveSpeed()
        {
            switch (PlayerStateManagement.currentBehaviorState)
            {
                case PlayerBehaviorState.Dry: return drySpeed;
                case PlayerBehaviorState.LightlyWet: return lightlyWetSpeed;
                case PlayerBehaviorState.ModeratelyWet: return moderatelyWetSpeed;
                case PlayerBehaviorState.HeavilyWet: return heavilyWetSpeed;
                case PlayerBehaviorState.Saturated: return saturatedSpeed;

                default:
                    Debug.LogError("没有匹配的PlayerBehaviorState");
                    return drySpeed;
            }
        }
    }

    void UpdateAnimation()
    {
        // Speed：控制 Idle / Walk 切换（Blend Tree用）
        animator.SetFloat("moveSpeed", Mathf.Abs(horizontalMove));

        // State：控制不同湿度状态动画
        animator.SetInteger("State", (int)PlayerStateManagement.currentBehaviorState + 1);

    }
}


