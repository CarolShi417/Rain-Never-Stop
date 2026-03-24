using UnityEngine;
using UnityEngine.UI;

public class NormalItemInteraction : MonoBehaviour
{
    [Header("气泡UI")]
    [SerializeField] private BubblePanel ui;    
    [SerializeField] private Transform canvasTransform; // 拖 Canvas
    [SerializeField] private Transform anchor; // UI出现位置
    [SerializeField] private string contentText;//文字内容
    [SerializeField] private Sprite panelSprite;//panel形状

    [Header("缩放")]
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float scaleMultiplier = 1.2f;   
    private Vector3 originalScale;

    [Header("是否已交互")]
    [SerializeField] private ItemInteractionManagement itemInterManager;
    private bool hasInteracted = false;

    void Start()
    {
        originalScale = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 放大item sprite
            transform.localScale = originalScale * scaleMultiplier;
                        
            // 设置UI内容
            ui.Setup(contentText, panelSprite, anchor);
            ui.gameObject.SetActive(true);
            

            //确认此物品是否已交互
            if (hasInteracted) return; // 防止重复触发

            hasInteracted = true;

            itemInterManager.RegisterInteraction();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //ui.HideText();

            // 恢复大小
            transform.localScale = originalScale;

            ui.gameObject.SetActive(false);
            

        }
    }
    
}