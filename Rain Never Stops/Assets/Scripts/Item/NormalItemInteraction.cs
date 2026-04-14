using UnityEngine;
using UnityEngine.UI;

public abstract class NormalItemInteraction : MonoBehaviour
{
    [Header("Collider")]
    [SerializeField] private Collider2D triggerCollider; //判定玩家是否在附近

    [Header("眼睛UI")]
    [SerializeField] private EyePanel eyePanel;
    [SerializeField] private Transform eyePanelAnchor; // UI出现位置
    [SerializeField] private string nameText;//文字内容                                                   

    [Header("缩放")]
    [SerializeField] private float scaleMultiplier = 1.2f;
    private Transform spriteRendererTransform; //子物体transform   
    private Vector3 originalScale; //spriteRenderer原始大小
    private bool isMouseHovering = false;

    protected bool playerNearItem = false; // 子类可访问：玩家是否在触发范围内

    [Header("交互管理")]
    [SerializeField] private ItemInteractionManagement itemInterManager;
    private bool hasInteracted = false;//是否已交互
    

    protected virtual void Start()
    {
        // 找到子物体 SpriteRenderer 的 Transform，用于单独控制缩放
        spriteRendererTransform = transform.Find("SpriteRenderer");
        if (spriteRendererTransform != null)
            originalScale = spriteRendererTransform.localScale; // 记录原始大小

        eyePanel.gameObject.SetActive(false);
    }


    protected virtual void Update()
    {
        // 将鼠标屏幕坐标转换为世界坐标
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // 向鼠标位置发射射线，检测命中的碰撞体
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        // 判断命中的碰撞体是否属于本物体（包括子物体）
        bool hovering = (hit.collider != null && hit.collider.transform.IsChildOf(transform));

        if (playerNearItem)
        {
            // 鼠标刚进入悬停 → 放大子物体
            if (hovering && !isMouseHovering)
            {
                isMouseHovering = true;
                if (spriteRendererTransform != null)
                    spriteRendererTransform.localScale = originalScale * scaleMultiplier;
            }
            // 鼠标刚离开悬停 → 还原子物体大小
            else if (!hovering && isMouseHovering)
            {
                isMouseHovering = false;
                if (spriteRendererTransform != null)
                    spriteRendererTransform.localScale = originalScale;
            }

            // 鼠标左键点击且悬停在物体上 → 交给子类处理具体行为
            if (Input.GetMouseButtonDown(0) && hovering)
            {
                OnItemClicked();
            }
        }
        else if (isMouseHovering)
        {
            // 玩家离开范围时，若仍处于悬停状态则强制还原大小
            isMouseHovering = false;
            if (spriteRendererTransform != null)
                spriteRendererTransform.localScale = originalScale;
        }
    }
    //玩家进入范围
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearItem = true;

            if (eyePanel != null)
            {
                eyePanel.Setup(nameText, eyePanelAnchor);
                eyePanel.gameObject.SetActive(true);
            }

            OnPlayerEnter();
        }
    }
    //玩家离开范围
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearItem = false;

            if (eyePanel != null)
                eyePanel.gameObject.SetActive(false);



            OnPlayerExit();
        }
    }
    

    // BubblePanel 第一次点击 cancelBubble 时调用
    public void CompleteInteraction()
    {        
        if (!hasInteracted)
        {
            hasInteracted = true;
            Debug.Log("当前场景完成一个物品交互");
            itemInterManager.RegisterInteraction();
        }
    }

    // 抽象方法：子类必须实现，定义点击后的具体行为
    protected abstract void OnItemClicked();

    // 虚方法：子类可选重写，处理玩家进入范围时的额外逻辑
    protected virtual void OnPlayerEnter() { }

    // 虚方法：子类可选重写，处理玩家离开范围时的额外逻辑（如关闭面板）
    protected virtual void OnPlayerExit() { }

}