using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour
{
    //玩家移动模式，是否只能往右走
    public enum MoveMode
    {
        RightOnly,
        LeftRight
    }

    [SerializeField] private MoveMode moveMode = MoveMode.LeftRight;

    [SerializeField] private float currentSpeed;//在inspector显示当前速度，方便调试

    [SerializeField] private float drySpeed = 20f;
    [SerializeField] private float lightlyWetSpeed = 15f;
    [SerializeField] private float moderatelyWetSpeed = 10f;
    [SerializeField] private float heavilyWetSpeed = 5f;
    [SerializeField] private float saturatedSpeed = 3f;

    private float horizontalMove = 0f;

    public Animator animator;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //获取最新的behavior
        PlayerStateManagement.ChangeBehaviorState();

        HandleMovement();

        //确定walk-idle animation切换条件
        //animator.SetFloat("moveSpeed", Mathf.Abs(horizontalMove));//Mathf.Abs()绝对值，因为往左/右为1正1负


        //同步动画状态
        //animator.SetInteger("State", (int)PlayerStateManagement.currentState);
        
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
}


