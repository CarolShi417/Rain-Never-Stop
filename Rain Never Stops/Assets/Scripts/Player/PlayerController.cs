using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    //玩家移动模式，是否只能往右走
    //public enum MoveMode
    //{
    //    RightOnly,
    //    LeftRight
    //}
    //[Header("moveMode")]
    //[SerializeField] private MoveMode moveMode = MoveMode.LeftRight;

    [Header("移动范围")]
    public float minX = -10f;
    public float maxX = 10f;
    [Header("移动速度")]
    [SerializeField] private float currentSpeed;//在inspector显示当前速度，方便调试

    [SerializeField] private float drySpeed = 20f;
    [SerializeField] private float lightlyWetSpeed = 15f;
    [SerializeField] private float moderatelyWetSpeed = 10f;
    [SerializeField] private float heavilyWetSpeed = 6f;
    [SerializeField] private float saturatedSpeed = 4f;
    [SerializeField] private float dyingSpeed = 2f;

    [Header("当前移动向量")]
    private float horizontalMove = 0f;
    [Header("交互锁，控制玩家移动/静止")]
    public static bool isMovementLocked = false;

    [Header("animation")]
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // 获取Animator
        //进入新场景获取一次Bebavior
        PlayerStateManagement.ChangeBehaviorState();

        ShowAnimationFromLastScene();

        //监听PlayerStateManagement 何时触发纸人死亡
        Events.OnPlayerStateDead += OnPlayerDead;
    }
     
    void Update()
    {
        //如果接到电话，停止一切行动
        if (PlayerStateManagement.isTeleTrigger) return;

        //获取最新的behavior
        PlayerStateManagement.ChangeBehaviorState();



        //解锁，可以移动和更新动画
        if (!PlayerLockState.isMovementLocked)
        {
            //处理玩家移动
            HandleMovement();
            animator.SetBool("isLocked", false); // ✅ 解锁时设false
            //处理动画
            UpdateAnimation();
        }
        else
        {
            animator.SetBool("isLocked", true);  // ✅ 锁定时设true
            animator.SetFloat("moveSpeed", 0f);
            animator.SetInteger("State", (int)PlayerStateManagement.currentBehaviorState + 1);
        }

    }

    void HandleMovement()
    {
        // 获取输入
        float horizontal = Input.GetAxis("Horizontal");


        // 根据状态获取速度
        currentSpeed = GetMoveSpeed();
        horizontalMove = horizontal * currentSpeed;

        // 限制X轴范围
        float newX = transform.position.x + horizontalMove * Time.deltaTime;
        newX = Mathf.Clamp(newX, minX, maxX);

        // 应用新位置
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        // 移动
        //transform.Translate(Vector2.right * horizontalMove * Time.deltaTime);

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
                case PlayerBehaviorState.Dying: return dyingSpeed;

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

    void ShowAnimationFromLastScene()
    {
        int state = (int)PlayerStateManagement.currentBehaviorState;
        switch (PlayerStateManagement.currentBehaviorState)
        {
            case PlayerBehaviorState.Dry:
                animator.Play("player_01_dry_idle");
                break;
            case PlayerBehaviorState.LightlyWet:
                animator.Play("player_02_lightlyWet_idle");
                break;
            case PlayerBehaviorState.ModeratelyWet:
                animator.Play("player_03_moderatelyWet_idle");
                break;
            case PlayerBehaviorState.HeavilyWet:
                animator.Play("player_04_heavilyWet_idle");
                break;
            case PlayerBehaviorState.Saturated:
                animator.Play("player_05_saturated_idle");
                break;
            case PlayerBehaviorState.Dying:
                animator.Play("player_06_dying_idle");
                break;
        }

    }

    private void OnDisable()
    {
        // 物体销毁或禁用时取消订阅，防止内存泄漏
        Events.OnPlayerStateDead -= OnPlayerDead;
    }

    // 收到死亡事件时执行
    private void OnPlayerDead()
    {
        PlayerLockState.isMovementLocked = true; // 锁定玩家移动
        animator.SetBool("isDead", true);        // 触发Animator死亡动画transition
        StartCoroutine(WaitAndSwitch());         // 开始等待协程
    }

    //死亡动画协程
    IEnumerator WaitAndSwitch()
    {
        yield return null; // 等一帧，确保Animator已切换到死亡动画状态

        // 获取当前死亡动画的时长
        float animLength = animator.GetCurrentAnimatorStateInfo(0).length;

        // 等待动画播放完毕
        yield return new WaitForSeconds(animLength);

        // 再额外等待2秒
        yield return new WaitForSeconds(2f);

        // 跳转到结局场景
        FindObjectOfType<SceneManagement>()?.GoToEnding1();
    }
}


