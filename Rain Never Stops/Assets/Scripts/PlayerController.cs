using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float normalSpeed = 5f;
    [SerializeField] private float halfWetSpeed = 3f;
    [SerializeField] private float fullWetSpeed = 1f;
    [SerializeField] private float halfDrySpeed = 3f;

    private float horizontalMove = 0f;

    public Animator animator;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 获取输入
        float horizontal = Input.GetAxis("Horizontal");

        // 根据状态获取速度
        float moveSpeed = GetMoveSpeed();
        horizontalMove = Input.GetAxis("Horizontal") * moveSpeed;

        //确定walk-idle animation切换条件
        animator.SetFloat("moveSpeed", Mathf.Abs(horizontalMove));//Mathf.Abs()绝对值，因为往左/右为1正1负

        // 移动
        transform.Translate(Vector2.right * horizontalMove * Time.deltaTime);

        // 改变x轴方向
        if (horizontal != 0)
        {
            transform.localScale = new Vector3(horizontal > 0 ? 1 : -1, transform.localScale.y, transform.localScale.z);
        }

        //同步动画状态
        animator.SetInteger("State", (int)PlayerStateManager.currentState);

        float GetMoveSpeed()
        {
            switch (PlayerStateManager.currentState)
            {
                case PlayerState.Normal: return normalSpeed;
                case PlayerState.HalfWet: return halfWetSpeed;
                case PlayerState.FullWet: return fullWetSpeed;
                case PlayerState.HalfDry: return halfDrySpeed;
                case PlayerState.Broken: return 0f;
            }

            return normalSpeed;
        }
    }

    
}

