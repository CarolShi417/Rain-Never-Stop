using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDropWord : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("DrogDrop机制")]
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 originalPos; // 原始位置
    public RectTransform blank; // 目标槽位
    private CanvasGroup canvasGroup;
    [Header("是否正确")]
    public bool isCorrect = false; // 当前这个词是否放对了
    [Header("正确/错误反馈")]
    [SerializeField] private Image blankImage;      // 拖入 blank 的 Image 组件
    private Color originalColor;                    // 记录原始灰色
    private static readonly Color correctColor = new Color(0.4f, 0.8f, 0.4f, 1f); // 绿色，可自行调整
    [Header("传信号给CasePanel")]
    [SerializeField] private CasePanel casePanel; // 拖入同一个CasePanel

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();

        originalColor = blankImage.color;// 记录 blank 原始颜色
        originalPos = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isCorrect) return; //已正确，禁止拖动

        
        transform.SetAsLastSibling(); // 防遮挡

        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isCorrect) return; // 已正确，禁止拖动
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isCorrect) return; // 已正确，禁止拖动

        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = true;

        if (eventData.pointerEnter != null &&
            eventData.pointerEnter == blank.gameObject)
        {
            SnapToBlank();
            isCorrect = true;                  // 放对了
            Debug.Log("放对了");
            blankImage.color = correctColor;   // 变色
            casePanel.CheckAllCorrect();       // 通知CasePanel检查
        }
        else
        {
            ReturnToOrigin();
            isCorrect = false;                 // 没放对
        }
    }

    // 前往正确空格
    void SnapToBlank()
    {
        rectTransform.position = blank.position;
    }

    // 错误，回到原位
    void ReturnToOrigin()
    {
        rectTransform.anchoredPosition = originalPos;
    }

    public void ResetWord()
    {
        isCorrect = false;
        rectTransform.anchoredPosition = originalPos;
        blankImage.color = originalColor;
    }
}

