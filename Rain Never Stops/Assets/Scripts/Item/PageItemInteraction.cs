using UnityEngine;

public class PageItemInteraction : MonoBehaviour
{
    [Header("Collider")]
    [SerializeField] private Collider2D triggerCollider; //判定玩家是否在附近
    [SerializeField] private Collider2D mouseClickCollider; //判定鼠标是否正确点击物体

    [Header("眼睛UI")]
    [SerializeField] private EyePanel eyePanel;
    [SerializeField] private Transform eyePanelAnchor; // UI出现位置
    [SerializeField] private string nameText;//文字内容                      

    [Header("气泡UI")]
    [SerializeField] private PagePanel pagePanel;

    [Header("缩放")]
    //private SpriteRenderer spriteRenderer;
    [SerializeField] private float scaleMultiplier = 1.2f;
    private Vector3 originalScale;

    [Header("是否已交互")]
    [SerializeField] private ItemInteractionManagement itemInterManager;
    private bool hasInteracted = false;//是否已交互
    private bool playerNearItem = false;//玩家是否在触发panel范围内

    void Start()
    {
        originalScale = transform.localScale;
        pagePanel.gameObject.SetActive(false);
        eyePanel.gameObject.SetActive(false);
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
                ShowPagePanel();
            }

        }

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("进入");
            playerNearItem = true;

            // 放大item sprite
            transform.localScale = originalScale * scaleMultiplier;

            eyePanel.Setup(nameText, eyePanelAnchor);
            eyePanel.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 恢复大小
            transform.localScale = originalScale;

            playerNearItem = false;

            eyePanel.gameObject.SetActive(false);
        }
    }

    void ShowPagePanel()
    {
        pagePanel.gameObject.SetActive(true);
    }

    //如果按下取消按钮，那么该道具判定为已交互
    public void onCancelButton()
    {
        if (!hasInteracted)
        {
            hasInteracted = true;
            itemInterManager.RegisterInteraction();
        }
    }
}
