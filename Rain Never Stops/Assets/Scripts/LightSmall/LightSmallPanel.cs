using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LightSmallPanel : MonoBehaviour
{
    public static LightSmallPanel Instance;//将panel设为全局入口

    public TMP_Text text;
    public Image background;
    private Transform anchor;
    public bool IsVisible => gameObject.activeSelf; //当前panel是否可见

    private void Awake()
    {
        Instance = this; //注册
        gameObject.SetActive(false);
    }
    public void Setup(string content, Transform followAnchor)
    {
        text.text = content;
        anchor = followAnchor;
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsVisible && anchor != null)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(anchor.position);
            transform.position = screenPos;
        }
    }

    public void HideBubble()
    {
        gameObject.SetActive(false);
    }
}
