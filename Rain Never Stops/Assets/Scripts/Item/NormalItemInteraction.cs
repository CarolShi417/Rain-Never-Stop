using UnityEngine;
using UnityEngine.UI;

public class NormalItemInteraction : MonoBehaviour
{
    [Header("Collider")]
    [SerializeField] private Collider2D triggerCollider; //判定玩家是否在附近
    [SerializeField] private Collider2D mouseClickCollider; //判定鼠标是否正确点击物体

    [Header("眼睛UI")]
    [SerializeField] private EyePanel eyePanel;
    [SerializeField] private Transform eyePanelAnchor; // UI出现位置
    [SerializeField] private string nameText;//文字内容                                                   

    [Header("气泡UI")]
    [SerializeField] private BubblePanel bubblePanel;    
    //[SerializeField] private Transform canvasTransform; // 拖 Canvas
    [SerializeField] private Transform bubblePanelAnchor; // UI出现位置
    [SerializeField] private string contentText;//文字内容
    [SerializeField] private Sprite panelSprite;//panel形状

    [Header("缩放")]
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float scaleMultiplier = 1.2f;
    private Vector3 originalScale;

    [Header("是否已交互")]
    [SerializeField] private ItemInteractionManagement itemInterManager;
    private bool hasInteracted = false;//是否已交互
    private bool playerNearItem = false;//玩家是否在触发bubble范围内

    void Start()
    {
        originalScale = transform.localScale;
        bubblePanel.gameObject.SetActive(false);
        eyePanel.gameObject.SetActive(false);
    }

    void Update()
    {     
        //鼠标和物体交互 BubblePanel
        if (Input.GetMouseButtonDown(0) && playerNearItem)
        {
            // 获取鼠标世界坐标
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 在点击点检测碰撞
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            // 判断是否点击到这个物体或其子物体
            if (hit.collider != null && hit.collider.transform.IsChildOf(transform))
            {
                ShowHideBubblePanel();
            }

        }

    }
    //玩家进入范围
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearItem = true;

            // 放大item sprite
            transform.localScale = originalScale * scaleMultiplier;

            eyePanel.Setup(nameText, eyePanelAnchor);
            eyePanel.gameObject.SetActive(true);
        }
    }
    //玩家离开范围
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 恢复大小
            transform.localScale = originalScale;

            playerNearItem = false;

            bubblePanel.gameObject.SetActive(false);
            eyePanel.gameObject.SetActive(false);
        }
    }
    void ShowHideBubblePanel()
    {
        bool isActive = bubblePanel.gameObject.activeSelf;

        if (isActive)
        {
            // 已显示 → 关闭
            bubblePanel.gameObject.SetActive(false);
        }
        else
        {
            // 未显示 → 打开
            bubblePanel.Setup(contentText, panelSprite, bubblePanelAnchor);
            bubblePanel.gameObject.SetActive(true);

            // 第一次点击才计数
            if (!hasInteracted)
            {
                hasInteracted = true;
                itemInterManager.RegisterInteraction();
            }
        }
    }
    
}