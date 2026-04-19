using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BubblePanel : MonoBehaviour
{
    public TMP_Text text;
    public Image background;
    private Transform anchor;

    private NormalItemInteraction bubbleItem;
    public bool IsVisible => gameObject.activeSelf; //当前panel是否可见
    private void Awake()
    {
        //gameObject.SetActive(false);        
    }

    public void Setup(string content, Sprite bgSprite, Transform followAnchor, NormalItemInteraction item)
    {

        text.text = content;
        background.sprite = bgSprite;
        anchor = followAnchor;
        bubbleItem = item; // 这里赋值
        //Debug.Log("bubble panel显示");
    }

    void Update()
    {
        //Debug.Log("IsVisible: " + IsVisible + " | anchor: " + anchor); 
        if (IsVisible && anchor != null)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(anchor.position);
            transform.position = screenPos;
            //Debug.Log("BubblePanel位置: " + screenPos);
        }
    }

    private void OnDisable()
    {
        Debug.Log("BubblePanel 被关闭了！\n" + System.Environment.StackTrace);
    }
}