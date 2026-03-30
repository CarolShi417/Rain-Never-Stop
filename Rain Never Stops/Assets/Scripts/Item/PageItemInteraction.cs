using UnityEngine;

public class PageItemInteraction : MonoBehaviour
{
    [Header("弹窗UI")]
    [SerializeField] private PagePanel ui;
    [SerializeField] private string contentText;
    [SerializeField] private Sprite panelSprite;

    [Header("缩放")]
    //private SpriteRenderer spriteRenderer;
    [SerializeField] private float scaleMultiplier = 1.2f;
    private Vector3 originalScale;

    [Header("是否已交互")]
    [SerializeField] private ItemInteractionManagement itemInterManager;
    private bool hasInteracted = false;
    private bool playerNearItem = false;//玩家是否在触发panel范围内

    void Start()
    {
        originalScale = transform.localScale;
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && playerNearItem)
        {
            // 获取鼠标世界坐标
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 在点击点检测碰撞
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            // 判断是否点击到这个物体或其子物体
            if (hit.collider != null && hit.collider.transform.IsChildOf(transform))
            {
                //ShowPagePanel();
            }

        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // 放大
        transform.localScale = originalScale * scaleMultiplier;

        // 只传数据（位置由 BubblePanel 内部处理）
        ui.Setup(contentText, panelSprite);
        ui.gameObject.SetActive(true);

        // 防重复触发
        if (hasInteracted) return;

        hasInteracted = true;
        itemInterManager.RegisterInteraction();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // 恢复大小
        transform.localScale = originalScale;

        ui.gameObject.SetActive(false);
    }

    
}
