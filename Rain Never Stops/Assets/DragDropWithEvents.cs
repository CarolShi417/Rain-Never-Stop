using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropWithEvent : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 originalPos; // 原始位置
    public RectTransform blank; // 目标槽位
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPos = rectTransform.anchoredPosition;
        transform.SetAsLastSibling(); // 防遮挡
        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = true;

        if (eventData.pointerEnter != null &&
            eventData.pointerEnter == blank.gameObject)
        {
            SnapToBlank();
        }
        else
        {
            ReturnToOrigin();
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
}

